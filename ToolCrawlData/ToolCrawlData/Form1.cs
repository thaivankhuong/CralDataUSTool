using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ExcelDataReader;
using Microsoft.CSharp.RuntimeBinder;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ToolCrawlData.Common;
using ToolCrawlData.Data;


namespace ToolCrawlData
{
    public partial class Form1 : Form
    {
        public const string _Dallas = "Dallas";
        public const string _Denton = "Denton";
        //
        public List<string> listAddress = new List<string>();
        private DBContext db = new DBContext();
        private IWebDriver firefoxDriverCallDetail_v1 = null;
        private IWebDriver firefoxDriverCallDetail_v2 = null;
        private IWebDriver firefoxDriverCallDetail_v3 = null;
        private IWebDriver firefoxDriverCallDetail_v4 = null;
        private IWebDriver firefoxDriverCallDetail_v5 = null;
        private int countSuccessDetail = 0;
        //

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txtpassword.PasswordChar = '*';
            this.comboBox1.Items.Add(_Dallas);
            this.comboBox1.Items.Add(_Denton);
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Select file";
                fdlg.FileName = this.txtfromExcel.Text;
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                bool flag = fdlg.ShowDialog() == DialogResult.OK;
                if (flag)
                {
                    this.txtfromExcel.Text = fdlg.FileName;
                    using (FileStream stream = File.Open(fdlg.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream, null);
                        ExcelDataSetConfiguration excelDataSetConfiguration = new ExcelDataSetConfiguration();
                        excelDataSetConfiguration.ConfigureDataTable = ((IExcelDataReader _) => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        });
                        ExcelDataSetConfiguration conf = excelDataSetConfiguration;
                        DataSet dataSet = reader.AsDataSet(conf);
                        DataTable dataTable = dataSet.Tables[0];
                        this.dataGridView1.Rows.Clear();
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            DataGridViewRow newRow = new DataGridViewRow();
                            newRow.CreateCells(this.dataGridView1);
                            string _addressIndex = "";
                            if (this.comboBox1.SelectedItem.ToString() == _Dallas)
                                _addressIndex = "Address";
                            else if (this.comboBox1.SelectedItem.ToString() == _Denton)
                                _addressIndex = "site_addr";
                            object obj = dataTable.Rows[i][_addressIndex];
                            string text;
                            if (obj == null)
                            {
                                text = null;
                            }
                            else
                            {
                                string text2 = obj.ToString();
                                text = ((text2 != null) ? text2.Trim() : null);
                            }
                            string address = text;
                            newRow.Cells[0].Value = address;
                            bool flag2 = !this.listAddress.Contains(address);
                            if (flag2)
                            {
                                this.listAddress.Add(address);
                                this.dataGridView1.Rows.Add(newRow);
                            }
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show("Data import isvalid");
            }
        }

