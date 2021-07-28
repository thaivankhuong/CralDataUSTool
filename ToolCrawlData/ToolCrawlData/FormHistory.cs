using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Office.Interop.Excel;
using ToolCrawlData.Data;

namespace ToolCrawlData
{
    public partial class FormHistory : Form
    {
        private DBContext db = new DBContext();
        public FormHistory()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
			this.Loaddatatable(true);
		}

        private void FormHistory_Load(object sender, EventArgs e)
        {
			this.Loaddatatable(false);
		}

		public void Loaddatatable(bool issearch = false)
		{
			DataSet data = this.db.loadData();
			bool flag = !issearch;
            System.Data.DataTable datatable;
			if (flag)
			{
				datatable = this.db.loadData().Tables[0];
			}
			else
			{
				datatable = this.db.loadDataBySearch(new SearchData
				{
					Address = this.txtAddress.Text,
					FullName = this.txtName.Text,
					LicenseNumber = this.txtlicensenumber.Text,
					ZipCode = this.txtzipcode.Text
				}).Tables[0];
			}
			this.dataGridView1.Rows.Clear();
			this.dataGridView1.Refresh();
			for (int i = 0; i < datatable.Rows.Count; i++)
			{
				this.dataGridView1.Rows.Add(new object[]
				{
					datatable.Rows[i]["Address"],
					datatable.Rows[i]["FullName"],
					datatable.Rows[i]["FirstName"],
					"",
					datatable.Rows[i]["LastName"],
					datatable.Rows[i]["LicenseNumber"],
					datatable.Rows[i]["BirthDay"],
					datatable.Rows[i]["ZipCode"],
					"X"
				});
			}
		}

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
			bool flag = this.dataGridView1.RowCount > 0;
			if (flag)
			{
				object misValue = Missing.Value;
				Microsoft.Office.Interop.Excel.Application xlApp = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
				Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
				
				Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
				List<DataGridViewRow> rows = this.dataGridView1.Rows.Cast<DataGridViewRow>().ToList<DataGridViewRow>();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
			bool flag = this.dataGridView1.Columns[e.ColumnIndex].Name == "Remove";
			if (flag)
			{
				DataGridViewRow row = (from DataGridViewRow r in this.dataGridView1.Rows
									   where r.Index == e.RowIndex
									   select r).First<DataGridViewRow>();
				string address = Convert.ToString(row.Cells["Address"].Value);
				this.db.RemoveDataByAddress(address);
				this.Loaddatatable(true);
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
			DialogResult dialogResult = MessageBox.Show("Sure", "Accept remove ", MessageBoxButtons.YesNo);
			bool flag3 = dialogResult == DialogResult.Yes;
			if (flag3)
			{
				this.db.RemoveAllData();
				this.Loaddatatable();

			}
		}
    }
}
