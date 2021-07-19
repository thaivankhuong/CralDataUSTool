using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolCrawlData.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.IO;
using ExcelDataReader;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using ToolCrawlData.Common;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ToolCrawlData
{
    public partial class Form1 : Form
    {
        public const string _Dallas = "Dallas";
        public const string _Denton = "Denton";
        public const string _Collin = "Collin";
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
            this.db.createTable();
            this.txtusername.Text = "18955858";
            this.txtpassword.PasswordChar = '*';
            this.comboBox1.Items.Add(_Dallas);
            this.comboBox1.Items.Add(_Denton);
            this.comboBox1.Items.Add(_Collin);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnbrower_Click(object sender, EventArgs e)
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
                        System.Data.DataTable dataTable = dataSet.Tables[0];
                        this.dataGridView1.Rows.Clear();
                        if (this.comboBox1.SelectedItem.ToString() == _Dallas || this.comboBox1.SelectedItem.ToString() == _Denton)
                        {
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
                        else if(this.comboBox1.SelectedItem.ToString() == _Collin)
                        {
                            for (int i = dataTable.Rows.Count - 1; i > 0 ; i--)
                            {
                                var textrow = Convert.ToString(dataTable.Rows[i][2]);
                                var textrowdeription = Convert.ToString(dataTable.Rows[i][1]);
                                if (textrow.Contains("Allen, TX"))
                                {
                                    string address = Convert.ToString(dataTable.Rows[i-1][2]);
                                    if(!listAddress.Any(s=>s == address))
                                    {
                                        DataGridViewRow newRow = new DataGridViewRow();
                                        newRow.CreateCells(this.dataGridView1);
                                        newRow.Cells[0].Value = address;
                                        this.listAddress.Add(address);
                                        this.dataGridView1.Rows.Add(newRow);

                                    }

                                }
                            }
                        }
                        
                    }
                    lbltotalon.Text = listAddress.Count.ToString();
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show("Data import isvalid");
            }
        }

        private void btngetdataowner_Click(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Choose City!");
                return;
            }

            if (!listAddress.Any())
            {
                MessageBox.Show("Data Not Fonund!");
                return;
            }
            btnGetOwnerName.Text = "Get Owner Name...";
            btnGetOwnerName.Enabled = false;
            if (this.comboBox1.SelectedItem.ToString() == _Denton)
            {
                this.SearchAllAddressDeton();
            }
            else if (this.comboBox1.SelectedItem.ToString() == _Dallas)
            {
                this.SearchAllAddressDallas();
            }
            else if (this.comboBox1.SelectedItem.ToString() == _Collin)
            {
                this.SearchAllAddressCollin();
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
                            this.SearchDataDeton(books.ToList());
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
                        this.SearchDataDeton(listserchnew.ToList());
                    });
                    t2.Start();
                    t2.IsBackground = true;
                }
            }
        }

        public void SearchDataDeton(List<string> lstaddress)
        {
           
            IWebDriver firefoxDriver = this.FirefoxDriverHide();
            foreach (var item in lstaddress)
            {
                string address = item;
                try
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
                }
                catch (Exception exx)
                {

                    string result = address + " Khong tim thay du lieu";
                    this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                    firefoxDriver.Quit();
                    this.FinishProcess(firefoxDriver);
                }
            }
            
            firefoxDriver.Quit();
            this.FinishProcess(firefoxDriver);
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
                        if (comboBox1.SelectedItem.ToString() == _Denton)
                        {
                            bool flag3 = resultData.Contains(",");
                            if (flag3)
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
                                lblsuccesson.Text = Convert.ToString( Convert.ToInt32(lblsuccesson.Text) + 1);
                            }
                        }
                        else if(comboBox1.SelectedItem.ToString() == _Dallas)
                        {
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.Cells[1].Value = resultData;
                            row.Cells[2].Value = "True";
                            Tuple<string, string, string> result2 = this.GetFirstNameAndLastnameByFullNameDallas(resultData);
                            row.Cells[3].Value = result2.Item3;
                            row.Cells[4].Value = result2.Item2;
                            row.Cells[5].Value = result2.Item1;
                            this.db.insertDAta(new SearchData
                            {
                                Address = Convert.ToString(row.Cells["Address"].Value),
                                FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                                FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                                MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                                LastName = Convert.ToString(row.Cells["LastName"].Value)
                            });
                            lblsuccesson.Text = Convert.ToString(Convert.ToInt32(lblsuccesson.Text) + 1);
                        }
                        if (comboBox1.SelectedItem.ToString() == _Collin)
                        {
                            row.DefaultCellStyle.BackColor = Color.Green;
                            row.Cells[1].Value = resultData;
                            row.Cells[2].Value = "True";
                            Tuple<string, string> result2 = this.GetFirstNameAndLastnameByFullNameCollin(resultData);
                            row.Cells[3].Value = result2.Item2;
                            row.Cells[5].Value = result2.Item1;
                            this.db.insertDAta(new SearchData
                            {
                                Address = Convert.ToString(row.Cells["Address"].Value),
                                FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                                FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                                MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                                LastName = Convert.ToString(row.Cells["LastName"].Value)
                            });
                            lblsuccesson.Text = Convert.ToString(Convert.ToInt32(lblsuccesson.Text) + 1);
                        }
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.Cells[1].Value = resultData;
                        row.Cells[2].Value = "False";
                        lblfailon.Text = Convert.ToString(Convert.ToInt32(lblfailon.Text) + 1);

                    }
                }
                IEnumerable<DataGridViewRow> enumerable = from DataGridViewRow r in this.dataGridView1.Rows
                                                          where Convert.ToString(r.Cells["Status"].Value).Equals("True") || Convert.ToString(r.Cells["Status"].Value).Equals("False")
                                                          select r;
                List<DataGridViewRow> rows = (enumerable != null) ? enumerable.ToList<DataGridViewRow>() : null;
                bool flag5 = rows.Any<DataGridViewRow>() && rows.Count == this.listAddress.Count;
                if (flag5)
                {
                    MessageBox.Show("Completed " + DateTime.Now);
                }
            }
            catch (Exception exx)
            {
            }
        }

        public Tuple<string, string, string> GetFirstNameAndLastnameByFullNameDallas(string fullName)
        {
            List<string> arrstring = (from s in fullName.Split(new char[]
            {
                ' '
            })
                                      where !string.IsNullOrEmpty(s)
                                      select s).ToList<string>();
            bool flag = arrstring.Any<string>();
            Tuple<string, string, string> result;
            if (flag)
            {
                bool flag2 = arrstring.Count > 1;
                if (flag2)
                {
                    result = new Tuple<string, string, string>(arrstring[0].Trim(), "", arrstring[1]);
                }
                else
                {
                    result = new Tuple<string, string, string>(arrstring[0].Trim(), "", "");
                }
            }
            else
            {
                result = new Tuple<string, string, string>("", "", "");
            }
            return result;
        }

        public Tuple<string, string> GetFirstNameAndLastnameByFullNameCollin(string fullName)
        {
            List<string> arrstring = (from s in fullName.Split(new char[]
            {
                ' '
            })
                                      where !string.IsNullOrEmpty(s)
                                      select s).ToList<string>();
            bool flag = arrstring.Any<string>();
            Tuple<string, string> result;
            if (flag)
            {
                bool flag2 = arrstring.Count > 1;
                if (flag2)
                {
                    result = new Tuple<string, string>(arrstring[0].Trim(), arrstring[1]);
                }
                else
                {
                    result = new Tuple<string, string>(arrstring[0].Trim(), "");
                }
            }
            else
            {
                result = new Tuple<string, string>("", "");
            }
            return result;
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
                            this.SearchDataDallas(books.ToList());
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
                        this.SearchDataDallas(listserchnew.ToList());
                    });
                    t2.Start();
                    t2.IsBackground = true;
                }
            }
        }

        public void SearchDataDallas(List<string> lstaddress)
        {
            int intstartProcess = 0;
            IWebDriver firefoxDriver = this.FirefoxDriverHide();
            foreach (var item in lstaddress)
            {
                string address = item;
                Tuple<string, string, string> resultAddress = this.ConverAddressDeton(address);
                try
                {
                    firefoxDriver.Url = "https://www.dallascad.org/SearchAddr.aspx";
                    firefoxDriver.Navigate();
                    
                    IWebElement addressNumber = firefoxDriver.FindElement(By.Id("txtAddrNum"));
                    addressNumber.SendKeys(resultAddress.Item1);
                    IWebElement addressName = firefoxDriver.FindElement(By.Id("txtStName"));
                    addressName.SendKeys(resultAddress.Item2);

                    if (!string.IsNullOrEmpty(resultAddress.Item3))
                    {
                        IWebElement dropDirection = firefoxDriver.FindElement(By.Id("AcctTypeCheckList1_chkAcctType_0"));
                        dropDirection.SendKeys(resultAddress.Item3);
                    }

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
                    try
                    {
                        WebDriverWait wait = new WebDriverWait(firefoxDriver, TimeSpan.FromSeconds(30.0));
                        wait.Until<bool>((IWebDriver x) => ((IJavaScriptExecutor)firefoxDriver).ExecuteScript("return document.readyState", Array.Empty<object>()).Equals("complete"));

                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    
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
                            intstartProcess++;
                            continue;
                        }
                    }
                    this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                    intstartProcess++;
                    continue;
                }
                catch (Exception exx)
                {
                    string result = address + " Khong tim thay du lieu";
                    this.setdelegateUpdateValueDataGridView(address, false, $"Search Key Data AddressNumber: {resultAddress.Item1} ,Addressname : {resultAddress.Item2}, Direction: {resultAddress.Item3}   Not found Data");
                    intstartProcess++;
                }
            }
            if (intstartProcess == lstaddress.Count)
            {
                firefoxDriver.Quit();
                this.FinishProcess(firefoxDriver);
            }
        }


        public Tuple<string, string, string> ConverAddressDeton(string addressfull)
        {
            Tuple<string, string, string> result;
            try
            {
                var arrAdrress = addressfull.Split(' ');
                arrAdrress = arrAdrress.Where(s => !string.IsNullOrEmpty(s)).ToArray();

                string addressNumber = arrAdrress[0];
                string addressStress = "";
                string Direction = "";
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
                        if (i == 1 && arrDirection.Any(s => s == arrAdrress[i]))
                        {
                            Direction = arrAdrress[i];
                        }
                        else
                        {
                            addressStress += arrAdrress[i] + " ";
                        }
                    }
                }
                result = new Tuple<string, string, string>(addressNumber.Trim(), addressStress.Trim(), Direction.Trim());
            }
            catch (Exception exx)
            {
                result = new Tuple<string, string, string>("", "", "");
            }
            return result;
        }


        #endregion

        #region  Colin
        public void SearchAllAddressCollin()
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
                            this.SearchDataCollin(books.ToList());
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
                        this.SearchDataCollin(listserchnew.ToList());
                    });
                    t2.Start();
                    t2.IsBackground = true;
                }
            }
        }
        public void SearchDataCollin(List<string> lstaddress)
        {
            int intstartProcess = 0;
            IWebDriver firefoxDriver = this.FirefoxDriverHide();
            //IWebDriver firefoxDriver =  new FirefoxDriver();
            foreach (var item in lstaddress)
            {
                string address = item;
                Tuple<string, string> resultAddress = this.ConverAddressCollin(address);
                try
                {
                    //https://www.collincad.org/propertysearch?situs_num=1617&situs_street=Nestledown%Dr


                    firefoxDriver.Url = "https://www.collincad.org/propertysearch?situs_num=" + resultAddress.Item1 + "&situs_street=" + resultAddress.Item2.Replace(" ", "%");
                    //firefoxDriver.Url = "https://www.collincad.org/propertysearch";
                    firefoxDriver.Navigate();
                    if(firefoxDriver.FindElement(By.TagName("body")).Text.Contains("Sorry, but we couldn't find any properties matching the criteria you provided"))
                    {
                        this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                        intstartProcess++;
                        continue;
                    }

                    IWebElement resultdetail = firefoxDriver.FindElement(By.Id("propertysearchresults"));
                    string html = resultdetail.Text;
                    bool flag3 = resultdetail != null;
                    if (flag3)
                    {
                        IWebElement resultbody = resultdetail.FindElement(By.TagName("tbody"));
                        ReadOnlyCollection<IWebElement> resulttrs = resultbody.FindElements(By.TagName("tr"));
                        bool flag4 = resulttrs.Any<IWebElement>();
                        if (flag4)
                        {
                            ReadOnlyCollection<IWebElement> objownernames = resulttrs[0].FindElements(By.TagName("td"));
                            string ownerName = objownernames[2].Text;
                            this.setdelegateUpdateValueDataGridView(address, true, ownerName);
                            intstartProcess++;
                            continue;
                        }
                    }
                    this.setdelegateUpdateValueDataGridView(address, false, "Not found Data");
                    intstartProcess++;
                    continue;
                }
                catch (Exception exx)
                {
                    string result = address + " Khong tim thay du lieu";
                    this.setdelegateUpdateValueDataGridView(address, false, $"Search Key Data AddressNumber: {resultAddress.Item1} ,Addressname : {resultAddress.Item2}   Not found Data");
                    intstartProcess++;
                }
            }
            //if (intstartProcess == lstaddress.Count)
            //{
                firefoxDriver.Quit();
                this.FinishProcess(firefoxDriver);
            //}
        }


        public Tuple<string, string> ConverAddressCollin(string addressfull)
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
                    addressStress += arrAdrress[i] + " ";
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
                while (checkfailse)
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
                            if (countfailse == 0)
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
                            //string firstName_Detail = Regex.Matches(results, "First Name(.*?)Middle Name", RegexOptions.Singleline)[0].ToString().Replace("First Name", "").Replace("Middle Name", "").Replace("\r\n", string.Empty).Trim();
                            //string midname = Regex.Matches(results, "Middle Name(.*?)Last Name", RegexOptions.Singleline)[0].ToString().Replace("Last Name", "").Replace("Middle Name", "").Replace("\r\n", string.Empty);
                            //string lastName_Detail = Regex.Matches(results, "Last Name(.*?)License number", RegexOptions.Singleline)[0].ToString().Replace("Last Name", "").Replace("License number", "").Replace("\r\n", string.Empty);
                            string licenseNumber = Regex.Matches(results, "License number(.*?)License type", RegexOptions.Singleline)[0].ToString().Replace("License number", "").Replace("License type", "").Replace("\r\n", string.Empty);
                            //string Address = Regex.Matches(results, "Address(.*?)Date of Birth", RegexOptions.Singleline)[0].ToString().Replace("Address", "").Replace("Date of Birth", "").Replace("\r\n", string.Empty);
                            string dateofbirth = Regex.Matches(results, "Date of Birth(.*?)City", RegexOptions.Singleline)[0].ToString().Replace("Date of Birth", "").Replace("City", "").Replace("\r\n", string.Empty);
                            string city_zipcode = Regex.Matches(results, "City/ZIP Code(.*?)Issue Date", RegexOptions.Singleline)[0].ToString().Replace("City/ZIP Code", "").Replace("Issue Date", "").Replace("\r\n", string.Empty);
                            this.setdelegateDataGridViewCallbackSearchOwner(address, true, licenseNumber, dateofbirth, "True", city_zipcode);
                        }
                    }
                }
            }
            catch (Exception ex3)
            {
                this.setdelegateDataGridViewCallbackSearchOwner(address, false, "", "", "Error Exception " + ex3.ToString());
            }
        }
        private delegate void SetDataGridViewCallbackSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse, string ZipCode);
        private void setdelegateDataGridViewCallbackSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse, string ZipCode = "")
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
                    resultdatafailse,
                    ZipCode
                });
            }
            else
            {
                this.UpdateValueDataGridViewForDetailSearchOwner(address, isCrawlData, licenseNumber, birthDate, resultdatafailse,ZipCode);
            }
        }

        public void UpdateValueDataGridViewForDetailSearchOwner(string address, bool isCrawlData, string licenseNumber, string birthDate, string resultdatafailse,string zipcode)
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
                    row.Cells[9].Value = zipcode;
                    this.db.UpdateData(new SearchData
                    {
                        Address = Convert.ToString(row.Cells["Address"].Value),
                        FullName = Convert.ToString(row.Cells["OwnerName"].Value),
                        FirstName = Convert.ToString(row.Cells["FirstName"].Value),
                        MiddleName = Convert.ToString(row.Cells["MiddleName"].Value),
                        LastName = Convert.ToString(row.Cells["LastName"].Value),
                        LicenseNumber = licenseNumber,
                        BirthDay = birthDate,
                        ZipCode = zipcode
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

        public List<string> arrStressNameCollin = new List<string>
        {
            "AVE", "BLF", "BLVD", "BND", "CIR", "CRK", "CRST", "CT", "CV", "DR", "EXPY", "FWY", "GLN", "HL", "HOLW", "HWY", "LN", "LNDG", "LOOP", "PARK", "PASS", "PATH", "PKWY", "PL", "PLACE", "PT", "RD", "RDG", "ROW", "RUN", "SQ", "ST", "TER", "TRCE", "TRL", "VW", "WAY", "XING"
        }; 

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
            btnGetDataDetail.Text = "Get DataPulic.Com";
            btnGetDataDetail.Enabled = true;
            btnGetOwnerName.Text = "Get Owner Name";
            btnGetOwnerName.Enabled = true;
            lblfailon.Text = "0";
            lblsuccesson.Text = "0";
            lbltotalon.Text = "0";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = this.dataGridView1.Columns[e.ColumnIndex].Name == "Remove";
            if (flag)
            {
                DataGridViewRow row = (from DataGridViewRow r in this.dataGridView1.Rows
                                       where r.Index == e.RowIndex
                                       select r).First<DataGridViewRow>();
                string address = Convert.ToString(row.Cells["Address"].Value);
                bool flag2 = Convert.ToString(row.Cells["Status"].Value) == "True" || Convert.ToString(row.Cells["ResultDetail"].Value) == "True";
                if (flag2)
                {
                    DialogResult dialogResult = MessageBox.Show("Sure", "Accept remove ", MessageBoxButtons.YesNo);
                    bool flag3 = dialogResult == DialogResult.Yes;
                    if (flag3)
                    {
                        this.listAddress.Remove(address);
                        this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                }
                else
                {
                    this.listAddress.Remove(address);
                    this.dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnGetDataDetail_Click(object sender, EventArgs e)
        {
            bool flag = this.listAddress.Any<string>();
            if (flag)
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
            }
            else
            {
                MessageBox.Show("Data Search NotFound");
            }
        }

        private void btnviewhistory_Click(object sender, EventArgs e)
        {
            FormHistory a = new FormHistory();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = this.dataGridView1.RowCount > 0;
            if (flag)
            {
                object misValue = Missing.Value;
                Microsoft.Office.Interop.Excel.Application xlApp = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
                Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
                
                List<DataGridViewRow> rows = this.dataGridView1.Rows.Cast<DataGridViewRow>().ToList<DataGridViewRow>();
                bool @checked = this.rddataownersuccess.Checked;
                Worksheet xlWorkSheet =  xlWorkBook.Worksheets.get_Item(1);
                if (@checked)
                {
                    rows = (from s in rows
                            where Convert.ToString(s.Cells["Status"].Value) == "True"
                            select s).ToList<DataGridViewRow>();
                }
                bool checked2 = this.rddatasuccessdetail.Checked;
                if (checked2)
                {
                    rows = (from s in rows
                            where Convert.ToString(s.Cells["ResultDetail"].Value) == "True"
                            select s).ToList<DataGridViewRow>();
                }
                for (int x = 1; x < this.dataGridView1.Columns.Count + 1; x++)
                {
                    xlWorkSheet.Cells[1, x] = this.dataGridView1.Columns[x - 1].HeaderText;
                }
                for (int i = 0; i < rows.Count; i++)
                {
                    for (int j = 0; j < this.dataGridView1.ColumnCount; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = Convert.ToString(rows[i].Cells[j].Value);
                    }
                }
                SaveFileDialog saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = "ExportDataCrawl" + Guid.NewGuid();
                saveFileDialoge.DefaultExt = ".xlsx";
                bool flag2 = saveFileDialoge.ShowDialog() == DialogResult.OK;
                if (flag2)
                {
                    xlWorkBook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                this.releaseObject(xlWorkSheet);
                this.releaseObject(xlWorkBook);
                this.releaseObject(xlApp);
            }
            else
            {
                MessageBox.Show("Data Not Found");
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
