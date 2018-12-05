using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KCSpy.Model;
using KCSpy.Util;
using Neetsonic.Tool;
using Neetsonic.Tool.Extensions;

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
        public FrmMain(string[] cmdArgs)
        {
            _cmdArgs = cmdArgs;
            InitializeComponent();
        }
        private readonly string[] _cmdArgs;
        private bool _firstFromCmd;

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
        private void BtnLoadSeedFile_Click(object sender, EventArgs e) => Spy.LoadKeySeed();
        private void BtnLoadServerFile_Click(object sender, EventArgs e)
        {
            Spy.LoadServer();
            SetServer();
        }
        private void BtnOpenExcel_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtExcelFile.Text)) { Process.Start(txtExcelFile.Text); }
        }
        private void BtnOpenSeedFile_Click(object sender, EventArgs e) => Spy.OpenSeedFile();
        private void BtnOpenServerFile_Click(object sender, EventArgs e) => Spy.OpenServerFile();
        private void BtnSelectAll_Click(object sender, EventArgs e) => txtSniff.Highlight();
        private async void BtnSenka_Click(object sender, EventArgs e)
        {
            await Spy.RequestSenka(cmbServer.SelectedItem as Server, int.Parse(txtPageStart.Text), int.Parse(txtPageEnd.Text), SenkaReport);
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void BtnStop_Click(object sender, EventArgs e) => Spy.StopRequest();
        private async void BtnTest_ClickAsync(object sender, EventArgs e)
        {
            if(txtSniff.Text.Length > 0 && DialogResult.OK == MessageBoxEx.Confirm(@"是否清空当前已有文本？")) txtSniff.Clear();
            bool fromExcel = chkExcel.Checked;
            bool autoServer = chkAutoServer.Checked;
            string filePath = txtExcelFile.Text;
            Server server = (Server)cmbServer.SelectedItem;
            if(fromExcel)
            {
                if(autoServer)
                {
                    await Spy.RequestPlayerInfo(filePath, SniffCurr, SniffReport, SniffLog);
                }
                else
                {
                    await Spy.RequestPlayerInfo(server, filePath, SniffCurr, SniffReport, SniffLog);
                }
                string fileName = Path.GetFileName(txtExcelFile.Text);
                string newName;
                if(null != fileName)
                {
                    int start = fileName.LastIndexOf('(');
                    if(start > 0)
                    {
                        int end = fileName.LastIndexOf(')');
                        string oldDate = fileName.Substring(start + 1, end - start - 1);
                        newName = fileName.Replace(oldDate, string.Format($@"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}"));
                    }
                    else { newName = fileName.Insert(fileName.LastIndexOf('.'), string.Format($@"({DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day})")); }
                    BeginInvoke(new MethodInvoker(() => txtExcelFile.Text = FileTool.Rename(filePath, newName)));
                }
            }
            else
            {
                if(chkMultiThreads.Checked)
                {
                    int startID = int.Parse(txtBeginID.Text), endID = int.Parse(txtEndID.Text), step = (int)Math.Ceiling((endID - startID + 1) / (double)server.Tokens.Count());
                    List<Task> tasks = new List<Task>();
                    foreach(string token in server.Tokens)
                    {
                        tasks.Add(Spy.RequestPlayerInfo(server, startID, startID + step - 1, SniffCurr, SniffReport, SniffLog));
                        startID += step;
                    }
                    await Task.WhenAll(tasks);
                }
                else
                {
                    int startID = int.Parse(txtBeginID.Text), endID = int.Parse(txtEndID.Text);
                    await Spy.RequestPlayerInfo(server, startID, endID, SniffCurr, SniffReport, SniffLog);
                }
            }
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void ChkExcel_CheckedChanged(object sender, EventArgs e)
        {
            if(_firstFromCmd)
            {
                _firstFromCmd = false;
                return;
            }
            if(chkExcel.Checked)
            {
                OpenFileDialog dlg = new OpenFileDialog {Filter = @"Excel文件|*.xlsx;"};
                if(DialogResult.OK == dlg.ShowDialog()) { txtExcelFile.Text = dlg.FileName; }
                else { chkExcel.Checked = false; }
            }
            else { txtExcelFile.Clear(); }
        }
        private void CmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Server server = cmbServer.SelectedItem as Server;
            txtToken.Text = server?.Token;
            txtMemberID.Text = server?.MemberID;
            chkMultiThreads.Enabled = null != server?.TokenString && server.Tokens.Any();
            if(!chkMultiThreads.Enabled) { chkMultiThreads.Checked = false; }
        }
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e) => new FrmConfig().ShowDialog();
        private void FrmMain_Load(object sender, EventArgs e)
        {
            SetServer();
            if(_cmdArgs.Length > 0)
            {
                chkExcel.Checked = _firstFromCmd = true;
                txtExcelFile.Text = _cmdArgs[0];
            }
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
        private async void BtnServerAvelable_Click(object sender, EventArgs e)
        {
            await Spy.RequestServerAvailable(SenkaReport);
            MessageBoxEx.Info(@"任务执行完成！");
        }
    }
}