namespace KCSpy.View
{
    partial class FrmMain
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
            if(disposing && (components != null))
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.txtBeginID = new System.Windows.Forms.TextBox();
            this.txtEndID = new System.Windows.Forms.TextBox();
            this.btnSenka = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCurrCount = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.txtPageStart = new System.Windows.Forms.TextBox();
            this.txtPageEnd = new System.Windows.Forms.TextBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSaveData = new System.Windows.Forms.CheckBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSniff = new System.Windows.Forms.TabPage();
            this.dateTimer = new System.Windows.Forms.DateTimePicker();
            this.chkSetTime = new System.Windows.Forms.CheckBox();
            this.chkAsMonth = new System.Windows.Forms.CheckBox();
            this.chkAsYear = new System.Windows.Forms.CheckBox();
            this.rbtnRank = new System.Windows.Forms.RadioButton();
            this.rbtnNormal = new System.Windows.Forms.RadioButton();
            this.lblTokens = new System.Windows.Forms.Label();
            this.numTimes = new System.Windows.Forms.NumericUpDown();
            this.txtTextFile = new System.Windows.Forms.TextBox();
            this.chkTextFile = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtSniff = new Neetsonic.Control.TextBox();
            this.txtLog = new Neetsonic.Control.TextBox();
            this.tabSenka = new System.Windows.Forms.TabPage();
            this.btnServerAvailable = new System.Windows.Forms.Button();
            this.groupBox3 = new Neetsonic.Control.GroupBox();
            this.btnDownloadAll = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.txtSenka = new Neetsonic.Control.TextBox();
            this.txtReferer = new System.Windows.Forms.TextBox();
            this.groupBox2 = new Neetsonic.Control.GroupBox();
            this.btnAutoSeedFile = new System.Windows.Forms.Button();
            this.btnLoadSeedFile = new System.Windows.Forms.Button();
            this.btnOpenSeedFile = new System.Windows.Forms.Button();
            this.groupBox1 = new Neetsonic.Control.GroupBox();
            this.btnLoadServerFile = new System.Windows.Forms.Button();
            this.btnOpenServerFile = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabSniff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).BeginInit();
            this.tabSenka.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 79);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 20);
            label1.TabIndex = 3;
            label1.Text = "Token";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(9, 14);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 20);
            label3.TabIndex = 7;
            label3.Text = "起点ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(183, 14);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 20);
            label4.TabIndex = 9;
            label4.Text = "终点ID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(13, 39);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(51, 20);
            label5.TabIndex = 14;
            label5.Text = "服务器";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(204, 116);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(81, 20);
            label6.TabIndex = 18;
            label6.Text = "MemberID";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(9, 19);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(65, 20);
            label7.TabIndex = 20;
            label7.Text = "起始页码";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(119, 19);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 20);
            label8.TabIndex = 23;
            label8.Text = "终止页码";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 116);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 20);
            label2.TabIndex = 5;
            label2.Text = "Referer";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(8, 50);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(93, 20);
            label11.TabIndex = 36;
            label11.Text = "使用线程数量";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(10, 121);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(113, 20);
            label12.TabIndex = 41;
            label12.Text = "Excel表输出格式";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(7, 22);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(65, 20);
            label10.TabIndex = 29;
            label10.Text = "起始时间";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(7, 57);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(65, 20);
            label9.TabIndex = 31;
            label9.Text = "结束时间";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(14, 153);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 34);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_ClickAsync);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(299, 153);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 34);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(75, 76);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(297, 26);
            this.txtToken.TabIndex = 4;
            // 
            // txtBeginID
            // 
            this.txtBeginID.Location = new System.Drawing.Point(72, 11);
            this.txtBeginID.Name = "txtBeginID";
            this.txtBeginID.Size = new System.Drawing.Size(94, 26);
            this.txtBeginID.TabIndex = 8;
            // 
            // txtEndID
            // 
            this.txtEndID.Location = new System.Drawing.Point(246, 11);
            this.txtEndID.Name = "txtEndID";
            this.txtEndID.Size = new System.Drawing.Size(94, 26);
            this.txtEndID.TabIndex = 10;
            // 
            // btnSenka
            // 
            this.btnSenka.Location = new System.Drawing.Point(350, 10);
            this.btnSenka.Name = "btnSenka";
            this.btnSenka.Size = new System.Drawing.Size(70, 34);
            this.btnSenka.TabIndex = 11;
            this.btnSenka.Text = "战果";
            this.btnSenka.UseVisualStyleBackColor = true;
            this.btnSenka.Click += new System.EventHandler(this.BtnSenka_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(111, 153);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 34);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "中断";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // lblCurrCount
            // 
            this.lblCurrCount.AutoSize = true;
            this.lblCurrCount.Location = new System.Drawing.Point(383, 13);
            this.lblCurrCount.Name = "lblCurrCount";
            this.lblCurrCount.Size = new System.Drawing.Size(37, 20);
            this.lblCurrCount.TabIndex = 13;
            this.lblCurrCount.Text = "当前";
            // 
            // cmbServer
            // 
            this.cmbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(75, 36);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(297, 28);
            this.cmbServer.TabIndex = 15;
            this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.CmbServer_SelectedIndexChanged);
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(291, 113);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(81, 26);
            this.txtMemberID.TabIndex = 19;
            // 
            // txtPageStart
            // 
            this.txtPageStart.Location = new System.Drawing.Point(80, 16);
            this.txtPageStart.Name = "txtPageStart";
            this.txtPageStart.Size = new System.Drawing.Size(33, 26);
            this.txtPageStart.TabIndex = 21;
            // 
            // txtPageEnd
            // 
            this.txtPageEnd.Location = new System.Drawing.Point(190, 16);
            this.txtPageEnd.Name = "txtPageEnd";
            this.txtPageEnd.Size = new System.Drawing.Size(33, 26);
            this.txtPageEnd.TabIndex = 24;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(788, 25);
            this.menuMain.TabIndex = 25;
            this.menuMain.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConfig});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // tsmiConfig
            // 
            this.tsmiConfig.Name = "tsmiConfig";
            this.tsmiConfig.Size = new System.Drawing.Size(124, 22);
            this.tsmiConfig.Text = "修改配置";
            this.tsmiConfig.Click += new System.EventHandler(this.ConfigToolStripMenuItem_Click);
            // 
            // chkSaveData
            // 
            this.chkSaveData.AutoSize = true;
            this.chkSaveData.Location = new System.Drawing.Point(260, 16);
            this.chkSaveData.Name = "chkSaveData";
            this.chkSaveData.Size = new System.Drawing.Size(84, 24);
            this.chkSaveData.TabIndex = 26;
            this.chkSaveData.Text = "保存数据";
            this.chkSaveData.UseVisualStyleBackColor = true;
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabSniff);
            this.tabMain.Controls.Add(this.tabSenka);
            this.tabMain.Location = new System.Drawing.Point(0, 156);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(776, 369);
            this.tabMain.TabIndex = 35;
            // 
            // tabSniff
            // 
            this.tabSniff.BackColor = System.Drawing.SystemColors.Control;
            this.tabSniff.Controls.Add(this.dateTimer);
            this.tabSniff.Controls.Add(this.chkSetTime);
            this.tabSniff.Controls.Add(this.chkAsMonth);
            this.tabSniff.Controls.Add(this.chkAsYear);
            this.tabSniff.Controls.Add(this.rbtnRank);
            this.tabSniff.Controls.Add(this.rbtnNormal);
            this.tabSniff.Controls.Add(label12);
            this.tabSniff.Controls.Add(this.lblTokens);
            this.tabSniff.Controls.Add(this.numTimes);
            this.tabSniff.Controls.Add(label11);
            this.tabSniff.Controls.Add(this.txtTextFile);
            this.tabSniff.Controls.Add(this.chkTextFile);
            this.tabSniff.Controls.Add(this.btnExport);
            this.tabSniff.Controls.Add(this.btnTest);
            this.tabSniff.Controls.Add(this.txtSniff);
            this.tabSniff.Controls.Add(this.btnSelectAll);
            this.tabSniff.Controls.Add(label3);
            this.tabSniff.Controls.Add(this.txtBeginID);
            this.tabSniff.Controls.Add(label4);
            this.tabSniff.Controls.Add(this.txtLog);
            this.tabSniff.Controls.Add(this.txtEndID);
            this.tabSniff.Controls.Add(this.btnStop);
            this.tabSniff.Controls.Add(this.lblCurrCount);
            this.tabSniff.Location = new System.Drawing.Point(4, 4);
            this.tabSniff.Name = "tabSniff";
            this.tabSniff.Padding = new System.Windows.Forms.Padding(3);
            this.tabSniff.Size = new System.Drawing.Size(768, 336);
            this.tabSniff.TabIndex = 0;
            this.tabSniff.Text = "嗅探";
            // 
            // dateTimer
            // 
            this.dateTimer.Enabled = false;
            this.dateTimer.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimer.Location = new System.Drawing.Point(299, 48);
            this.dateTimer.Name = "dateTimer";
            this.dateTimer.Size = new System.Drawing.Size(75, 26);
            this.dateTimer.TabIndex = 47;
            // 
            // chkSetTime
            // 
            this.chkSetTime.AutoSize = true;
            this.chkSetTime.Location = new System.Drawing.Point(237, 50);
            this.chkSetTime.Name = "chkSetTime";
            this.chkSetTime.Size = new System.Drawing.Size(56, 24);
            this.chkSetTime.TabIndex = 46;
            this.chkSetTime.Text = "定时";
            this.chkSetTime.UseVisualStyleBackColor = true;
            this.chkSetTime.CheckedChanged += new System.EventHandler(this.ChkSetTime_CheckedChanged);
            // 
            // chkAsMonth
            // 
            this.chkAsMonth.AutoSize = true;
            this.chkAsMonth.Location = new System.Drawing.Point(619, 12);
            this.chkAsMonth.Name = "chkAsMonth";
            this.chkAsMonth.Size = new System.Drawing.Size(84, 24);
            this.chkAsMonth.TabIndex = 45;
            this.chkAsMonth.Text = "月度数据";
            this.chkAsMonth.UseVisualStyleBackColor = true;
            // 
            // chkAsYear
            // 
            this.chkAsYear.AutoSize = true;
            this.chkAsYear.Location = new System.Drawing.Point(529, 12);
            this.chkAsYear.Name = "chkAsYear";
            this.chkAsYear.Size = new System.Drawing.Size(84, 24);
            this.chkAsYear.TabIndex = 44;
            this.chkAsYear.Text = "年度数据";
            this.chkAsYear.UseVisualStyleBackColor = true;
            // 
            // rbtnRank
            // 
            this.rbtnRank.AutoSize = true;
            this.rbtnRank.Location = new System.Drawing.Point(218, 119);
            this.rbtnRank.Name = "rbtnRank";
            this.rbtnRank.Size = new System.Drawing.Size(111, 24);
            this.rbtnRank.TabIndex = 43;
            this.rbtnRank.Text = "天下统一模版";
            this.rbtnRank.UseVisualStyleBackColor = true;
            // 
            // rbtnNormal
            // 
            this.rbtnNormal.AutoSize = true;
            this.rbtnNormal.Checked = true;
            this.rbtnNormal.Location = new System.Drawing.Point(129, 119);
            this.rbtnNormal.Name = "rbtnNormal";
            this.rbtnNormal.Size = new System.Drawing.Size(83, 24);
            this.rbtnNormal.TabIndex = 42;
            this.rbtnNormal.TabStop = true;
            this.rbtnNormal.Text = "普通模版";
            this.rbtnNormal.UseVisualStyleBackColor = true;
            // 
            // lblTokens
            // 
            this.lblTokens.AutoSize = true;
            this.lblTokens.Location = new System.Drawing.Point(166, 50);
            this.lblTokens.Name = "lblTokens";
            this.lblTokens.Size = new System.Drawing.Size(0, 20);
            this.lblTokens.TabIndex = 40;
            // 
            // numTimes
            // 
            this.numTimes.Location = new System.Drawing.Point(107, 48);
            this.numTimes.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimes.Name = "numTimes";
            this.numTimes.Size = new System.Drawing.Size(46, 26);
            this.numTimes.TabIndex = 39;
            this.numTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtTextFile
            // 
            this.txtTextFile.Location = new System.Drawing.Point(88, 80);
            this.txtTextFile.Name = "txtTextFile";
            this.txtTextFile.ReadOnly = true;
            this.txtTextFile.Size = new System.Drawing.Size(286, 26);
            this.txtTextFile.TabIndex = 38;
            // 
            // chkTextFile
            // 
            this.chkTextFile.AutoSize = true;
            this.chkTextFile.Location = new System.Drawing.Point(12, 81);
            this.chkTextFile.Name = "chkTextFile";
            this.chkTextFile.Size = new System.Drawing.Size(70, 24);
            this.chkTextFile.TabIndex = 37;
            this.chkTextFile.Text = "从文本";
            this.chkTextFile.UseVisualStyleBackColor = true;
            this.chkTextFile.CheckedChanged += new System.EventHandler(this.ChkFromFile_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(206, 153);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 34);
            this.btnExport.TabIndex = 36;
            this.btnExport.Text = "导出到表";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // txtSniff
            // 
            this.txtSniff.AcceptsReturn = true;
            this.txtSniff.Location = new System.Drawing.Point(387, 38);
            this.txtSniff.MaxLength = 3276700;
            this.txtSniff.Multiline = true;
            this.txtSniff.Name = "txtSniff";
            this.txtSniff.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSniff.Size = new System.Drawing.Size(375, 289);
            this.txtSniff.TabIndex = 1;
            this.txtSniff.WordWrap = false;
            // 
            // txtLog
            // 
            this.txtLog.AcceptsReturn = true;
            this.txtLog.Location = new System.Drawing.Point(12, 200);
            this.txtLog.MaxLength = 3276700;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(367, 127);
            this.txtLog.TabIndex = 22;
            this.txtLog.WordWrap = false;
            // 
            // tabSenka
            // 
            this.tabSenka.BackColor = System.Drawing.SystemColors.Control;
            this.tabSenka.Controls.Add(this.btnServerAvailable);
            this.tabSenka.Controls.Add(label7);
            this.tabSenka.Controls.Add(this.txtPageStart);
            this.tabSenka.Controls.Add(label8);
            this.tabSenka.Controls.Add(this.txtPageEnd);
            this.tabSenka.Controls.Add(this.chkSaveData);
            this.tabSenka.Controls.Add(this.btnSenka);
            this.tabSenka.Controls.Add(this.groupBox3);
            this.tabSenka.Controls.Add(this.txtSenka);
            this.tabSenka.Location = new System.Drawing.Point(4, 4);
            this.tabSenka.Name = "tabSenka";
            this.tabSenka.Padding = new System.Windows.Forms.Padding(3);
            this.tabSenka.Size = new System.Drawing.Size(768, 336);
            this.tabSenka.TabIndex = 1;
            this.tabSenka.Text = "战果";
            // 
            // btnServerAvailable
            // 
            this.btnServerAvailable.Location = new System.Drawing.Point(444, 176);
            this.btnServerAvailable.Name = "btnServerAvailable";
            this.btnServerAvailable.Size = new System.Drawing.Size(118, 34);
            this.btnServerAvailable.TabIndex = 33;
            this.btnServerAvailable.Text = "可注册服务器";
            this.btnServerAvailable.UseVisualStyleBackColor = true;
            this.btnServerAvailable.Click += new System.EventHandler(this.BtnServerAvelable_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDownloadAll);
            this.groupBox3.Controls.Add(label10);
            this.groupBox3.Controls.Add(this.btnDownload);
            this.groupBox3.Controls.Add(label9);
            this.groupBox3.Controls.Add(this.dateStart);
            this.groupBox3.Controls.Add(this.dateEnd);
            this.groupBox3.Location = new System.Drawing.Point(444, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 138);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "人事表下载";
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.Location = new System.Drawing.Point(82, 96);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.Size = new System.Drawing.Size(92, 31);
            this.btnDownloadAll.TabIndex = 34;
            this.btnDownloadAll.Text = "下载全部";
            this.btnDownloadAll.UseVisualStyleBackColor = true;
            this.btnDownloadAll.Click += new System.EventHandler(this.BtnDownloadAll_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(11, 96);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(65, 31);
            this.btnDownload.TabIndex = 33;
            this.btnDownload.Text = "下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // dateStart
            // 
            this.dateStart.CustomFormat = "yyyy/MM";
            this.dateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateStart.Location = new System.Drawing.Point(78, 19);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(96, 26);
            this.dateStart.TabIndex = 28;
            this.dateStart.Value = new System.DateTime(2013, 7, 1, 15, 47, 0, 0);
            // 
            // dateEnd
            // 
            this.dateEnd.Checked = false;
            this.dateEnd.CustomFormat = "yyyy/MM";
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEnd.Location = new System.Drawing.Point(78, 54);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(96, 26);
            this.dateEnd.TabIndex = 30;
            // 
            // txtSenka
            // 
            this.txtSenka.AcceptsReturn = true;
            this.txtSenka.Location = new System.Drawing.Point(13, 60);
            this.txtSenka.MaxLength = 3276700;
            this.txtSenka.Multiline = true;
            this.txtSenka.Name = "txtSenka";
            this.txtSenka.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSenka.Size = new System.Drawing.Size(407, 270);
            this.txtSenka.TabIndex = 27;
            this.txtSenka.WordWrap = false;
            // 
            // txtReferer
            // 
            this.txtReferer.Location = new System.Drawing.Point(75, 113);
            this.txtReferer.Name = "txtReferer";
            this.txtReferer.Size = new System.Drawing.Size(114, 26);
            this.txtReferer.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAutoSeedFile);
            this.groupBox2.Controls.Add(this.btnLoadSeedFile);
            this.groupBox2.Controls.Add(this.btnOpenSeedFile);
            this.groupBox2.Location = new System.Drawing.Point(543, 53);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 70);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "API种子文件";
            // 
            // btnAutoSeedFile
            // 
            this.btnAutoSeedFile.Location = new System.Drawing.Point(147, 25);
            this.btnAutoSeedFile.Name = "btnAutoSeedFile";
            this.btnAutoSeedFile.Size = new System.Drawing.Size(80, 34);
            this.btnAutoSeedFile.TabIndex = 31;
            this.btnAutoSeedFile.Text = "自动获取";
            this.btnAutoSeedFile.UseVisualStyleBackColor = true;
            this.btnAutoSeedFile.Click += new System.EventHandler(this.BtnAutoSeedFile_Click);
            // 
            // btnLoadSeedFile
            // 
            this.btnLoadSeedFile.Location = new System.Drawing.Point(63, 25);
            this.btnLoadSeedFile.Name = "btnLoadSeedFile";
            this.btnLoadSeedFile.Size = new System.Drawing.Size(80, 34);
            this.btnLoadSeedFile.TabIndex = 30;
            this.btnLoadSeedFile.Text = "重新载入";
            this.btnLoadSeedFile.UseVisualStyleBackColor = true;
            this.btnLoadSeedFile.Click += new System.EventHandler(this.BtnLoadSeedFile_Click);
            // 
            // btnOpenSeedFile
            // 
            this.btnOpenSeedFile.Location = new System.Drawing.Point(6, 25);
            this.btnOpenSeedFile.Name = "btnOpenSeedFile";
            this.btnOpenSeedFile.Size = new System.Drawing.Size(51, 34);
            this.btnOpenSeedFile.TabIndex = 29;
            this.btnOpenSeedFile.Text = "打开";
            this.btnOpenSeedFile.UseVisualStyleBackColor = true;
            this.btnOpenSeedFile.Click += new System.EventHandler(this.BtnOpenSeedFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadServerFile);
            this.groupBox1.Controls.Add(this.btnOpenServerFile);
            this.groupBox1.Location = new System.Drawing.Point(385, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 70);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器配置文件";
            // 
            // btnLoadServerFile
            // 
            this.btnLoadServerFile.Location = new System.Drawing.Point(63, 25);
            this.btnLoadServerFile.Name = "btnLoadServerFile";
            this.btnLoadServerFile.Size = new System.Drawing.Size(80, 34);
            this.btnLoadServerFile.TabIndex = 30;
            this.btnLoadServerFile.Text = "重新载入";
            this.btnLoadServerFile.UseVisualStyleBackColor = true;
            this.btnLoadServerFile.Click += new System.EventHandler(this.BtnLoadServerFile_Click);
            // 
            // btnOpenServerFile
            // 
            this.btnOpenServerFile.Location = new System.Drawing.Point(6, 25);
            this.btnOpenServerFile.Name = "btnOpenServerFile";
            this.btnOpenServerFile.Size = new System.Drawing.Size(51, 34);
            this.btnOpenServerFile.TabIndex = 29;
            this.btnOpenServerFile.Text = "打开";
            this.btnOpenServerFile.UseVisualStyleBackColor = true;
            this.btnOpenServerFile.Click += new System.EventHandler(this.BtnOpenServerFile_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 533);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(label1);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(label5);
            this.Controls.Add(label2);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.txtReferer);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KCSpy";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabSniff.ResumeLayout(false);
            this.tabSniff.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).EndInit();
            this.tabSenka.ResumeLayout(false);
            this.tabSenka.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private Neetsonic.Control.TextBox txtSniff;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.TextBox txtBeginID;
        private System.Windows.Forms.TextBox txtEndID;
        private System.Windows.Forms.Button btnSenka;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCurrCount;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.TextBox txtPageStart;
        private Neetsonic.Control.TextBox txtLog;
        private System.Windows.Forms.TextBox txtPageEnd;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.CheckBox chkSaveData;
        private System.Windows.Forms.Button btnOpenServerFile;
        private Neetsonic.Control.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoadServerFile;
        private Neetsonic.Control.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAutoSeedFile;
        private System.Windows.Forms.Button btnLoadSeedFile;
        private System.Windows.Forms.Button btnOpenSeedFile;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabSniff;
        private System.Windows.Forms.TabPage tabSenka;
        private Neetsonic.Control.TextBox txtSenka;
        private Neetsonic.Control.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Button btnDownloadAll;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnServerAvailable;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtTextFile;
        private System.Windows.Forms.CheckBox chkTextFile;
        private System.Windows.Forms.Label lblTokens;
        private System.Windows.Forms.NumericUpDown numTimes;
        private System.Windows.Forms.TextBox txtReferer;
        private System.Windows.Forms.RadioButton rbtnRank;
        private System.Windows.Forms.RadioButton rbtnNormal;
        private System.Windows.Forms.CheckBox chkAsMonth;
        private System.Windows.Forms.CheckBox chkAsYear;
        private System.Windows.Forms.DateTimePicker dateTimer;
        private System.Windows.Forms.CheckBox chkSetTime;
    }
}