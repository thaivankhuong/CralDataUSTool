
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.rddatasuccessdetail = new System.Windows.Forms.RadioButton();
            this.rddataownersuccess = new System.Windows.Forms.RadioButton();
            this.rdfulldata = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetDataDetail = new System.Windows.Forms.Button();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtfromExcel = new System.Windows.Forms.TextBox();
            this.btnGetOwnerName = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnclearall = new System.Windows.Forms.Button();
            this.btnviewhistory = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnbrower = new System.Windows.Forms.Button();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OwnerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiddleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResultDetail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbltotalon = new System.Windows.Forms.Label();
            this.lblfailon = new System.Windows.Forms.Label();
            this.lblsuccesson = new System.Windows.Forms.Label();
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
            this.panel1.Size = new System.Drawing.Size(1305, 146);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.rddatasuccessdetail);
            this.groupBox3.Controls.Add(this.rddataownersuccess);
            this.groupBox3.Controls.Add(this.rdfulldata);
            this.groupBox3.Location = new System.Drawing.Point(843, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 137);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export Excel ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(64, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Export Excel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rddatasuccessdetail
            // 
            this.rddatasuccessdetail.AutoSize = true;
            this.rddatasuccessdetail.Location = new System.Drawing.Point(17, 65);
            this.rddatasuccessdetail.Name = "rddatasuccessdetail";
            this.rddatasuccessdetail.Size = new System.Drawing.Size(122, 17);
            this.rddatasuccessdetail.TabIndex = 2;
            this.rddatasuccessdetail.TabStop = true;
            this.rddatasuccessdetail.Text = "Data Success Detail";
            this.rddatasuccessdetail.UseVisualStyleBackColor = true;
            // 
            // rddataownersuccess
            // 
            this.rddataownersuccess.AutoSize = true;
            this.rddataownersuccess.Location = new System.Drawing.Point(17, 42);
            this.rddataownersuccess.Name = "rddataownersuccess";
            this.rddataownersuccess.Size = new System.Drawing.Size(126, 17);
            this.rddataownersuccess.TabIndex = 1;
            this.rddataownersuccess.TabStop = true;
            this.rddataownersuccess.Text = "Data Owner Success";
            this.rddataownersuccess.UseVisualStyleBackColor = true;
            // 
            // rdfulldata
            // 
            this.rdfulldata.AutoSize = true;
            this.rdfulldata.Location = new System.Drawing.Point(17, 19);
            this.rdfulldata.Name = "rdfulldata";
            this.rdfulldata.Size = new System.Drawing.Size(67, 17);
            this.rdfulldata.TabIndex = 0;
            this.rdfulldata.TabStop = true;
            this.rdfulldata.Text = "Full Data";
            this.rdfulldata.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnGetDataDetail);
            this.groupBox2.Controls.Add(this.txtpassword);
            this.groupBox2.Controls.Add(this.txtusername);
            this.groupBox2.Location = new System.Drawing.Point(462, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 137);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Get Data Detail";
            // 
            // btnGetDataDetail
            // 
            this.btnGetDataDetail.Location = new System.Drawing.Point(85, 97);
            this.btnGetDataDetail.Name = "btnGetDataDetail";
            this.btnGetDataDetail.Size = new System.Drawing.Size(193, 23);
            this.btnGetDataDetail.TabIndex = 2;
            this.btnGetDataDetail.Text = "Get PublicData.Com";
            this.btnGetDataDetail.UseVisualStyleBackColor = true;
            this.btnGetDataDetail.Click += new System.EventHandler(this.btnGetDataDetail_Click);
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(85, 57);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(256, 20);
            this.txtpassword.TabIndex = 2;
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(85, 31);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(256, 20);
            this.txtusername.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblsuccesson);
            this.groupBox1.Controls.Add(this.lblfailon);
            this.groupBox1.Controls.Add(this.lbltotalon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnbrower);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.txtfromExcel);
            this.groupBox1.Controls.Add(this.btnGetOwnerName);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 137);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get Owner Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Import Excel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "City";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(72, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(263, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // txtfromExcel
            // 
            this.txtfromExcel.Location = new System.Drawing.Point(72, 49);
            this.txtfromExcel.Name = "txtfromExcel";
            this.txtfromExcel.Size = new System.Drawing.Size(281, 20);
            this.txtfromExcel.TabIndex = 1;
            // 
            // btnGetOwnerName
            // 
            this.btnGetOwnerName.Location = new System.Drawing.Point(81, 75);
            this.btnGetOwnerName.Name = "btnGetOwnerName";
            this.btnGetOwnerName.Size = new System.Drawing.Size(212, 23);
            this.btnGetOwnerName.TabIndex = 0;
            this.btnGetOwnerName.Text = "Get Data Owner Name";
            this.btnGetOwnerName.UseVisualStyleBackColor = true;
            this.btnGetOwnerName.Click += new System.EventHandler(this.btngetdataowner_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 146);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1305, 636);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "UserName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "PassWord";
            // 
            // btnclearall
            // 
            this.btnclearall.Location = new System.Drawing.Point(1101, 12);
            this.btnclearall.Name = "btnclearall";
            this.btnclearall.Size = new System.Drawing.Size(85, 30);
            this.btnclearall.TabIndex = 1;
            this.btnclearall.Text = "Clear All";
            this.btnclearall.UseVisualStyleBackColor = true;
            this.btnclearall.Click += new System.EventHandler(this.btnclearall_Click);
            // 
            // btnviewhistory
            // 
            this.btnviewhistory.Location = new System.Drawing.Point(1101, 100);
            this.btnviewhistory.Name = "btnviewhistory";
            this.btnviewhistory.Size = new System.Drawing.Size(96, 23);
            this.btnviewhistory.TabIndex = 1;
            this.btnviewhistory.Text = "View History";
            this.btnviewhistory.UseVisualStyleBackColor = true;
            this.btnviewhistory.Click += new System.EventHandler(this.btnviewhistory_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.OwnerName,
            this.Status,
            this.FirstName,
            this.MiddleName,
            this.LastName,
            this.LicenseNumber,
            this.BirthDay,
            this.ResultDetail,
            this.ZipCode,
            this.Remove});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1305, 636);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnbrower
            // 
            this.btnbrower.Location = new System.Drawing.Point(359, 46);
            this.btnbrower.Name = "btnbrower";
            this.btnbrower.Size = new System.Drawing.Size(73, 23);
            this.btnbrower.TabIndex = 3;
            this.btnbrower.Text = "Brower";
            this.btnbrower.UseVisualStyleBackColor = true;
            this.btnbrower.Click += new System.EventHandler(this.btnbrower_Click);
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
            // MiddleName
            // 
            this.MiddleName.HeaderText = "MidlleName";
            this.MiddleName.Name = "MiddleName";
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
            this.Remove.UseColumnTextForButtonValue = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 24);
            this.label5.TabIndex = 5;
            this.label5.Text = "Total : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(143, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "Failse :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(279, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 24);
            this.label7.TabIndex = 5;
            this.label7.Text = "Success :";
            // 
            // lbltotalon
            // 
            this.lbltotalon.AutoSize = true;
            this.lbltotalon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalon.Location = new System.Drawing.Point(68, 101);
            this.lbltotalon.Name = "lbltotalon";
            this.lbltotalon.Size = new System.Drawing.Size(21, 24);
            this.lbltotalon.TabIndex = 5;
            this.lbltotalon.Text = "0";
            // 
            // lblfailon
            // 
            this.lblfailon.AutoSize = true;
            this.lblfailon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfailon.ForeColor = System.Drawing.Color.Red;
            this.lblfailon.Location = new System.Drawing.Point(219, 101);
            this.lblfailon.Name = "lblfailon";
            this.lblfailon.Size = new System.Drawing.Size(21, 24);
            this.lblfailon.TabIndex = 5;
            this.lblfailon.Text = "0";
            // 
            // lblsuccesson
            // 
            this.lblsuccesson.AutoSize = true;
            this.lblsuccesson.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsuccesson.ForeColor = System.Drawing.Color.Green;
            this.lblsuccesson.Location = new System.Drawing.Point(386, 101);
            this.lblsuccesson.Name = "lblsuccesson";
            this.lblsuccesson.Size = new System.Drawing.Size(21, 24);
            this.lblsuccesson.TabIndex = 5;
            this.lblsuccesson.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 782);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton rddatasuccessdetail;
        private System.Windows.Forms.RadioButton rddataownersuccess;
        private System.Windows.Forms.RadioButton rdfulldata;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetDataDetail;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtfromExcel;
        private System.Windows.Forms.Button btnGetOwnerName;
        private System.Windows.Forms.Button btnviewhistory;
        private System.Windows.Forms.Button btnclearall;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnbrower;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn OwnerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResultDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZipCode;
        private System.Windows.Forms.DataGridViewButtonColumn Remove;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblsuccesson;
        private System.Windows.Forms.Label lblfailon;
        private System.Windows.Forms.Label lbltotalon;
        private System.Windows.Forms.Label label5;
    }
}

