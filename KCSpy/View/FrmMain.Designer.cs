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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnTest = new System.Windows.Forms.Button();
            this.txtContent = new Neetsonic.Control.TextBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.txtReferer = new System.Windows.Forms.TextBox();
            this.txtBeginID = new System.Windows.Forms.TextBox();
            this.txtEndID = new System.Windows.Forms.TextBox();
            this.btnSenka = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCurrCount = new System.Windows.Forms.Label();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.chkFile = new System.Windows.Forms.CheckBox();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.txtPageStart = new System.Windows.Forms.TextBox();
            this.txtSenka = new Neetsonic.Control.TextBox();
            this.txtPageEnd = new System.Windows.Forms.TextBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.chkSaveData = new System.Windows.Forms.CheckBox();
            this.textBox1 = new Neetsonic.Control.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 73);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 20);
            label1.TabIndex = 3;
            label1.Text = "Token";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 110);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 20);
            label2.TabIndex = 5;
            label2.Text = "Referer";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 155);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 20);
            label3.TabIndex = 7;
            label3.Text = "起点ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(186, 155);
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
            label6.Location = new System.Drawing.Point(389, 39);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(81, 20);
            label6.TabIndex = 18;
            label6.Text = "MemberID";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(571, 39);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(65, 20);
            label7.TabIndex = 20;
            label7.Text = "起始页码";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(681, 39);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 20);
            label8.TabIndex = 23;
            label8.Text = "终止页码";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 230);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 34);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_ClickAsync);
            // 
            // txtContent
            // 
            this.txtContent.AcceptsReturn = true;
            this.txtContent.Location = new System.Drawing.Point(12, 277);
            this.txtContent.MaxLength = 3276700;
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(360, 160);
            this.txtContent.TabIndex = 1;
            this.txtContent.WordWrap = false;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(297, 230);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 34);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(75, 70);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(297, 26);
            this.txtToken.TabIndex = 4;
            // 
            // txtReferer
            // 
            this.txtReferer.Location = new System.Drawing.Point(75, 107);
            this.txtReferer.Name = "txtReferer";
            this.txtReferer.Size = new System.Drawing.Size(297, 26);
            this.txtReferer.TabIndex = 6;
            // 
            // txtBeginID
            // 
            this.txtBeginID.Location = new System.Drawing.Point(75, 152);
            this.txtBeginID.Name = "txtBeginID";
            this.txtBeginID.Size = new System.Drawing.Size(94, 26);
            this.txtBeginID.TabIndex = 8;
            // 
            // txtEndID
            // 
            this.txtEndID.Location = new System.Drawing.Point(249, 152);
            this.txtEndID.Name = "txtEndID";
            this.txtEndID.Size = new System.Drawing.Size(94, 26);
            this.txtEndID.TabIndex = 10;
            // 
            // btnSenka
            // 
            this.btnSenka.Location = new System.Drawing.Point(393, 230);
            this.btnSenka.Name = "btnSenka";
            this.btnSenka.Size = new System.Drawing.Size(70, 34);
            this.btnSenka.TabIndex = 11;
            this.btnSenka.Text = "战果";
            this.btnSenka.UseVisualStyleBackColor = true;
            this.btnSenka.Click += new System.EventHandler(this.BtnSenka_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(109, 230);
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
            this.lblCurrCount.Location = new System.Drawing.Point(8, 449);
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
            // 
            // chkFile
            // 
            this.chkFile.AutoSize = true;
            this.chkFile.Location = new System.Drawing.Point(17, 193);
            this.chkFile.Name = "chkFile";
            this.chkFile.Size = new System.Drawing.Size(113, 24);
            this.chkFile.TabIndex = 16;
            this.chkFile.Text = "从文件读取ID";
            this.chkFile.UseVisualStyleBackColor = true;
            this.chkFile.CheckedChanged += new System.EventHandler(this.ChkFile_CheckedChanged);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(136, 191);
            this.txtFile.Name = "txtFile";
            this.txtFile.ReadOnly = true;
            this.txtFile.Size = new System.Drawing.Size(236, 26);
            this.txtFile.TabIndex = 17;
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(476, 36);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(89, 26);
            this.txtMemberID.TabIndex = 19;
            // 
            // txtPageStart
            // 
            this.txtPageStart.Location = new System.Drawing.Point(642, 36);
            this.txtPageStart.Name = "txtPageStart";
            this.txtPageStart.Size = new System.Drawing.Size(33, 26);
            this.txtPageStart.TabIndex = 21;
            // 
            // txtSenka
            // 
            this.txtSenka.AcceptsReturn = true;
            this.txtSenka.Location = new System.Drawing.Point(393, 277);
            this.txtSenka.MaxLength = 3276700;
            this.txtSenka.Multiline = true;
            this.txtSenka.Name = "txtSenka";
            this.txtSenka.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSenka.Size = new System.Drawing.Size(392, 160);
            this.txtSenka.TabIndex = 22;
            this.txtSenka.WordWrap = false;
            // 
            // txtPageEnd
            // 
            this.txtPageEnd.Location = new System.Drawing.Point(752, 36);
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
            this.menuMain.Size = new System.Drawing.Size(796, 25);
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
            this.chkSaveData.Location = new System.Drawing.Point(393, 193);
            this.chkSaveData.Name = "chkSaveData";
            this.chkSaveData.Size = new System.Drawing.Size(84, 24);
            this.chkSaveData.TabIndex = 26;
            this.chkSaveData.Text = "保存数据";
            this.chkSaveData.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Location = new System.Drawing.Point(393, 73);
            this.textBox1.MaxLength = 3276700;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(392, 107);
            this.textBox1.TabIndex = 27;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(714, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 34);
            this.button1.TabIndex = 28;
            this.button1.Text = "战果";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 477);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkSaveData);
            this.Controls.Add(this.txtPageEnd);
            this.Controls.Add(label8);
            this.Controls.Add(this.txtSenka);
            this.Controls.Add(this.txtPageStart);
            this.Controls.Add(label7);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(label6);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.chkFile);
            this.Controls.Add(this.cmbServer);
            this.Controls.Add(label5);
            this.Controls.Add(this.lblCurrCount);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSenka);
            this.Controls.Add(this.txtEndID);
            this.Controls.Add(label4);
            this.Controls.Add(this.txtBeginID);
            this.Controls.Add(label3);
            this.Controls.Add(this.txtReferer);
            this.Controls.Add(label2);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(label1);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.menuMain);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private Neetsonic.Control.TextBox txtContent;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.TextBox txtReferer;
        private System.Windows.Forms.TextBox txtBeginID;
        private System.Windows.Forms.TextBox txtEndID;
        private System.Windows.Forms.Button btnSenka;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCurrCount;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.CheckBox chkFile;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.TextBox txtPageStart;
        private Neetsonic.Control.TextBox txtSenka;
        private System.Windows.Forms.TextBox txtPageEnd;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiConfig;
        private System.Windows.Forms.CheckBox chkSaveData;
        private Neetsonic.Control.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}