using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KCSpy.Model;
using KCSpy.Util;
using Neetsonic.Tool;
using Neetsonic.Tool.Extensions;
using ProcessType = KCSpy.Util.Spy.ProcessType;

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void BtnAutoSeedFile_Click(object sender, EventArgs e)
        {
            await Task.Run(() => Spy.AutoRefreshSeed());
            MessageBoxEx.Info(@"更新完毕，已重新载入！");
        }
        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            Server server = cmbServer.SelectedItem as Server;
            DateTime currDate = new DateTime(dateStart.Value.Year, dateStart.Value.Month, 1);
            DateTime endDate = new DateTime(dateEnd.Value.Year, dateEnd.Value.Month, 1);
            await Spy.DownloadSenkaPic(server, currDate, endDate);
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private async void BtnDownloadAll_Click(object sender, EventArgs e)
        {
            DateTime currDate = new DateTime(dateStart.Value.Year, dateStart.Value.Month, 1);
            DateTime endDate = new DateTime(dateEnd.Value.Year, dateEnd.Value.Month, 1);
            await Spy.DownloadAllSenkaPic(currDate, endDate);
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private async void BtnExport_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, txtSniff.Text);
            await Spy.ExportClipboardToExcel(Path.Combine(Global.Config.ExcelPath, rbtnNormal.Checked ? @"普通模版.xlsx" : @"天下统一模版.xlsx"));
            Clipboard.Clear();
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void BtnLoadSeedFile_Click(object sender, EventArgs e) => Spy.LoadKeySeed();
        private void BtnLoadServerFile_Click(object sender, EventArgs e)
        {
            Spy.LoadServer();
            SetServer();
        }
        private void BtnOpenSeedFile_Click(object sender, EventArgs e) => Spy.OpenSeedFile();
        private void BtnOpenServerFile_Click(object sender, EventArgs e) => Spy.OpenServerFile();
        private void BtnSelectAll_Click(object sender, EventArgs e) => txtSniff.Highlight();
        private async void BtnSenka_Click(object sender, EventArgs e)
        {
            await Spy.RequestSenka(cmbServer.SelectedItem as Server, int.Parse(txtPageStart.Text), int.Parse(txtPageEnd.Text), SenkaReport);
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private async void BtnServerAvelable_Click(object sender, EventArgs e)
        {
            await Spy.RequestServerAvailable(SenkaReport);
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void BtnStop_Click(object sender, EventArgs e) => Spy.StopRequest();
        private async void BtnTest_ClickAsync(object sender, EventArgs e)
        {
            if(chkSetTime.Checked)
            {
                SniffLog(string.Format($@"等待至[{dateTimer.Value.ToString(CultureInfo.CurrentCulture)}]执行..."));
                TimeSpan sleepSeconds = dateTimer.Value - DateTime.Now;
                await Task.Run(() =>
                {
                    Thread.Sleep(sleepSeconds);
                });
            }
            if(txtSniff.Text.Length > 0 && DialogResult.OK == MessageBoxEx.Confirm(@"是否清空当前已有文本？")) txtSniff.Clear();
            bool fromTextFile = chkTextFile.Checked;
            string textFilePath = txtTextFile.Text;
            txtSniff.Visible = false;
            Server server = (Server)cmbServer.SelectedItem;
            if(fromTextFile)
            {
                ProcessType processType = ProcessType.Normal;
                if(chkAsYear.Checked) {processType = ProcessType.Year;}
                else if(chkAsMonth.Checked) { processType = ProcessType.Month;}
                await Spy.RequestPlayerInfoTextFile(server, textFilePath, (int)numTimes.Value, SniffCurr, SniffReport, SniffLog, processType);
            }
            else
            {
                int startID = int.Parse(txtBeginID.Text), endID = int.Parse(txtEndID.Text);
                await Spy.RequestPlayerInfo(server, startID, endID, (int)numTimes.Value, SniffCurr, SniffReport, SniffLog);
            }
            MessageBoxEx.Info(@"任务执行完成！");
            txtSniff.Visible = true;
        }
        private void ChkFromFile_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTextFile.Checked)
            {
                OpenFileDialog dlg = new OpenFileDialog {Filter = @"文本文件|*.txt;"};
                if(DialogResult.OK == dlg.ShowDialog()) { txtTextFile.Text = dlg.FileName; }
                else { chkTextFile.Checked = false; }
            }
            else
            {
                txtTextFile.Clear();
            }
        }
        private void CmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Server server = cmbServer.SelectedItem as Server;
            txtToken.Text = server?.Token;
            txtMemberID.Text = server?.MemberID;
            lblTokens.Text = null != server?.TokenString && server.Tokens.Any() ? string.Format($@"({server.Tokens.Count()})个Token") : @"1个Token";
        }
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e) => new FrmConfig().ShowDialog();
        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetServer();
        }
        private void SenkaReport(string txt) => BeginInvoke(new MethodInvoker(() => txtSenka.AppendLine(txt)));
        private void SetServer()
        {
            cmbServer.DataSource = Spy.Servers;
            cmbServer.DisplayMember = @"Name";
            cmbServer.ValueMember = @"IP";
        }
        private void SniffCurr(string content) => BeginInvoke(new MethodInvoker(() => lblCurrCount.Text = content));
        private void SniffLog(string txt) => BeginInvoke(new MethodInvoker(() => txtLog.AppendLine(txt)));
        private void SniffReport(string txt) => BeginInvoke(new MethodInvoker(() => txtSniff.AppendLine(txt)));

        private void ChkSetTime_CheckedChanged(object sender, EventArgs e) => dateTimer.Enabled = chkSetTime.Checked;
    }
}