        private void btnGetOwnerName_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem.ToString() == _Denton)
            {
                this.SearchAllAddressDeton();
            }
            else if (this.comboBox1.SelectedItem.ToString() == _Dallas)
            {
                this.SearchAllAddressDallas();
            }
        }

        #region Denton
        public void SearchAllAddressDeton()
        {
            bool flag = this.listAddress.Any<string>();
            if (flag)
            {
                List<string> _listAddress = new List<string>();
                int page = this.listAddress.Count / 10;
                double perpage = (double)(this.listAddress.Count % 10);
                for (int i = 1; i <= 10; i++)
                {
                    bool flag2 = page == 0;
                    if (flag2)
                    {
                        page = 1;
                    }
                    string[] books = this.listAddress.Skip((i - 1) * page).Take(page).ToArray<string>();
                    bool flag3 = books.Any<string>();
                    if (flag3)
                    {
                        _listAddress.AddRange(books);
                        Thread t = new Thread(delegate ()
                        {
                            for (int j = 0; j < books.Count<string>(); j++)
                            {
                                this.SearchDataDeton(books[j]);
                            }
                        });
                        t.Start();
                        t.IsBackground = true;
                    }
                }
                bool flag4 = _listAddress.Count < this.listAddress.Count;
                if (flag4)
                {
                    List<string> listserchnew = (from s in this.listAddress
                                                 where !_listAddress.Any((string x) => x == s)
                                                 select s).ToList<string>();
                    Thread t2 = new Thread(delegate ()
                    {
                        for (int j = 0; j < listserchnew.Count<string>(); j++)
                        {
                            this.SearchDataDeton(listserchnew[j]);
                        }
                    });
                    t2.Start();
                    t2.IsBackground = true;
                }
            }
        }

        public void SearchDataDeton(string address)
        {
            Stopwatch watch = Stopwatch.StartNew();
            address = address.Trim();
            IWebDriver firefoxDriver = this.FirefoxDriverHide();
            try
            {
                bool flag = string.IsNullOrEmpty(address);
                if (flag)
                {
                    MessageBox.Show("Địa chỉ không được bỏ trống");
                    string result = "Update " + address + "false";
                }
                else
                {
                    firefoxDriver.Url = "https://propaccess.trueautomation.com/clientdb/?cid=19";
                    firefoxDriver.Navigate();
                    IWebElement idinputsearch = firefoxDriver.FindElement(By.Id("propertySearchOptions_searchText"));
                    idinputsearch.SendKeys(address);
                    IWebElement btseach = firefoxDriver.FindElement(By.Id("propertySearchOptions_search"));
                    btseach.Click();
                    WebDriverWait enterMail = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10.0));
                    WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(30.0));
                    wait.Until<bool>((IWebDriver x) => ((IJavaScriptExecutor)firefoxDriver).ExecuteScript("return document.readyState", Array.Empty<object>()).Equals("complete"));
                    IWebElement propertySearchResults_resultsTable = firefoxDriver.FindElement(By.Id("propertySearchResults_resultsTable"));
                    string text = propertySearchResults_resultsTable.Text;
                    bool flag2 = Regex.Matches(text, "View Details").Count == 1;
                    if (flag2)
                    {
                        ReadOnlyCollection<IWebElement> listdiva = propertySearchResults_resultsTable.FindElements(By.TagName("a"));
                        bool flag3 = listdiva.Any<IWebElement>();
                        if (flag3)
                        {
                            IWebElement objadetails = listdiva[0];
                            string objahref = objadetails.GetAttribute("href");
                            firefoxDriver.Url = objahref;
                            firefoxDriver.Navigate();
                            IWebElement detailProperTy = firefoxDriver.FindElement(By.Id("propertyDetails"));
                            string stringfulldata = ((RemoteWebElement)detailProperTy).Text;
                            string[] read = stringfulldata.Split(new char[]
                            {
                                '\r',
                                '\n'
                            }, StringSplitOptions.RemoveEmptyEntries);
                            MatchCollection objaddress = Regex.Matches(stringfulldata, "Address(.*?)Mapsco", RegexOptions.Singleline);
                            string straddress = objaddress[0].Value.Replace("Address:", "").Replace("Mapsco", "").Replace("\r\n", "");
                            MatchCollection objname = Regex.Matches(stringfulldata, "Name:(.*?)Owner ID", RegexOptions.Singleline);
                            string strName = objname[0].Value.Replace("Name:", "").Replace("Owner ID", "").Replace("\r\n", "").Trim();
                            string result = "Success " + address + " " + strName;
                            this.setdelegateUpdateValueDataGridView(address, true, strName);
                        }
                        else
                        {
                            this.setdelegateUpdateValueDataGridView(address, false, "Failse");
                            string result = address + " Failse. Notfound";
                        }
                    }
                    else
                    {
                        bool flag4 = Regex.Matches(text, "View Details").Count > 1;
                        if (flag4)
                        {
                            this.setdelegateUpdateValueDataGridView(address, false, "have more than 1 data line");
                            string result = address + " have more than 1 data line";
                        }
                        else
                        {
                            this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                            string result = address + " Khong tim thay du lieu";
                        }
                    }
                    watch.Stop();
                    long elapsedMs = watch.ElapsedMilliseconds;
                    firefoxDriver.Quit();
                    this.FinishProcess(firefoxDriver);
                }
            }
            catch (Exception exx)
            {
                watch.Stop();
                long elapsedMs2 = watch.ElapsedMilliseconds;
                string result = address + " Khong tim thay du lieu";
                this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                firefoxDriver.Quit();
                this.FinishProcess(firefoxDriver);
            }
        }

        private delegate void SetDataGridViewCallback(string address, bool isCrawlData, string resultData);
        private void setdelegateUpdateValueDataGridView(string address, bool isCrawlData, string resultData)
        {
            bool invokeRequired = this.dataGridView1.InvokeRequired;
            if (invokeRequired)
            {
                Form1.SetDataGridViewCallback d = new Form1.SetDataGridViewCallback(this.setdelegateUpdateValueDataGridView);
                base.Invoke(d, new object[]
                {
                    address,
                    isCrawlData,
                    resultData
                });
            }
            else
            {
                this.UpdateValueDataGridView(address, isCrawlData, resultData);
            }
        }

        public void UpdateValueDataGridView(string address, bool isCrawlData, string resultData)
        {
            try
            {
                DataGridViewRow row = (from DataGridViewRow r in this.dataGridView1.Rows
                                       where r.Cells["Address"].Value.ToString().Equals(address)
                                       select r).First<DataGridViewRow>();
                int rowIndex = row.Index;
                bool flag = rowIndex > -1;
                if (flag)
                {
                    if (isCrawlData)
                    {
                        bool flag2 = resultData.Contains(",");
                        if (flag2)
                        {
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.Cells[1].Value = resultData;
                            row.Cells[2].Value = "True";
                            Tuple<string, string, string> result = this.GetFirstNameAndLastnameByFullName(resultData);
                            row.Cells[3].Value = result.Item1;
                            row.Cells[4].Value = result.Item2;
                            row.Cells[5].Value = result.Item3;
                            this.db.insertDAta(new SearchData
                            {
                                Address = Convert.ToString(row.Cells["Address"].Value),
                                FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                                FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                                MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                                LastName = Convert.ToString(row.Cells["LastName"].Value)
                            });
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                            row.Cells[1].Value = resultData;
                            row.Cells[2].Value = "False";
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.Cells[1].Value = resultData;
                        row.Cells[2].Value = "False";
                    }
                }
                IEnumerable<DataGridViewRow> enumerable = from DataGridViewRow r in this.dataGridView1.Rows
                                                          where Convert.ToString(r.Cells["Status"].Value).Equals("True") || Convert.ToString(r.Cells["Status"].Value).Equals("False")
                                                          select r;
                List<DataGridViewRow> rows = (enumerable != null) ? enumerable.ToList<DataGridViewRow>() : null;
                bool flag3 = rows.Any<DataGridViewRow>() && rows.Count == this.listAddress.Count;
                if (flag3)
                {
                    MessageBox.Show("Completed " + DateTime.Now);
                }
            }
            catch (Exception exx)
            {

            }
        }

        public Tuple<string, string, string> GetFirstNameAndLastnameByFullName(string fullName)
        {
            string[] arrstring = fullName.Split(new char[]
            {
                ','
            });
            string[] arrlastphay = arrstring[1].Split(new char[]
            {
                ' '
            });
            bool flag = arrlastphay.Any<string>();
            Tuple<string, string, string> result;
            if (flag)
            {
                arrlastphay = (from s in arrlastphay
                               where !string.IsNullOrEmpty(s)
                               select s).ToArray<string>();
                result = new Tuple<string, string, string>(arrstring[0].Trim(), "", arrlastphay[0]);
            }
            else
            {
                result = new Tuple<string, string, string>("", "", "");
            }
            return result;
        }

        #endregion

        #region Dallas
        public void SearchAllAddressDallas()
        {
            bool flag = this.listAddress.Any<string>();
            if (flag)
            {
                List<string> _listAddress = new List<string>();
                int page = this.listAddress.Count / 10;
                double perpage = (double)(this.listAddress.Count % 10);
                for (int i = 1; i <= 10; i++)
                {
                    bool flag2 = page == 0;
                    if (flag2)
                    {
                        page = 1;
                    }
                    string[] books = this.listAddress.Skip((i - 1) * page).Take(page).ToArray<string>();
                    bool flag3 = books.Any<string>();
                    if (flag3)
                    {
                        _listAddress.AddRange(books);
                        Thread t = new Thread(delegate ()
                        {
                            for (int j = 0; j < books.Count<string>(); j++)
                            {
                                this.SearchDataDallas(books[j]);
                            }
                        });
                        t.Start();
                        t.IsBackground = true;
                    }
                }
                bool flag4 = _listAddress.Count < this.listAddress.Count;
                if (flag4)
                {
                    List<string> listserchnew = (from s in this.listAddress
                                                 where !_listAddress.Any((string x) => x == s)
                                                 select s).ToList<string>();
                    Thread t2 = new Thread(delegate ()
                    {
                        for (int j = 0; j < listserchnew.Count<string>(); j++)
                        {
                            this.SearchDataDallas(listserchnew[j]);
                        }
                    });
                    t2.Start();
                    t2.IsBackground = true;
                }
            }
        }

        public void SearchDataDallas(string address)
        {
            address = address.Trim();
            IWebDriver firefoxDriver = this.FirefoxDriverHide();
            try
            {
                bool flag = string.IsNullOrEmpty(address);
                if (flag)
                {
                    MessageBox.Show("Địa chỉ không được bỏ trống");
                    string result = "Update " + address + "false";
                }
                else
                {
                    firefoxDriver.Url = "https://www.dallascad.org/SearchAddr.aspx";
                    firefoxDriver.Navigate();
                    Tuple<string, string> resultAddress = this.ConverAddressDeton(address);
                    IWebElement addressNumber = firefoxDriver.FindElement(By.Id("txtAddrNum"));
                    addressNumber.SendKeys(resultAddress.Item1);
                    IWebElement addressName = firefoxDriver.FindElement(By.Id("txtStName"));
                    addressName.SendKeys(resultAddress.Item2);
                    WebDriverWait enterMail = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(10.0));
                    IWebElement checkBox = firefoxDriver.FindElement(By.Id("AcctTypeCheckList1_chkAcctType_0"));
                    bool flag2 = !checkBox.Selected;
                    if (flag2)
                    {
                        checkBox.Click();
                    }
                    IWebElement checkBox2 = firefoxDriver.FindElement(By.Id("AcctTypeCheckList1_chkAcctType_1"));
                    bool selected = checkBox2.Selected;
                    if (selected)
                    {
                        checkBox2.Click();
                    }
                    IWebElement checkBox3 = firefoxDriver.FindElement(By.Id("AcctTypeCheckList1_chkAcctType_2"));
                    bool selected2 = checkBox3.Selected;
                    if (selected2)
                    {
                        checkBox3.Click();
                    }
                    IWebElement btnSeach = firefoxDriver.FindElement(By.Id("cmdSubmit"));
                    btnSeach.Click();
                    WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(30.0));
                    wait.Until<bool>((IWebDriver x) => ((IJavaScriptExecutor)firefoxDriver).ExecuteScript("return document.readyState", Array.Empty<object>()).Equals("complete"));
                    IWebElement resultdetail = firefoxDriver.FindElement(By.Id("SearchResults1_dgResults"));
                    string html = resultdetail.Text;
                    bool flag3 = resultdetail != null;
                    if (flag3)
                    {
                        ReadOnlyCollection<IWebElement> resulttrs = resultdetail.FindElements(By.TagName("tr"));
                        bool flag4 = resulttrs.Any<IWebElement>();
                        if (flag4)
                        {
                            ReadOnlyCollection<IWebElement> objownernames = resulttrs[2].FindElements(By.TagName("td"));
                            string ownerName = objownernames[3].Text;
                            this.setdelegateUpdateValueDataGridView(address, true, ownerName);
                            firefoxDriver.Quit();
                            this.FinishProcess(firefoxDriver);
                            return;
                        }
                    }
                    this.setdelegateUpdateValueDataGridView(address, true, "Not found Data");
                    firefoxDriver.Quit();
                    this.FinishProcess(firefoxDriver);
                }
            }
            catch (Exception exx)
            {
                string result = address + " Khong tim thay du lieu";
                this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                firefoxDriver.Quit();
                this.FinishProcess(firefoxDriver);
            }
        }


        public Tuple<string, string> ConverAddressDeton(string addressfull)
        {
            Tuple<string, string> result;
            try
            {
                var arrAdrress = addressfull.Split(' ');
                arrAdrress = arrAdrress.Where(s => !string.IsNullOrEmpty(s)).ToArray();

                string addressNumber = arrAdrress[0];
                string addressStress = "";
                for (int i = 1; i < arrAdrress.Count(); i++)
                {
                    if (i == arrAdrress.Count() - 1)
                    {
                        if (!arrPermist.Any((string s) => s == arrAdrress[i]))
                        {
                            addressStress += arrAdrress[i] + " ";
                        }
                    }
                    else
                    {
                        addressStress += arrAdrress[i] + " ";
                    }
                }
                result = new Tuple<string, string>(addressNumber.Trim(), addressStress.Trim());
            }
            catch (Exception exx)
            {
                result = new Tuple<string, string>("", "");
            }
            return result;
        }


        #endregion


        public FirefoxDriver FirefoxDriverHide()
        {
            FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService();
            firefoxDriverService.HideCommandPromptWindow = true;
            FirefoxOptions ffoption = new FirefoxOptions();
            ffoption.AddArgument("-headless");
            return new FirefoxDriver(firefoxDriverService, ffoption);
        }

        public void FinishProcess(IWebDriver iwebdriver)
        {
            DisposeDriverService.FinishHim(iwebdriver);
        }

        private void btnGetDataDetail_Click(object sender, EventArgs e)
        {

            if (listAddress.Any())
            {

                IEnumerable<DataGridViewRow> enumerable = from DataGridViewRow r in this.dataGridView1.Rows
                                                          where Convert.ToString(r.Cells["Status"].Value).Equals("True")
                                                          select r;

                if (enumerable.Any())
                {

                    Thread t = new Thread(delegate ()
                    {
                        foreach (DataGridViewRow item in enumerable)
                        {

                            SearchDetailByDriver1(Convert.ToString(item.Cells["FirstName"].Value), Convert.ToString(item.Cells["LastName"].Value), Convert.ToString(item.Cells["Address"].Value));
                        }
                    });
                    t.Start();
                    t.IsBackground = true;
                }
                else
                {

                }
            }
        }

        public void SearchDetailByDriver1(string firstName, string lastName, string address)
        {
            try
            {
                bool flag = this.firefoxDriverCallDetail_v1 == null;
                if (flag)
                {
                    this.firefoxDriverCallDetail_v1 = this.FirefoxDriverHide();
                    this.firefoxDriverCallDetail_v1.Url = "https://login.publicdata.com/";
                    this.firefoxDriverCallDetail_v1.Navigate();
                    IWebElement enterEmail = this.firefoxDriverCallDetail_v1.FindElement(By.Id("login_id"));
                    enterEmail.SendKeys(this.txtusername.Text);
                    WebDriverWait enterMail = new WebDriverWait(this.firefoxDriverCallDetail_v1, TimeSpan.FromSeconds(10.0));
                    IWebElement password = this.firefoxDriverCallDetail_v1.FindElement(By.Name("password"));
                    password.SendKeys(this.txtpassword.Text);
                    IWebElement login = this.firefoxDriverCallDetail_v1.FindElement(By.ClassName("signin"));
                    login.Click();
                    WebDriverWait fpageHome = new WebDriverWait(this.firefoxDriverCallDetail_v1, TimeSpan.FromSeconds(10.0));
                }
                this.firefoxDriverCallDetail_v1.Url = "https://login.publicdata.com/pdquery.php?o=grp_dl_tx_advanced_main&dlnumber=018955858&dlstate=UID&id=&identifier=&sessionid=";
                this.firefoxDriverCallDetail_v1.Navigate();
                string OwnerName = string.Concat(new string[]
                {
                    "'",
                    firstName,
                    ", ",
                    lastName,
                    " ",
                    address,
                    "'"
                });
                ((IJavaScriptExecutor)this.firefoxDriverCallDetail_v1).ExecuteScript("$('form select')[1].value = 'DPPATX21-C'; $('form input')[0].value = " + OwnerName + ";$('form button')[0].click()", Array.Empty<object>());
                WebDriverWait wait = new WebDriverWait(this.firefoxDriverCallDetail_v1, TimeSpan.FromSeconds(100.0));
                wait.Until<bool>((IWebDriver x) => ((IJavaScriptExecutor)this.firefoxDriverCallDetail_v1).ExecuteScript("return document.readyState", Array.Empty<object>()).Equals("complete"));
                WebDriverWait wait2 = new WebDriverWait(this.firefoxDriverCallDetail_v1, TimeSpan.FromSeconds(1000.0));
                IWebElement ffresults = null;
                bool checkfailse = true;
                int countfailse = 3;
                while (checkfailse )
                {
                    try
                    {
                        bool displayed = this.firefoxDriverCallDetail_v1.FindElement(By.Id("results")).Displayed;
                        if (displayed)
                        {
                            checkfailse = false;
                            ffresults = this.firefoxDriverCallDetail_v1.FindElement(By.Id("results"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(2000);
                        try
                        {
                            bool displayed2 = this.firefoxDriverCallDetail_v1.FindElement(By.Id("results")).Displayed;
                            if (displayed2)
                            {
                                checkfailse = false;
                                ffresults = this.firefoxDriverCallDetail_v1.FindElement(By.Id("results"));
                            }
                            countfailse--;
                            if (countfailse == 0)
                            {
                                checkfailse = false;
                            }
                        }
                        catch (Exception exxx)
                        {
                            countfailse--;
                            if(countfailse == 0)
                            {
                                checkfailse = false;
                            }
                        }
                    }
                }
                ReadOnlyCollection<IWebElement> adetails = ffresults.FindElements(By.TagName("a"));
                ReadOnlyCollection<IWebElement> uldetails = ffresults.FindElements(By.TagName("ul"));
                List<string> listas = new List<string>();
                bool flag2 = uldetails.Count == 0;
                if (flag2)
                {
                    this.setdelegateDataGridViewCallbackSearchOwner(address, false, "", "", "Data not found");
                }
                else
                {
                    bool flag3 = uldetails.Any<IWebElement>();
                    if (flag3)
                    {
                        bool flag4 = uldetails.Count == 1;
                        if (flag4)
                        {
                            string liresult = adetails[1].ToString();
                            IWebElement adetail = ffresults.FindElement(By.ClassName("entry"));
                            IWebElement adetail2 = adetail.FindElement(By.TagName("a"));
                            foreach (IWebElement item in from s in adetails
                                                         where s.Text != "Texas Driver"
                                                         select s)
                            {
                                string htmla = item.GetAttribute("outerHTML");
                                string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
                                Match i = Regex.Match(htmla, HRefPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));
                                bool success = i.Success;
                                if (success)
                                {
                                    string href = Convert.ToString(i.Groups[1]).Replace("amp;", "");
                                    bool flag5 = href.Contains("DPPATX21-C");
                                    if (flag5)
                                    {
                                        listas.Add(href);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (IWebElement item2 in from s in adetails
                                                          where s.Text != "Texas Driver"
                                                          select s)
                            {
                                bool flag6 = item2.Text.Contains(address);
                                if (flag6)
                                {
                                    string htmla2 = item2.GetAttribute("outerHTML");
                                    string HRefPattern2 = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
                                    Match j = Regex.Match(htmla2, HRefPattern2, RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));
                                    bool success2 = j.Success;
                                    if (success2)
                                    {
                                        string href2 = Convert.ToString(j.Groups[1]).Replace("amp;", "");
                                        bool flag7 = href2.Contains("DPPATX21-C");
                                        if (flag7)
                                        {
                                            listas.Add(href2);
                                        }
                                    }
                                }
                            }
                            bool flag8 = !listas.Any<string>();
                            if (flag8)
                            {
                                this.setdelegateDataGridViewCallbackSearchOwner(address, false, "", "", "Mutiple Result: Address not the same");
                                return;
                            }
                        }
                        bool flag9 = listas.Any<string>();
                        if (flag9)
                        {
                            this.firefoxDriverCallDetail_v1.Url = listas[0];
                            this.firefoxDriverCallDetail_v1.Navigate();
                            IWebElement ffdetail = null;
                            try
                            {
                                ffdetail = this.firefoxDriverCallDetail_v1.FindElement(By.Id("dataset-1"));
                            }
                            catch (Exception ex2)
                            {
                                Thread.Sleep(2000);
                                ffdetail = this.firefoxDriverCallDetail_v1.FindElement(By.Id("dataset-1"));
                            }
                            string results = ffdetail.Text;
                            string firstName_Detail = Regex.Matches(results, "First Name(.*?)Middle Name", RegexOptions.Singleline)[0].ToString().Replace("First Name", "").Replace("Middle Name", "").Replace("\r\n", string.Empty).Trim();
                            string midname = Regex.Matches(results, "Middle Name(.*?)Last Name", RegexOptions.Singleline)[0].ToString().Replace("Last Name", "").Replace("Middle Name", "").Replace("\r\n", string.Empty);
                            string lastName_Detail = Regex.Matches(results, "Last Name(.*?)License number", RegexOptions.Singleline)[0].ToString().Replace("Last Name", "").Replace("License number", "").Replace("\r\n", string.Empty);
                            string licenseNumber = Regex.Matches(results, "License number(.*?)License type", RegexOptions.Singleline)[0].ToString().Replace("License number", "").Replace("License type", "").Replace("\r\n", string.Empty);
                            string Address = Regex.Matches(results, "Address(.*?)Date of Birth", RegexOptions.Singleline)[0].ToString().Replace("Address", "").Replace("Date of Birth", "").Replace("\r\n", string.Empty);
                            string dateofbirth = Regex.Matches(results, "Date of Birth(.*?)City", RegexOptions.Singleline)[0].ToString().Replace("Date of Birth", "").Replace("City", "").Replace("\r\n", string.Empty);
                            string city_zipcode = Regex.Matches(results, "City/ZIP Code(.*?)Issue Date", RegexOptions.Singleline)[0].ToString().Replace("City/ZIP Code", "").Replace("Issue Date", "").Replace("\r\n", string.Empty);
                            this.setdelegateDataGridViewCallbackSearchOwner(address, true, licenseNumber, dateofbirth, "True");
                        }
                    }
                }
            }
            catch (Exception ex3)
            {
                this.setdelegateDataGridViewCallbackSearchOwner(address, false, "", "", "Error Exception " + ex3.ToString());
            }
        }
        private delegate void SetDataGridViewCallbackSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse);
        private void setdelegateDataGridViewCallbackSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse)
        {
            bool invokeRequired = this.dataGridView1.InvokeRequired;
            if (invokeRequired)
            {
                Form1.SetDataGridViewCallbackSearchOwner d = new Form1.SetDataGridViewCallbackSearchOwner(this.setdelegateDataGridViewCallbackSearchOwner);
                base.Invoke(d, new object[]
                {
                    address,
                    isCrawlData,
                    licenseNumber,
                    birthDate,
                    resultdatafailse
                });
            }
            else
            {
                this.UpdateValueDataGridViewForDetailSearchOwner(address, isCrawlData, licenseNumber, birthDate, resultdatafailse);
            }
        }

        public void UpdateValueDataGridViewForDetailSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse)
        {
            DataGridViewRow row = (from DataGridViewRow r in this.dataGridView1.Rows
                                   where r.Cells["Address"].Value.ToString().Equals(address)
                                   select r).First<DataGridViewRow>();
            int rowIndex = row.Index;
            bool flag = rowIndex > -1;
            if (flag)
            {
                if (isCrawlData)
                {
                    row.DefaultCellStyle.BackColor = Color.Pink;
                    row.Cells[8].Value = "True";
                    row.Cells[6].Value = licenseNumber;
                    row.Cells[7].Value = birthDate;
                    this.db.UpdateData(new SearchData
                    {
                        Address = Convert.ToString(row.Cells["Address"].Value),
                        FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                        FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                        MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                        LastName = Convert.ToString(row.Cells["LastName"].Value),
                        LicenseNumber = Convert.ToString(row.Cells["LicenseNumber"].Value),
                        BirthDay = Convert.ToString(row.Cells["BirthDay"].Value)
                    });
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    row.Cells[8].Value = resultdatafailse;
                    this.db.UpdateData(new SearchData
                    {
                        Address = Convert.ToString(row.Cells["Address"].Value),
                        FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                        FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                        MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                        LastName = Convert.ToString(row.Cells["LastName"].Value),
                        LicenseNumber = "NotFound",
                        BirthDay = "NotFound"
                    });
                }
            }
            IEnumerable<DataGridViewRow> enumerable = from DataGridViewRow r in this.dataGridView1.Rows
                                                      where Convert.ToString(r.Cells["Status"].Value).Equals("True") && Convert.ToString(r.Cells["ResultDetail"].Value) != ""
                                                      select r;
            List<DataGridViewRow> rowsCallDetail = (enumerable != null) ? enumerable.ToList<DataGridViewRow>() : null;
            IEnumerable<DataGridViewRow> enumerable2 = from DataGridViewRow r in this.dataGridView1.Rows
                                                       where Convert.ToString(r.Cells["Status"].Value).Equals("True")
                                                       select r;
            List<DataGridViewRow> rowsaddress = (enumerable2 != null) ? enumerable2.ToList<DataGridViewRow>() : null;
            bool flag2 = rowsCallDetail.Count == rowsaddress.Count;
            if (flag2)
            {
                MessageBox.Show("Completed " + DateTime.Now);
                this.FinishProcess(this.firefoxDriverCallDetail_v1);
            }
        }

        private void btnclearall_Click(object sender, EventArgs e)
        {
            this.firefoxDriverCallDetail_v1 = null;
            this.firefoxDriverCallDetail_v2 = null;
            this.firefoxDriverCallDetail_v3 = null;
            this.firefoxDriverCallDetail_v4 = null;
            this.firefoxDriverCallDetail_v5 = null;
            this.countSuccessDetail = 0;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();
            this.listAddress = new List<string>();
        }

        private List<string> arrPermist = new List<string>
        {
            "AVE",
            "BLVD",
            "CIR",
            "CT",
            "DR",
            "EXPY",
            "FWY",
            "HWY",
            "LN",
            "PKWY",
            "PL",
            "RD",
            "ST",
            "TRL",
            "WAY"
        };
        public List<string> arrDirection = new List<string>
        {
            "N",
            "S",
            "E",
            "W",
            "NE",
            "NW",
            "SE",
            "SW"
        };
    }
}
    

