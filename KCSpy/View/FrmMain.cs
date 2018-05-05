using System;
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
using Newtonsoft.Json;

namespace KCSpy.View
{
    public partial class FrmMain : Form
    {
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

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            txtContent.Focus();
            txtContent.SelectAll();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                List<UserKit> users = new List<UserKit>();
                for(int i = int.Parse(txtBeginID.Text); i <= int.Parse(txtEndID.Text); i++)
                {
                    try
                    {
                        //data  
                        string postData = string.Format($@"api_verno=1&api_token={txtToken.Text}&api_member_id={i}");
                        byte[] data = Encoding.UTF8.GetBytes(postData);

                        // Prepare web request...  
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://125.6.189.7/kcsapi/api_req_member/get_practice_enemyinfo");
                        request.Method = "POST";
                        request.Accept = @"*/*";
                        request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                        request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.8,ja;q=0.6,en;q=0.4,zh-TW;q=0.2");
                        request.ContentLength = data.Length;
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.Host = "125.6.189.7";
                        request.Headers.Add("Origin", @"http://125.6.189.7");
                        SetHeaderValue(request.Headers, @"Proxy-Connection", @"Keep-Alive");
                        request.Referer = txtReferer.Text;
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

                        Stream newStream = request.GetRequestStream();

                        // Send the data.  
                        newStream.Write(data, 0, data.Length);
                        newStream.Close();

                        // Get response  
                        HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        UserKit kit = JsonConvert.DeserializeObject<UserKitCover>(reader.ReadToEnd().Substring(7)).api_data;
                        if(null != kit)
                        {
                            BeginInvoke(new MethodInvoker(() =>
                            {
                                txtContent.AppendText(string.Format("{0}\t{1}\t{2}\t{3}", kit.api_nickname, kit.api_experience[0], kit.api_member_id, Environment.NewLine));
                            }));
                        }
                        //users.Add(kit);
                    }
                    catch(Exception ex)
                    {
                        string file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"error");
                        FileTool.CreateAndWriteText(file, txtContent.Text);
                        FileTool.OpenTextFile(file);
                        break;
                    }
                }
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