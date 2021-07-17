namespace ToolCrawlData
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnviewhistory = new System.Windows.Forms.Button();
            this.btnclearall = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.rddatasuccessdetail = new System.Windows.Forms.RadioButton();
            this.rddatasuccessowner = new System.Windows.Forms.RadioButton();
            this.rdfulldata = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetDataDetail = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetOwnerName = new System.Windows.Forms.Button();
            this.btnBrower = new System.Windows.Forms.Button();
            this.txtfromExcel = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnviewhistory);
            this.panel1.Controls.Add(this.btnclearall);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1250, 150);
            this.panel1.TabIndex = 0;
            // 
            // btnviewhistory
            // 
            this.btnviewhistory.Location = new System.Drawing.Point(1139, 89);
            this.btnviewhistory.Name = "btnviewhistory";
            this.btnviewhistory.Size = new System.Drawing.Size(99, 23);
            this.btnviewhistory.TabIndex = 4;
            this.btnviewhistory.Text = "View History";
            this.btnviewhistory.UseVisualStyleBackColor = true;
            // 
            // btnclearall
            // 
            this.btnclearall.Location = new System.Drawing.Point(1139, 27);
            this.btnclearall.Name = "btnclearall";
            this.btnclearall.Size = new System.Drawing.Size(99, 23);
            this.btnclearall.TabIndex = 3;
            this.btnclearall.Text = "Clear All Data";
            this.btnclearall.UseVisualStyleBackColor = true;
            this.btnclearall.Click += new System.EventHandler(this.btnclearall_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExportExcel);
            this.groupBox3.Controls.Add(this.rddatasuccessdetail);
            this.groupBox3.Controls.Add(this.rddatasuccessowner);
            this.groupBox3.Controls.Add(this.rdfulldata);
            this.groupBox3.Location = new System.Drawing.Point(863, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Get Data Detail";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(87, 15);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(119, 23);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Export";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            // 
            // rddatasuccessdetail
            // 
            this.rddatasuccessdetail.AutoSize = true;
            this.rddatasuccessdetail.Location = new System.Drawing.Point(27, 76);
            this.rddatasuccessdetail.Name = "rddatasuccessdetail";
            this.rddatasuccessdetail.Size = new System.Drawing.Size(122, 17);
            this.rddatasuccessdetail.TabIndex = 2;
            this.rddatasuccessdetail.TabStop = true;
            this.rddatasuccessdetail.Text = "Data Success Detail";
            this.rddatasuccessdetail.UseVisualStyleBackColor = true;
            // 
            // rddatasuccessowner
            // 
            this.rddatasuccessowner.AutoSize = true;
            this.rddatasuccessowner.Location = new System.Drawing.Point(27, 57);
            this.rddatasuccessowner.Name = "rddatasuccessowner";
            this.rddatasuccessowner.Size = new System.Drawing.Size(126, 17);
            this.rddatasuccessowner.TabIndex = 1;
            this.rddatasuccessowner.TabStop = true;
            this.rddatasuccessowner.Text = "Data Success Owner";
            this.rddatasuccessowner.UseVisualStyleBackColor = true;
            // 
            // rdfulldata
            // 
            this.rdfulldata.AutoSize = true;
            this.rdfulldata.Location = new System.Drawing.Point(27, 38);
            this.rdfulldata.Name = "rdfulldata";
            this.rdfulldata.Size = new System.Drawing.Size(64, 17);
            this.rdfulldata.TabIndex = 0;
            this.rdfulldata.TabStop = true;
            this.rdfulldata.Text = "FullData";
            this.rdfulldata.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetDataDetail);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtpassword);
            this.groupBox2.Controls.Add(this.txtusername);
            this.groupBox2.Location = new System.Drawing.Point(512, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Get Data Detail";
            // 
            // btnGetDataDetail
            // 
            this.btnGetDataDetail.Location = new System.Drawing.Point(70, 71);
            this.btnGetDataDetail.Name = "btnGetDataDetail";
            this.btnGetDataDetail.Size = new System.Drawing.Size(201, 23);
            this.btnGetDataDetail.TabIndex = 6;
            this.btnGetDataDetail.Text = "Get DataPulic.Com";
            this.btnGetDataDetail.UseVisualStyleBackColor = true;
            this.btnGetDataDetail.Click += new System.EventHandler(this.btnGetDataDetail_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "PassWord :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "UserName:";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(70, 48);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(246, 20);
            this.txtpassword.TabIndex = 1;
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(70, 23);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(246, 20);
            this.txtusername.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetOwnerName);
            this.groupBox1.Controls.Add(this.btnBrower);
            this.groupBox1.Controls.Add(this.txtfromExcel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get Owner Name";
            // 
            // btnGetOwnerName
            // 
            this.btnGetOwnerName.Location = new System.Drawing.Point(100, 71);
            this.btnGetOwnerName.Name = "btnGetOwnerName";
            this.btnGetOwnerName.Size = new System.Drawing.Size(150, 23);
            this.btnGetOwnerName.TabIndex = 5;
            this.btnGetOwnerName.Text = "Get Owner Name";
            this.btnGetOwnerName.UseVisualStyleBackColor = true;
            this.btnGetOwnerName.Click += new System.EventHandler(this.btnGetOwnerName_Click);
            // 
            // btnBrower
            // 
            this.btnBrower.Location = new System.Drawing.Point(373, 42);
            this.btnBrower.Name = "btnBrower";
            this.btnBrower.Size = new System.Drawing.Size(75, 23);
            this.btnBrower.TabIndex = 4;
            this.btnBrower.Text = "Brower";
            this.btnBrower.UseVisualStyleBackColor = true;
            this.btnBrower.Click += new System.EventHandler(this.btnBrower_Click);
            // 
            // txtfromExcel
            // 
            this.txtfromExcel.Location = new System.Drawing.Point(77, 45);
            this.txtfromExcel.Name = "txtfromExcel";
            this.txtfromExcel.Size = new System.Drawing.Size(292, 20);
            this.txtfromExcel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Import Excel";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(77, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(237, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "City";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1250, 553);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.OwnerName,
            this.Status,
            this.FirstName,
            this.LastName,
            this.LicenseNumber,
            this.BirthDay,
            this.ResultDetail,
            this.ZipCode,
            this.Remove});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1250, 553);
            this.dataGridView1.TabIndex = 0;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // OwnerName
            // 
            this.OwnerName.HeaderText = "OwnerName";
            this.OwnerName.Name = "OwnerName";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            // 
            // FirstName
            // 
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            // 
            // LastName
            // 
            this.LastName.HeaderText = "LastName";
            this.LastName.Name = "LastName";
            // 
            // LicenseNumber
            // 
            this.LicenseNumber.HeaderText = "LicenseNumber";
            this.LicenseNumber.Name = "LicenseNumber";
            // 
            // BirthDay
            // 
            this.BirthDay.HeaderText = "BirthDay";
            this.BirthDay.Name = "BirthDay";
            // 
            // ResultDetail
            // 
            this.ResultDetail.HeaderText = "ResultDetail";
            this.ResultDetail.Name = "ResultDetail";
            // 
            // ZipCode
            // 
            this.ZipCode.HeaderText = "ZipCode";
            this.ZipCode.Name = "ZipCode";
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.Name = "Remove";
            this.Remove.Text = "Remove";
            this.Remove.ToolTipText = "Remove";
            this.Remove.UseColumnTextForButtonValue = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 703);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.RadioButton rddatasuccessdetail;
        private System.Windows.Forms.RadioButton rddatasuccessowner;
        private System.Windows.Forms.RadioButton rdfulldata;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetDataDetail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetOwnerName;
        private System.Windows.Forms.Button btnBrower;
        private System.Windows.Forms.TextBox txtfromExcel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnviewhistory;
        private System.Windows.Forms.Button btnclearall;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZipCode;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
    }
}

