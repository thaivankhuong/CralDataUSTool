using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ToolCrawlData.Data
{
    public class DBContext
    {
        public void createConection()
        {
            string _strConnect = "Data Source=MyDatabase.sqlite;Version=3;";
            this._con.ConnectionString = _strConnect;
            this._con.Open();
        }

        public void closeConnection()
        {
            this._con.Close();
        }

        public void createTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS tbl_Search ([id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Address nvarchar(200), FullName nvarchar(200), FirstName nvarchar(200), LastName nvarchar(200), BirthDay varchar(15), LicenseNumber varchar(30), ZipCode nvarchar(200))";
            bool flag = !File.Exists("MyDatabase.sqlite");
            if (flag)
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                this.createConection();
                SQLiteCommand command = new SQLiteCommand(sql, this._con);
                command.ExecuteNonQuery();
                this.closeConnection();
            }
        }

        // Token: 0x06000051 RID: 81 RVA: 0x00008F78 File Offset: 0x00007178
        public DataSet loadData()
        {
            DataSet ds = new DataSet();
            this.createConection();
            SQLiteDataAdapter da = new SQLiteDataAdapter("select * from tbl_Search", this._con);
            da.Fill(ds);
            this.closeConnection();
            return ds;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00008FB8 File Offset: 0x000071B8
        public DataSet loadDataBySearch(SearchData data)
        {
            DataSet ds = new DataSet();
            this.createConection();
            string sql = "select * from tbl_Search where 1 ==1 ";
            bool flag = !string.IsNullOrEmpty(data.Address);
            if (flag)
            {
                sql = sql + " and Address LIKE '%" + data.Address + "%'";
            }
            bool flag2 = !string.IsNullOrEmpty(data.FullName);
            if (flag2)
            {
                sql = sql + " and FullName LIKE '%" + data.FullName + "%'";
            }
            bool flag3 = !string.IsNullOrEmpty(data.LicenseNumber);
            if (flag3)
            {
                sql = sql + " and LicenseNumber LIKE '%" + data.LicenseNumber + "%'";
            }
            if (!string.IsNullOrEmpty(data.ZipCode))
            {
                sql = sql + " and ZipCode LIKE '%" + data.ZipCode + "%'";
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, this._con);
            da.Fill(ds);
            this.closeConnection();
            return ds;
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00009084 File Offset: 0x00007284
        public DataTable loadDataByAddress(string Address)
        {
            DataSet ds = new DataSet();
            this.createConection();
            SQLiteDataAdapter da = new SQLiteDataAdapter(string.Format("select * from tbl_Search where Address = '{0}'", Address), this._con);
            da.Fill(ds);
            this.closeConnection();
            return ds.Tables[0];
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000090D8 File Offset: 0x000072D8
        public bool insertDAta(SearchData data)
        {
            DataTable datatable = this.loadDataByAddress(data.Address);
            bool flag = datatable.Rows.Count == 0;
            bool result;
            if (flag)
            {
                string strInsert = string.Format("INSERT INTO tbl_Search(Address, FullName, FirstName, ZipCode, LastName,BirthDay,LicenseNumber) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", new object[]
                {
                    data.Address,
                    data.FullName,
                    data.FirstName,
                    data.ZipCode,
                    data.LastName,
                    data.BirthDay,
                    data.LicenseNumber
                });
                this.createConection();
                SQLiteCommand cmd = new SQLiteCommand(strInsert, this._con);
                cmd.ExecuteNonQuery();
                this.closeConnection();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool RemoveDataByAddress(string address)
        {
            DataTable datatable = this.loadDataByAddress(address);
            bool flag = datatable.Rows.Count > 0;
            bool result;
            if (flag)
            {
                string strInsert = string.Format("Delete FROM  tbl_Search where Address = '{0}' ", address);
                this.createConection();
                SQLiteCommand cmd = new SQLiteCommand(strInsert, this._con);
                cmd.ExecuteNonQuery();
                this.closeConnection();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public bool RemoveAllData()
        {
            string strInsert = string.Format("Delete FROM  tbl_Search ");
            this.createConection();
            SQLiteCommand cmd = new SQLiteCommand(strInsert, this._con);
            cmd.ExecuteNonQuery();
            this.closeConnection();
            return true;
        }
        public bool UpdateData(SearchData data)
        {
            bool flag = this.loadDataByAddress(data.Address).Columns.Count > 0;
            bool result;
            if (flag)
            {
                string strInsert = string.Format("Update  tbl_Search set  FullName  = '{1}', FirstName='{2}', ZipCode = '{3}', LastName = '{4}',BirthDay = '{5}',LicenseNumber='{6}' where  Address = '{0}' ", new object[]
                {
                    data.Address,
                    data.FullName,
                    data.FirstName,
                    data.ZipCode,
                    data.LastName,
                    data.BirthDay,
                    data.LicenseNumber
                });
                this.createConection();
                SQLiteCommand cmd = new SQLiteCommand(strInsert, this._con);
                cmd.ExecuteNonQuery();
                this.closeConnection();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        // Token: 0x04000071 RID: 113
        private SQLiteConnection _con = new SQLiteConnection();
    }
}
