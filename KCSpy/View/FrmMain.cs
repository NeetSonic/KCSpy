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

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
        private static bool Stop;

        public FrmMain()
        {
            InitializeComponent();
        }

        private static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            PropertyInfo property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if(property != null)
            {
                NameValueCollection collection = property.GetValue(header, null) as NameValueCollection;
                if(collection != null) collection[name] = value;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtContent.Clear();
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            txtContent.Focus();
            txtContent.SelectAll();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Stop = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if(txtContent.Text.Length > 0 && DialogResult.OK == MessageBoxEx.Confirm(@"是否清空当前已有文本？"))
            {
                txtContent.Clear();
            }

            Task.Run(() =>
            {
                string ret = null;
                Stop = false;
                List<UserKit> users = new List<UserKit>();
                for(int i = int.Parse(txtBeginID.Text); i <= int.Parse(txtEndID.Text); i++)
                {
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        lblCurrCount.Text = string.Format($@"当前 {i:D8}");
                    }));
                    //users.Add(kit);
                    if(Stop)
                        break;
                    try
                    {
                        //data  
                        string postData = string.Format($@"api_verno=1&api_token={txtToken.Text}&api_member_id={i}");
                        byte[] data = Encoding.UTF8.GetBytes(postData);

                        //肖特兰
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://125.6.189.7/kcsapi/api_req_member/get_practice_enemyinfo");
                        //request.Method = "POST";
                        //request.Accept = @"*/*";
                        //request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                        //request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.8,ja;q=0.6,en;q=0.4,zh-TW;q=0.2");
                        //request.ContentLength = data.Length;
                        //request.ContentType = "application/x-www-form-urlencoded";
                        //request.Host = "125.6.189.7";
                        //request.Headers.Add("Origin", @"http://125.6.189.7");
                        //SetHeaderValue(request.Headers, @"Proxy-Connection", @"Keep-Alive");
                        //request.Referer = txtReferer.Text;
                        //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

                        //大凑
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://203.104.209.150/kcsapi/api_req_member/get_practice_enemyinfo");
                        request.Method = "POST";
                        request.Accept = @"*/*";
                        request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                        request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
                        request.ContentLength = data.Length;
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Host = "203.104.209.150";
                        request.Headers.Add("Origin", @"http://203.104.209.150");
                        SetHeaderValue(request.Headers, @"Proxy-Connection", @"keep-alive");
                        request.Referer = txtReferer.Text;
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                        request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");

                        //塔威
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://125.6.189.71/kcsapi/api_req_member/get_practice_enemyinfo");
                        //request.Method = "POST";
                        //request.Accept = @"*/*";
                        //request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                        //request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
                        //request.ContentLength = data.Length;
                        //request.ContentType = "application/x-www-form-urlencoded";
                        //request.Host = "125.6.189.71";
                        //request.Headers.Add("Origin", @"http://125.6.189.71");
                        //SetHeaderValue(request.Headers, @"Proxy-Connection", @"Keep-Alive");
                        //request.Referer = txtReferer.Text;
                        //request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                        //request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");

                        Stream newStream = request.GetRequestStream();

                        // Send the data.  
                        newStream.Write(data, 0, data.Length);
                        newStream.Close();

                        // Get response  
                        HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        ret = reader.ReadToEnd();
                        UserKit kit = JsonConvert.DeserializeObject<UserKitCover>(ret.Substring(7)).api_data;
                        if(null != kit)
                        {
                            BeginInvoke(new MethodInvoker(() =>
                            {
                                txtContent.AppendText(string.Format("{0}\t{1}\t{2:D8}\t{3}", kit.api_nickname, kit.api_experience[0], kit.api_member_id, Environment.NewLine));
                            }));
                        }
                        else
                        {
                            ErrorKit err = JsonConvert.DeserializeObject<ErrorKit>(ret.Substring(7));
                            if(null != err)
                            {
                                switch(err.api_result)
                                {
                                    case 100: continue;
                                    case 201:
                                    {
                                        BeginInvoke(new MethodInvoker(() =>
                                        {
                                            txtContent.AppendText(string.Format(@"猫了{0}", Environment.NewLine));
                                        }));
                                        break;
                                    }
                                    default:
                                    {
                                        BeginInvoke(new MethodInvoker(() =>
                                        {
                                            txtContent.AppendText(ret);
                                        }));
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                BeginInvoke(new MethodInvoker(() =>
                                {
                                    txtContent.AppendText(ret);
                                }));
                            }
                        }


                    }
                    catch(Exception)
                    {
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            txtContent.AppendText(ret);
                        }));
                        string errFile = @"error.txt";
                        string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string file = Path.Combine(dir, errFile);
                        int count = 0;
                        while(File.Exists(file)) file = Path.Combine(dir, errFile.Insert(5, (++count).ToString()));
                        FileTool.CreateAndWriteText(file, txtContent.Text);
                        FileTool.OpenTextFile(file);
                        break;
                    }
                }

                BeginInvoke(new MethodInvoker(() =>
                {
                    MessageBoxEx.Info(@"任务执行完成！");
                }));
            });
            //users.Sort(new KitCmp());
            //users.Reverse();
            //foreach(UserKit kit in users) txtContent.AppendText(string.Format($@"提督名：{kit.api_nickname}  经验值：{kit.api_experience[0]}{Environment.NewLine}  ID:{kit.api_member_id}"));
        }

        internal class KitCmp : IComparer<UserKit>
        {
            public int Compare(UserKit x, UserKit y) => x.api_experience[0] - y.api_experience[0];
        }
    }
}