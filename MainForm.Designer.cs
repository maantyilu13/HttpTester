﻿/*
 * Created by SharpDevelop.
 * User: Kevin
 * Date: 2017/5/13
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace HttpTest
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.RichTextBox responseResult;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox edtUser;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox edtPassword;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.responseResult = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.edtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edtPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.clearParam = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cookieBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.enableAllowAutoRedirect = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.urlList = new System.Windows.Forms.ComboBox();
            this.acceptCombo = new System.Windows.Forms.ComboBox();
            this.contentTypeCombo = new System.Windows.Forms.ComboBox();
            this.encodeList = new System.Windows.Forms.ComboBox();
            this.requestMethod = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.webName = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.basePort = new System.Windows.Forms.TextBox();
            this.baseIp = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.getParam_view = new System.Windows.Forms.DataGridView();
            this.get_param = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.get_value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postData = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.referText = new System.Windows.Forms.TextBox();
            this.fixedRefer = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.showImage = new System.Windows.Forms.PictureBox();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.requestResult = new System.Windows.Forms.RichTextBox();
            this.log = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getParam_view)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showImage)).BeginInit();
            this.resultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // responseResult
            // 
            this.responseResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.responseResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.responseResult.Location = new System.Drawing.Point(273, 10);
            this.responseResult.Margin = new System.Windows.Forms.Padding(8);
            this.responseResult.Name = "responseResult";
            this.responseResult.ReadOnly = true;
            this.responseResult.Size = new System.Drawing.Size(636, 243);
            this.responseResult.TabIndex = 1;
            this.responseResult.Text = "";
            this.responseResult.TextChanged += new System.EventHandler(this.edtResult_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "请求URL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(556, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 21);
            this.button1.TabIndex = 3;
            this.button1.Text = "打开URL文件[发送批量请求]";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.Olive;
            this.btnTest.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location = new System.Drawing.Point(556, 177);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(179, 73);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "发送请求";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.BtnTestClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "用户名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // edtUser
            // 
            this.edtUser.Location = new System.Drawing.Point(54, 18);
            this.edtUser.Name = "edtUser";
            this.edtUser.Size = new System.Drawing.Size(108, 21);
            this.edtUser.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "密码";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // edtPassword
            // 
            this.edtPassword.Location = new System.Drawing.Point(54, 45);
            this.edtPassword.Name = "edtPassword";
            this.edtPassword.Size = new System.Drawing.Size(108, 21);
            this.edtPassword.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.clearParam);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.cookieBox);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.enableAllowAutoRedirect);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.urlList);
            this.panel1.Controls.Add(this.acceptCombo);
            this.panel1.Controls.Add(this.contentTypeCombo);
            this.panel1.Controls.Add(this.encodeList);
            this.panel1.Controls.Add(this.requestMethod);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.webName);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.labelPort);
            this.panel1.Controls.Add(this.basePort);
            this.panel1.Controls.Add(this.baseIp);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.getParam_view);
            this.panel1.Controls.Add(this.postData);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(919, 306);
            this.panel1.TabIndex = 9;
            // 
            // clearParam
            // 
            this.clearParam.Location = new System.Drawing.Point(67, 153);
            this.clearParam.Name = "clearParam";
            this.clearParam.Size = new System.Drawing.Size(87, 23);
            this.clearParam.TabIndex = 25;
            this.clearParam.Text = "清空拼接参数";
            this.clearParam.UseVisualStyleBackColor = true;
            this.clearParam.Click += new System.EventHandler(this.clearParam_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(660, 125);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "清理记录";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(660, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "清除Cookie";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cookieBox
            // 
            this.cookieBox.Location = new System.Drawing.Point(68, 95);
            this.cookieBox.Name = "cookieBox";
            this.cookieBox.ReadOnly = true;
            this.cookieBox.Size = new System.Drawing.Size(586, 21);
            this.cookieBox.TabIndex = 24;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 98);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 23;
            this.label14.Text = "cookie:";
            // 
            // enableAllowAutoRedirect
            // 
            this.enableAllowAutoRedirect.AutoSize = true;
            this.enableAllowAutoRedirect.Checked = true;
            this.enableAllowAutoRedirect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableAllowAutoRedirect.Location = new System.Drawing.Point(636, 29);
            this.enableAllowAutoRedirect.Name = "enableAllowAutoRedirect";
            this.enableAllowAutoRedirect.Size = new System.Drawing.Size(84, 16);
            this.enableAllowAutoRedirect.TabIndex = 22;
            this.enableAllowAutoRedirect.Text = "自动重定向";
            this.enableAllowAutoRedirect.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(341, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 21;
            this.label12.Text = "Accept";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "ContentType";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(283, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "请求体数据";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "拼接参数";
            // 
            // urlList
            // 
            this.urlList.FormattingEnabled = true;
            this.urlList.Location = new System.Drawing.Point(68, 127);
            this.urlList.Name = "urlList";
            this.urlList.Size = new System.Drawing.Size(586, 20);
            this.urlList.TabIndex = 0;
            this.urlList.SelectedIndexChanged += new System.EventHandler(this.urlList_SelectedIndexChanged);
            this.urlList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.urlList_KeyUp);
            // 
            // acceptCombo
            // 
            this.acceptCombo.FormattingEnabled = true;
            this.acceptCombo.Items.AddRange(new object[] {
            "text/html",
            "application/json",
            "text/xml",
            "image/*",
            "application/x-shockwave-flash",
            "application/vnd.ms-excel",
            "application/vnd.ms-powerpoint",
            "application/msword",
            "*/*"});
            this.acceptCombo.Location = new System.Drawing.Point(391, 64);
            this.acceptCombo.Name = "acceptCombo";
            this.acceptCombo.Size = new System.Drawing.Size(166, 20);
            this.acceptCombo.TabIndex = 19;
            // 
            // contentTypeCombo
            // 
            this.contentTypeCombo.FormattingEnabled = true;
            this.contentTypeCombo.Items.AddRange(new object[] {
            "application/x-www-form-urlencoded",
            "application/json",
            "multipart/form-data",
            "text/html",
            "text/xml"});
            this.contentTypeCombo.Location = new System.Drawing.Point(87, 64);
            this.contentTypeCombo.Name = "contentTypeCombo";
            this.contentTypeCombo.Size = new System.Drawing.Size(237, 20);
            this.contentTypeCombo.TabIndex = 19;
            // 
            // encodeList
            // 
            this.encodeList.FormattingEnabled = true;
            this.encodeList.Location = new System.Drawing.Point(607, 64);
            this.encodeList.Name = "encodeList";
            this.encodeList.Size = new System.Drawing.Size(128, 20);
            this.encodeList.TabIndex = 19;
            // 
            // requestMethod
            // 
            this.requestMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.requestMethod.FormattingEnabled = true;
            this.requestMethod.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD"});
            this.requestMethod.Location = new System.Drawing.Point(556, 27);
            this.requestMethod.Name = "requestMethod";
            this.requestMethod.Size = new System.Drawing.Size(58, 20);
            this.requestMethod.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(572, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "编码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(497, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "请求方式";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "基础URL";
            // 
            // webName
            // 
            this.webName.Location = new System.Drawing.Point(391, 27);
            this.webName.Name = "webName";
            this.webName.Size = new System.Drawing.Size(100, 21);
            this.webName.TabIndex = 13;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(250, 33);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 12);
            this.labelPort.TabIndex = 12;
            this.labelPort.Text = "端口";
            // 
            // basePort
            // 
            this.basePort.Location = new System.Drawing.Point(285, 28);
            this.basePort.Name = "basePort";
            this.basePort.Size = new System.Drawing.Size(39, 21);
            this.basePort.TabIndex = 11;
            // 
            // baseIp
            // 
            this.baseIp.Location = new System.Drawing.Point(68, 30);
            this.baseIp.Name = "baseIp";
            this.baseIp.Size = new System.Drawing.Size(157, 21);
            this.baseIp.TabIndex = 10;
            this.baseIp.Text = "http://127.0.0.1";
            this.baseIp.TextChanged += new System.EventHandler(this.baseIp_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Olive;
            this.label15.Location = new System.Drawing.Point(283, 294);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 12);
            this.label15.TabIndex = 9;
            this.label15.Text = "响应结果：";
            this.label15.Click += new System.EventHandler(this.label6_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Olive;
            this.label6.Location = new System.Drawing.Point(10, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "请求结果：";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Olive;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "请求参数：";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "URL前缀";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // getParam_view
            // 
            this.getParam_view.AllowDrop = true;
            this.getParam_view.AllowUserToOrderColumns = true;
            this.getParam_view.AllowUserToResizeRows = false;
            this.getParam_view.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.getParam_view.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.getParam_view.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.get_param,
            this.get_value});
            this.getParam_view.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.getParam_view.Location = new System.Drawing.Point(12, 177);
            this.getParam_view.Margin = new System.Windows.Forms.Padding(5);
            this.getParam_view.Name = "getParam_view";
            this.getParam_view.RowHeadersVisible = false;
            this.getParam_view.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.getParam_view.RowTemplate.Height = 23;
            this.getParam_view.ShowEditingIcon = false;
            this.getParam_view.Size = new System.Drawing.Size(230, 100);
            this.getParam_view.TabIndex = 15;
            // 
            // get_param
            // 
            this.get_param.HeaderText = "参数";
            this.get_param.Name = "get_param";
            // 
            // get_value
            // 
            this.get_value.HeaderText = "值";
            this.get_value.Name = "get_value";
            // 
            // postData
            // 
            this.postData.Location = new System.Drawing.Point(285, 177);
            this.postData.Multiline = true;
            this.postData.Name = "postData";
            this.postData.Size = new System.Drawing.Size(249, 100);
            this.postData.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.referText);
            this.groupBox1.Controls.Add(this.fixedRefer);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.showImage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.edtUser);
            this.groupBox1.Controls.Add(this.edtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(741, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 296);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "认证";
            // 
            // referText
            // 
            this.referText.Location = new System.Drawing.Point(54, 79);
            this.referText.Multiline = true;
            this.referText.Name = "referText";
            this.referText.Size = new System.Drawing.Size(108, 71);
            this.referText.TabIndex = 26;
            // 
            // fixedRefer
            // 
            this.fixedRefer.AutoSize = true;
            this.fixedRefer.Location = new System.Drawing.Point(8, 102);
            this.fixedRefer.Name = "fixedRefer";
            this.fixedRefer.Size = new System.Drawing.Size(48, 16);
            this.fixedRefer.TabIndex = 28;
            this.fixedRefer.Text = "固定";
            this.fixedRefer.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 83);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 27;
            this.label17.Text = "Refer:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(2, 166);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 9;
            this.label16.Text = "图片结果预览：";
            // 
            // showImage
            // 
            this.showImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.showImage.Location = new System.Drawing.Point(0, 181);
            this.showImage.Name = "showImage";
            this.showImage.Size = new System.Drawing.Size(168, 115);
            this.showImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.showImage.TabIndex = 2;
            this.showImage.TabStop = false;
            // 
            // resultPanel
            // 
            this.resultPanel.Controls.Add(this.responseResult);
            this.resultPanel.Controls.Add(this.requestResult);
            this.resultPanel.Controls.Add(this.log);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPanel.Location = new System.Drawing.Point(0, 306);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Padding = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.resultPanel.Size = new System.Drawing.Size(919, 275);
            this.resultPanel.TabIndex = 11;
            // 
            // requestResult
            // 
            this.requestResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.requestResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.requestResult.Location = new System.Drawing.Point(0, 10);
            this.requestResult.Name = "requestResult";
            this.requestResult.ReadOnly = true;
            this.requestResult.Size = new System.Drawing.Size(273, 243);
            this.requestResult.TabIndex = 0;
            this.requestResult.Text = "";
            this.requestResult.TextChanged += new System.EventHandler(this.edtResult_TextChanged);
            // 
            // log
            // 
            this.log.AutoSize = true;
            this.log.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.log.Location = new System.Drawing.Point(0, 253);
            this.log.Margin = new System.Windows.Forms.Padding(0);
            this.log.Name = "log";
            this.log.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.log.Size = new System.Drawing.Size(58, 12);
            this.log.TabIndex = 2;
            this.log.Text = "message:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 581);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HTTPClient Tester";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getParam_view)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showImage)).EndInit();
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel resultPanel;
        private System.Windows.Forms.RichTextBox requestResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox basePort;
        private System.Windows.Forms.TextBox baseIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox webName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView getParam_view;
        private System.Windows.Forms.TextBox postData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox requestMethod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn get_param;
        private System.Windows.Forms.DataGridViewTextBoxColumn get_value;
        private System.Windows.Forms.ComboBox urlList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox contentTypeCombo;
        private System.Windows.Forms.CheckBox enableAllowAutoRedirect;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox acceptCombo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox encodeList;
        private System.Windows.Forms.TextBox cookieBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button clearParam;
        private System.Windows.Forms.PictureBox showImage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox referText;
        private System.Windows.Forms.CheckBox fixedRefer;
        private System.Windows.Forms.Label log;
    }
}
