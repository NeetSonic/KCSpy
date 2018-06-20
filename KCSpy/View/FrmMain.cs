﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KCSpy.Model;
using Neetsonic.Tool;
using Neetsonic.Tool.Extensions;
using Newtonsoft.Json;
using SenkaGo.Util;
using TextBox = Neetsonic.Control.TextBox;

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
        private static bool Stop;
        private static readonly List<Server> Servers;

        public FrmMain()
        {
            InitializeComponent();
        }

        static FrmMain() => Servers = new List<Server>
        {
                new Server {Name = @"大凑", IP = @"203.104.209.150"},
                new Server {Name = @"特鲁克", IP = @"203.104.209.134"},
                new Server {Name = @"林加", IP = @"203.104.209.167"},
                new Server {Name = @"肖特兰", IP = @"125.6.189.7"},
                new Server {Name = @"布因", IP = @"125.6.189.39"},
                new Server {Name = @"塔威", IP = @"125.6.189.71"},
                new Server {Name = @"帕劳", IP = @"125.6.189.103"},
                new Server {Name = @"文莱", IP = @"125.6.189.135"},
                new Server {Name = @"单冠湾", IP = @"125.6.189.167"},
                new Server {Name = @"幌筵", IP = @"125.6.189.215"},
                new Server {Name = @"宿毛湾", IP = @"125.6.189.247"},
                new Server {Name = @"鹿屋", IP = @"203.104.209.23"},
                new Server {Name = @"岩川", IP = @"203.104.209.39"},
                new Server {Name = @"佐伯湾", IP = @"203.104.209.55"},
                new Server {Name = @"柱島", IP = @"203.104.209.102"}
        };

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
            bool saveData = chkSaveData.Checked;
            await Task.Run(() =>
            {
                Stop = false;
                string ret = null;
                if(fromFile)
                {
                    using(StreamReader sr = new StreamReader(txtFile.Text))
                    {
                        string id;
                        while((id = sr.ReadLine()) != null)
                        {
                            if(Stop)
                                break;
                            UpdateLabelAsync(lblCurrCount, string.Format($@"当前 {int.Parse(id):D8}"));
                            string postData = string.Format($@"api_verno=1&api_token={txtToken.Text}&api_member_id={id}");
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

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmConfig().ShowDialog();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cmbServer.DataSource = Servers;
            cmbServer.DisplayMember = @"Name";
            cmbServer.ValueMember = @"IP";
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

        private void UpdateLabelAsync(Label lbl, string content) => BeginInvoke(new MethodInvoker(() => lbl.Text = content));
    }
}