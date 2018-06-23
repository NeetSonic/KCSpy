using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using KCSpy.Model;
using Microsoft.Office.Interop.Excel;
using Neetsonic.Tool;
using Neetsonic.Tool.Extensions;
using Newtonsoft.Json;
using SenkaGo.Util;
using Application = System.Windows.Forms.Application;
using Label = System.Windows.Forms.Label;
using TextBox = Neetsonic.Control.TextBox;

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
        private static readonly string ServerFilePath = Path.Combine(Application.StartupPath, @"server.xml");
        private static bool Stop;
        private static List<Server> Servers = new List<Server>
        {
                new Server {Name = @"大凑", IP = @"203.104.209.150", Token = @"08b1c06a0ef50c378c45aa934542dbb91cb168b3"},
                new Server {Name = @"特鲁克", IP = @"203.104.209.134", Token = @"b851a4506eb04c81a4964d8734efe312f4902b53"},
                new Server {Name = @"林加", IP = @"203.104.209.167", Token = @"b22eac020acbda0feeb35e9cdd2d3a59febb0dfa"},
                new Server {Name = @"拉包尔", IP = @"203.104.248.135", Token = @"7536f33270f6a27fe3f23f3eb3ee2a4193b00253"},
                new Server {Name = @"肖特兰", IP = @"125.6.189.7", Token = @"2b77a433c6b9097b5057579db887cc1b08f0ae12"},
                new Server {Name = @"布因", IP = @"125.6.189.39", Token = @"552a7337c8682d22497c7dd16c46f28ef3420a9f"},
                new Server {Name = @"塔威", IP = @"125.6.189.71", Token = @"67b819daa43ca426811d73f39768944624accccd"},
                new Server {Name = @"帕劳", IP = @"125.6.189.103", Token = @"a09a6d289c8b67f66bf376adf9a619d1918191ff"},
                new Server {Name = @"文莱", IP = @"125.6.189.135", Token = @"6e02f318a51b23caa3f207b72e5de2f9e7304a84"},
                new Server {Name = @"单冠湾", IP = @"125.6.189.167", Token = @"72ce0498c052577a37260df7404bb0469e153feb"},
                new Server {Name = @"幌筵", IP = @"125.6.189.215", Token = @"2cabb9b4735d85559bdd4e1437ae1d91c2fdf397"},
                new Server {Name = @"宿毛湾", IP = @"125.6.189.247", Token = @"075a10f621670e394ef2b2d2075119c243d6ea97"},
                new Server {Name = @"鹿屋", IP = @"203.104.209.23", Token = @"9650cff46860f6dc6c955b910b1d950383f28254"},
                new Server {Name = @"岩川", IP = @"203.104.209.39", Token = @"682289b443bc984a5425b9bee5c234d16acfb823"},
                new Server {Name = @"佐伯湾", IP = @"203.104.209.55", Token = @"4895eb1d78a12b7fbd85417daf2634bf5fb2d8a1"},
                new Server {Name = @"柱島", IP = @"203.104.209.102", Token = @"b508c17e7e4e2a912b22b9a888cf60b2ab869e00"}
        };

        public FrmMain()
        {
            InitializeComponent();
        }
        private static Server AutoServer(string id)
        {
            int ID = int.Parse(id);
            if(ID < 500000) return Servers[0];
            if(ID < 600000) return Servers[1];
            if(ID < 700000) return Servers[2];
            if(ID < 800000) return Servers[3];
            if(ID < 900000) return Servers[4];
            if(ID < 6000000) return Servers[0];
            if(ID < 7000000) return Servers[1];
            if(ID < 8000000) return Servers[2];
            if(ID < 9000000) return Servers[3];
            if(ID < 10000000) return Servers[4];
            if(ID < 11000000) return Servers[5];
            if(ID < 12000000) return Servers[6];
            if(ID < 13000000) return Servers[7];
            if(ID < 14000000) return Servers[8];
            if(ID < 15000000) return Servers[9];
            if(ID < 16000000) return Servers[10];
            if(ID < 17000000) return Servers[11];
            if(ID < 18000000) return Servers[12];
            if(ID < 19000000) return Servers[13];
            if(ID < 20000000) return Servers[14];
            return ID < 21000000 ? Servers[15] : null;
        }
        private static void GenerateServerFile()
        {
            XmlSerializer serializer = new XmlSerializer(Servers.GetType());
            using(TextWriter writer = new StreamWriter(ServerFilePath))
            {
                serializer.Serialize(writer, Servers);
                writer.Close();
            }
        }
        private static void ReadServerFile()
        {
            XmlSerializer serializer = new XmlSerializer(Servers.GetType());
            using(XmlReader reader = XmlReader.Create(ServerFilePath))
            {
                Servers = (List<Server>)serializer.Deserialize(reader);
                reader.Close();
            }
        }
        private static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            PropertyInfo property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if(property != null)
            {
                if(property.GetValue(header, null) is NameValueCollection collection)
                    collection[name] = value;
            }
        }
        private void AppendLineAsnyc(TextBox txtCtrl, string content) => BeginInvoke(new MethodInvoker(() => txtCtrl.AppendLine(content)));
        private void BtnLoadServerFile_Click(object sender, EventArgs e)
        {
            if(File.Exists(ServerFilePath))
            {
                ReadServerFile();
                SetServer();
            }
        }
        private void BtnOpenServerFile_Click(object sender, EventArgs e) => FileTool.OpenTextFile(ServerFilePath);
        private void BtnSelectAll_Click(object sender, EventArgs e) => txtContent.Highlight();
        private async void BtnSenka_Click(object sender, EventArgs e)
        {
            string IP = cmbServer.SelectedValue.ToString();
            await Task.Run(() =>
            {
                int startPage = int.Parse(txtPageStart.Text);
                int endPage = int.Parse(txtPageEnd.Text);
                for(int currPage = startPage; currPage <= endPage; currPage++)
                {
                    string postData = string.Format($@"api_pageno={currPage}&api_verno=1&api_token={txtToken.Text}&api_ranking={KeyGen.CreateSignature(int.Parse(txtMemberID.Text))}");
                    byte[] data = Encoding.UTF8.GetBytes(postData);
                    string ret = PostSenka(IP, data);
                    List<SenkaItem> items = JsonConvert.DeserializeObject<SenkaCover>(ret.Substring(7)).api_data.api_list;
                    foreach(SenkaItem item in items)
                    {
                        Dictionary<string, double> kit = KeyGen.DecodeRankAndMedal(int.Parse(txtMemberID.Text), item.api_mxltvkpyuklh, item.api_wuhnhojjxmke, item.api_itslcqtmrxtf);
                        AppendLineAsnyc(txtSenka, string.Format($@"提督：{item.api_mtjmdcwtvhdr}  顺位：{item.api_mxltvkpyuklh}  战果值：{kit[@"rate"]}  甲章数：{kit[@"medal"]}"));
                    }
                }
            });
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void BtnStop_Click(object sender, EventArgs e) => Stop = true;
        private async void BtnTest_ClickAsync(object sender, EventArgs e)
        {
            if(txtContent.Text.Length > 0 && DialogResult.OK == MessageBoxEx.Confirm(@"是否清空当前已有文本？")) txtContent.Clear();
            string IP = cmbServer.SelectedValue.ToString();
            bool fromFile = chkFile.Checked;
            bool fromExcel = chkExcel.Checked;
            bool saveData = chkSaveData.Checked;
            bool autoServer = chkAutoServer.Checked;
            await Task.Run(() =>
            {
                Stop = false;
                string ret = null;
                if(fromExcel)
                {
                    string filePath = txtExcelFile.Text;
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbooks wbks = app.Workbooks;
                    _Workbook wbk = wbks.Add(filePath);
                    Sheets shs = wbk.Sheets;
                    _Worksheet wsh = (_Worksheet)shs.Item[1];
                    const int ExpCol = 2;
                    const int IdCol = 3;
                    for(int row = 2; row < wsh.Rows.Count; row++)
                    {
                        Range range = wsh.Cells[row, IdCol];
                        if(null == range || null == range.Value) break;
                        string id = range.Value.ToString();
                        if(Stop)
                            break;
                        string token = txtToken.Text;
                        if(autoServer)
                        {
                            Server server = AutoServer(id);
                            IP = server.IP;
                            token = server.Token;
                        }
                        UpdateLabelAsync(lblCurrCount, string.Format($@"当前 {int.Parse(id):D8}"));
                        string postData = string.Format($@"api_verno=1&api_token={token}&api_member_id={id}");
                        byte[] data = Encoding.UTF8.GetBytes(postData);
                        try
                        {
                            ret = Post(IP, data);
                            UserKit kit = JsonConvert.DeserializeObject<UserKitCover>(ret.Substring(7)).api_data;
                            if(null != kit)
                            {
                                AppendLineAsnyc(txtContent, string.Format("{0}\t{1}\t{2:D8}", kit.api_nickname, kit.api_experience[0], kit.api_member_id));
                                ((Range)wsh.Cells[row, ExpCol]).Value = kit.api_experience[0];
                            }
                            else
                            {
                                ErrorKit err = JsonConvert.DeserializeObject<ErrorKit>(ret.Substring(7));
                                if(null != err)
                                    switch(err.api_result)
                                    {
                                        case 100:
                                            continue;
                                        case 201:
                                        {
                                            AppendLineAsnyc(txtContent, string.Format(@"猫了{0}", Environment.NewLine));
                                            break;
                                        }
                                        default:
                                        {
                                            AppendLineAsnyc(txtContent, ret);
                                            break;
                                        }
                                    }
                                else
                                    AppendLineAsnyc(txtContent, ret);
                            }
                        }
                        catch(Exception)
                        {
                            AppendLineAsnyc(txtContent, ret);
                            const string errFile = @"error.txt";
                            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            string file = Path.Combine(dir, errFile);
                            int count = 0;
                            while(File.Exists(file))
                                file = Path.Combine(dir, errFile.Insert(5, (++count).ToString()));
                            FileTool.CreateAndWriteText(file, txtContent.Text);
                            FileTool.OpenTextFile(file);
                            break;
                        }
                    }
                    wbk.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    wbk.Close();
                    wbks.Close();
                    app.Quit();
                    Marshal.ReleaseComObject(app);

                    string fileName = Path.GetFileName(filePath);
                    if(null != fileName)
                    {
                        int start = fileName.LastIndexOf('(');
                        if(start > 0)
                        {
                            int end = fileName.LastIndexOf(')');
                            string oldDate = fileName.Substring(start + 1, end - start - 1);
                            string newName = fileName.Replace(oldDate, string.Format($@"{DateTime.Now.Year}.{DateTime.Now.Month}.{DateTime.Now.Day}"));
                            FileTool.Rename(filePath, newName);
                        }
                    }
                }
                else if(fromFile)
                {
                    using(StreamReader sr = new StreamReader(txtFile.Text))
                    {
                        string id;
                        while((id = sr.ReadLine()) != null)
                        {
                            if(Stop)
                                break;
                            string token = txtToken.Text;
                            if(autoServer)
                            {
                                Server server = AutoServer(id);
                                IP = server.IP;
                                token = server.Token;
                            }
                            UpdateLabelAsync(lblCurrCount, string.Format($@"当前 {int.Parse(id):D8}"));
                            string postData = string.Format($@"api_verno=1&api_token={token}&api_member_id={id}");
                            byte[] data = Encoding.UTF8.GetBytes(postData);
                            try
                            {
                                ret = Post(IP, data);
                                UserKit kit = JsonConvert.DeserializeObject<UserKitCover>(ret.Substring(7)).api_data;
                                if(null != kit)
                                {
                                    if(saveData) { }
                                    AppendLineAsnyc(txtContent, string.Format("{0}\t{1}\t{2:D8}", kit.api_nickname, kit.api_experience[0], kit.api_member_id));
                                }
                                else
                                {
                                    ErrorKit err = JsonConvert.DeserializeObject<ErrorKit>(ret.Substring(7));
                                    if(null != err)
                                        switch(err.api_result)
                                        {
                                            case 100:
                                                continue;
                                            case 201:
                                            {
                                                AppendLineAsnyc(txtContent, string.Format(@"猫了{0}", Environment.NewLine));
                                                break;
                                            }
                                            default:
                                            {
                                                AppendLineAsnyc(txtContent, ret);
                                                break;
                                            }
                                        }
                                    else
                                        AppendLineAsnyc(txtContent, ret);
                                }
                            }
                            catch(Exception)
                            {
                                AppendLineAsnyc(txtContent, ret);
                                const string errFile = @"error.txt";
                                string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                                string file = Path.Combine(dir, errFile);
                                int count = 0;
                                while(File.Exists(file))
                                    file = Path.Combine(dir, errFile.Insert(5, (++count).ToString()));
                                FileTool.CreateAndWriteText(file, txtContent.Text);
                                FileTool.OpenTextFile(file);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for(int i = int.Parse(txtBeginID.Text); i <= int.Parse(txtEndID.Text); i++)
                    {
                        if(Stop)
                            break;
                        UpdateLabelAsync(lblCurrCount, string.Format($@"当前 {i:D8}"));
                        //data  
                        string postData = string.Format($@"api_verno=1&api_token={txtToken.Text}&api_member_id={i}");
                        byte[] data = Encoding.UTF8.GetBytes(postData);
                        try
                        {
                            ret = Post(IP, data);
                            UserKit kit = JsonConvert.DeserializeObject<UserKitCover>(ret.Substring(7)).api_data;
                            if(null != kit)
                            {
                                AppendLineAsnyc(txtContent, string.Format("{0}\t{1}\t{2:D8}", kit.api_nickname, kit.api_experience[0], kit.api_member_id));
                            }
                            else
                            {
                                ErrorKit err = JsonConvert.DeserializeObject<ErrorKit>(ret.Substring(7));
                                if(null != err)
                                    switch(err.api_result)
                                    {
                                        case 100:
                                            continue;
                                        case 201:
                                        {
                                            AppendLineAsnyc(txtContent, string.Format(@"猫了{0}", Environment.NewLine));
                                            break;
                                        }
                                        default:
                                        {
                                            AppendLineAsnyc(txtContent, ret);
                                            break;
                                        }
                                    }
                                else
                                    AppendLineAsnyc(txtContent, ret);
                            }
                        }
                        catch(Exception)
                        {
                            AppendLineAsnyc(txtContent, ret);
                            const string errFile = @"error.txt";
                            string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            string file = Path.Combine(dir, errFile);
                            int count = 0;
                            while(File.Exists(file))
                                file = Path.Combine(dir, errFile.Insert(5, (++count).ToString()));
                            FileTool.CreateAndWriteText(file, txtContent.Text);
                            FileTool.OpenTextFile(file);
                            break;
                        }
                    }
                }
            });
            MessageBoxEx.Info(@"任务执行完成！");
        }
        private void ChkExcel_CheckedChanged(object sender, EventArgs e)
        {
            if(chkExcel.Checked)
            {
                OpenFileDialog dlg = new OpenFileDialog {Filter = @"Excel文件|*.xlsx;"};
                if(DialogResult.OK == dlg.ShowDialog())
                {
                    txtExcelFile.Text = dlg.FileName;
                }
                else
                {
                    chkExcel.Checked = false;
                }
            }
            else
            {
                txtExcelFile.Clear();
            }
        }
        private void ChkFile_CheckedChanged(object sender, EventArgs e)
        {
            if(chkFile.Checked)
            {
                OpenFileDialog dlg = new OpenFileDialog {Filter = @"文本文件|*.txt;"};
                if(DialogResult.OK == dlg.ShowDialog())
                {
                    txtFile.Text = dlg.FileName;
                }
                else
                {
                    chkFile.Checked = false;
                }
            }
            else
            {
                txtFile.Clear();
            }
        }
        private void CmbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtToken.Text = ((Server)cmbServer.SelectedItem)?.Token;
        }
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmConfig().ShowDialog();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if(File.Exists(ServerFilePath)) ReadServerFile();
            else GenerateServerFile();
            SetServer();
        }
        private string Post(string IP, byte[] data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format($@"http://{IP}/kcsapi/api_req_member/get_practice_enemyinfo"));
            request.Method = "POST";
            request.Accept = @"*/*";
            request.Headers.Add("Accept-Encoding", @"gzip, deflate");
            request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = IP;
            request.Headers.Add("Origin", string.Format($@"http://{IP}"));
            SetHeaderValue(request.Headers, @"Proxy-Connection", @"keep-alive");
            request.Referer = txtReferer.Text;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");
            Stream newStream = request.GetRequestStream();

            // Send the data.  
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            // Get response  
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }
        private string PostSenka(string IP, byte[] data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format($@"http://{IP}/kcsapi/api_req_ranking/mxltvkpyuklh"));
            request.Method = "POST";
            request.Accept = @"*/*";
            request.Headers.Add("Accept-Encoding", @"gzip, deflate");
            request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.8,ja;q=0.6,en;q=0.4,zh-TW;q=0.2");
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = IP;
            request.Headers.Add("Origin", string.Format($@"http://{IP}"));
            SetHeaderValue(request.Headers, @"Proxy-Connection", @"keep-alive");
            request.Referer = txtReferer.Text;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");
            Stream newStream = request.GetRequestStream();

            // Send the data.  
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            // Get response  
            HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }
        private void SetServer()
        {
            cmbServer.DataSource = Servers;
            cmbServer.DisplayMember = @"Name";
            cmbServer.ValueMember = @"IP";
        }
        private void UpdateLabelAsync(Label lbl, string content) => BeginInvoke(new MethodInvoker(() => lbl.Text = content));
    }
}