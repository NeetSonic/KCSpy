using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using HtmlAgilityPack;
using KCSpy.Model;
using Neetsonic.Tool;
using Neetsonic.Tool.Database;
using Neetsonic.Tool.Extensions;
using Newtonsoft.Json;
using SenkaGo.Util;
using TextBox = Neetsonic.Control.TextBox;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

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
                                    if(saveData)
                                    {
                                        
                                    }
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

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmConfig().ShowDialog();
        }

        private class Illustration
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string URL { get; set; }
            public string FileFormat => URL.Substring(URL.LastIndexOf('.'));
        }

        private static string GetURL(string illust_id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format($@"https://www.pixiv.net/member_illust.php?mode=medium&illust_id={illust_id}"));
            request.Method = "GET";
            request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.Headers.Add("Accept-Encoding", @"gzip, deflate, br");
            request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
            request.Host = @"www.pixiv.net";
            SetHeaderValue(request.Headers, @"Connection", @"keep-alive");
            request.Referer = @"https://www.pixiv.net/member_illust.php?id=67388";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Headers.Add("Upgrade-Insecure-Requests", @"1");
            request.Headers.Add("Cache-Control", @"max-age=0");
            request.Headers.Add("Cookie", @"first_visit_datetime_pc=2018-06-08+13%3A48%3A14; p_ab_id=5; p_ab_id_2=2; yuid=GQYEZzA58; __utmc=235335808; __utmz=235335808.1528433295.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); limited_ads=%7B%22header%22%3A%22%22%7D; _ga=GA1.2.657713264.1528433295; _gid=GA1.2.890661813.1528433319; login_bc=1; PHPSESSID=20722713_06d5f16685a8652a2e6827803858424d; device_token=246818d8418f7e288ecafab2d3f6ae48; privacy_policy_agreement=1; c_type=24; a_type=0; b_type=1; login_ever=yes; OX_plg=swf|sl|wmp|shk|pm; __utma=235335808.657713264.1528433295.1528441113.1528443138.4; GMOSSP_USER=LxIdoplcRqyfLP96; module_orders_mypage=%5B%7B%22name%22%3A%22sketch_live%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22tag_follow%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22recommended_illusts%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22showcase%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22everyone_new_illusts%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22following_new_illusts%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22mypixiv_new_illusts%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22fanbox%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22featured_tags%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22contests%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22user_events%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22sensei_courses%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22spotlight%22%2C%22visible%22%3Atrue%7D%2C%7B%22name%22%3A%22booth_follow_items%22%2C%22visible%22%3Atrue%7D%5D; is_sensei_service_user=1; __utmv=235335808.|2=login%20ever=yes=1^3=plan=premium=1^5=gender=male=1^6=user_id=20722713=1^9=p_ab_id=5=1^10=p_ab_id_2=2=1^11=lang=zh=1; __gads=ID=aab55b03e6d876e3:T=1528447041:S=ALNI_MYqFjBSkXKUvVBd9kkePwgCzSZTRg; __utmt=1; __utmb=235335808.39.9.1528447071900; tag_view_ranking=I8PKmJXPGb~RTJMXD26Ak~LtW-gO6CmS~4dxKmeew3P~a-yCMcqYxL~aO5pLrFNOB~4uaq8PBs7U~RFVdOq-YjA~Js5EBY4gOW~Ms9Iyj7TRt~Z9VMjBRtsS~YLmVA5rwXe~BU9SQkS-zU~zCtkCvzrOi~EGefOqA6KB~Ow9mLSvmxK~hQzD2l5xLh~4-_9de7LBH~iFcW6hPGPU~PGv6mTCThy~NCZvWchGEm~edXUchbQp5~iajmMoolkv~c8UxuNkgG6~8HRshblb4Q~tgP8r-gOe_~OUkihvwBMZ~cpt_Nk5mjc~lRxin4V3-v~HowAxXvXGp~7tRe7oIUrM~kSnbadY9nM~6gkYqC1mvr~KaIFU4VLba");
            WebProxy proxyObject = new WebProxy(@"127.0.0.1:8123", true);//str为IP地址 port为端口号 代理类  
            request.Proxy = proxyObject; //设置代理  

            HtmlDocument doc = new HtmlDocument();
            using(HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
            {
                doc.Load(new GZipStream(myResponse.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8);
                myResponse.Close();
            }
            int begin = doc.ParsedText.IndexOf(@"original", StringComparison.Ordinal);
            int end = doc.ParsedText.IndexOf(@"}", begin, StringComparison.Ordinal);
            string raw = doc.ParsedText.Substring(begin, end-begin+1);
            begin = raw.IndexOf(@"https", StringComparison.Ordinal);
            end = raw.LastIndexOf('"');
            string ret = raw.Substring(begin, end - begin);
            ret = ret.Replace(@"\", string.Empty);
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebProxy proxyObject = new WebProxy(@"127.0.0.1:8123", true); //str为IP地址 port为端口号 代理类  
            List<Illustration> illustrations = new List<Illustration>();
            int page = 0;
            const string MenuURL = @"https://www.pixiv.net/member_illust.php?id=441987";
            HtmlNode haveNext;
            do
            {
                page++;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format($@"{MenuURL}&type=all&p={page}"));
                request.Method = "GET";
                request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.Headers.Add("Accept-Encoding", @"gzip, deflate, br");
                request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
                request.Host = @"www.pixiv.net";
                SetHeaderValue(request.Headers, @"Connection", @"keep-alive");
                request.Referer = @"https://www.pixiv.net";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                request.Headers.Add("Upgrade-Insecure-Requests", @"1");
                request.Headers.Add("Cookie", @"first_visit_datetime_pc=2018-06-08+13%3A48%3A14; p_ab_id=5; p_ab_id_2=2; yuid=GQYEZzA58; __utma=235335808.657713264.1528433295.1528433295.1528433295.1; __utmc=235335808; __utmz=235335808.1528433295.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmt=1; limited_ads=%7B%22header%22%3A%22%22%7D; _ga=GA1.2.657713264.1528433295; _gid=GA1.2.890661813.1528433319; login_bc=1; PHPSESSID=20722713_06d5f16685a8652a2e6827803858424d; device_token=246818d8418f7e288ecafab2d3f6ae48; privacy_policy_agreement=1; c_type=24; a_type=0; b_type=1; login_ever=yes; __utmv=235335808.|2=login%20ever=yes=1^3=plan=premium=1^5=gender=male=1^6=user_id=20722713=1^9=p_ab_id=5=1^10=p_ab_id_2=2=1^11=lang=zh=1; __utmb=235335808.2.10.1528433295");
                request.Proxy = proxyObject; //设置代理  

                HtmlDocument doc = new HtmlDocument();
                using(HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
                {
                    doc.Load(new GZipStream(myResponse.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8);
                    myResponse.Close();
                }
                int count = doc.DocumentNode.SelectNodes(@"//*[@id='wrapper']/div[1]/div[1]/div/div[2]/ul/li").Count;
                for(int i = 1; i <= count; i++)
                {
                    HtmlNode nodeURL = doc.DocumentNode.SelectSingleNode(string.Format($@"//*[@id='wrapper']/div[1]/div[1]/div/div[2]/ul/li[{i}]/a[1]/div[1]/img"));
                    HtmlNode nodeName = doc.DocumentNode.SelectSingleNode(string.Format($@"//*[@id='wrapper']/div[1]/div[1]/div/div[2]/ul/li[{i}]/a[2]"));
                    string href = nodeName.Attributes[@"href"].Value;
                    string illust_id = href.Substring(href.LastIndexOf('=') + 1);
                    illustrations.Add(new Illustration
                    {
                        ID = href.Substring(href.LastIndexOf('=') + 1),
                        Name = nodeName.ChildNodes[0].InnerText,
                        URL = GetURL(illust_id)
                    });
                }
                haveNext = doc.DocumentNode.SelectSingleNode(@"//*[@id='wrapper']/div[1]/div[1]/div/ul[1]/div/span[2]/a");
            } while(haveNext != null);
            foreach(Illustration illust in illustrations)
            {
                int i = 0;
                while(true)
                {
                    try
                    {
                        if(i > 0)
                        {
                            illust.URL = string.Format($@"{illust.URL.Substring(0, illust.URL.LastIndexOf(string.Format($@"p{i-1}"), StringComparison.Ordinal))}p{i}{illust.FileFormat}");
                        }
                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(illust.URL);
                        webRequest.Method = "GET";
                        webRequest.Referer = @"https://www.pixiv.net/member_illust.php?mode=manga";
                        webRequest.Proxy = proxyObject; //设置代理  
                        using(Stream reader = ((HttpWebResponse)webRequest.GetResponse()).GetResponseStream())
                        {
                            using(FileStream writer = new FileStream(Path.Combine(@"D:\", string.Format($@"{illust.Name}_{illust.ID}_p{i}{illust.FileFormat}")), FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                byte[] buff = new byte[512];
                                int c; //实际读取的字节数  
                                while(reader != null && (c = reader.Read(buff, 0, buff.Length)) > 0)
                                {
                                    writer.Write(buff, 0, c);
                                }
                                writer.Close();
                            }
                            reader?.Close();
                        }
                        i++;
                    }
                    catch(Exception)
                    {
                        break;
                    }
                }
            }


        }
    }
}