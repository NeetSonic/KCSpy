using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using KCSpy.Model;
using Microsoft.Office.Interop.Excel;
using Neetsonic.Tool;
using Neetsonic.Tool.Extensions;
using Newtonsoft.Json;
using Application = System.Windows.Forms.Application;
using Exception = System.Exception;

namespace KCSpy.Util
{
    public static class Spy
    {
        public enum ProcessType
        {
            Normal,
            Year,
            Month
        }

        private static bool _stopRequest;
        private static readonly WebProxy ProxyACGPower = new WebProxy(@"127.0.0.1:8123", true);
        private static readonly string ServerFilePath = Path.Combine(Application.StartupPath, @"server.xml");
        private static readonly string SeedFilePath = Path.Combine(Application.StartupPath, @"KeySeed.txt");
        private const string SeedDownloadURL = @"http://203.104.209.183/kcs2/js/main.js";
        private const string SenkaPicDownloadURL = "http://203.104.209.7/kcscontents/information/image/rank";
        private static List<int> PORT_API_SEED = new List<int> {4427, 6755, 3264, 7474, 2823, 6304, 6225, 8447, 3219, 4527};
        private static List<int> SENKA_API_SEED = new List<int> {8931, 1201, 1156, 5061, 4569, 4732, 3779, 4568, 5695, 4619, 4912, 5669, 6586};
        static Spy()
        {
            LoadServer();
            LoadKeySeed();
        }
        public static List<Server> Servers { get; private set; } = new List<Server>
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
        public static void AutoRefreshSeed()
        {
            StringBuilder keys = new StringBuilder();
            WebClient web = new WebClient();
            string jsFilePath = Path.Combine(Application.LocalUserAppDataPath, @"main.js");
            web.DownloadFile(SeedDownloadURL, jsFilePath);
            string textAll = FileTool.OpenAndReadAllText(jsFilePath);
            int start = textAll.IndexOf(@"PORT_API_SEED", StringComparison.Ordinal);
            start = textAll.IndexOf('[', start);
            int end = textAll.IndexOf(']', start);
            keys.AppendLine(textAll.Substring(start + 1, end - start - 1));
            start = textAll.IndexOf(@"e.prototype.__drawRanking", StringComparison.Ordinal);
            start = textAll.IndexOf(@"var i=", start, StringComparison.Ordinal);
            start = textAll.IndexOf('[', start);
            end = textAll.IndexOf(']', start);
            keys.Append(textAll.Substring(start + 1, end - start - 1));
            File.Delete(SeedFilePath);
            FileTool.CreateAndWriteText(SeedFilePath, keys.ToString());
            LoadKeySeed();
        }
        public static async Task DownloadAllSenkaPic(DateTime currDate, DateTime endDate)
        {
            await Task.WhenAll(Servers.Select(s => DownloadSenkaPic(s, currDate, endDate)));
        }
        public static async Task DownloadSenkaPic(Server server, DateTime currDate, DateTime endDate)
        {
            await Task.Run(() =>
            {
                string dir = Path.Combine(Global.Config.SenkaPicPath, @"战果人事表", server.Name);
                if(!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
                WebClient myWebClient = new WebClient();
                for(; currDate <= endDate; currDate = currDate.AddMonths(1))
                {
                    string fileName = string.Format($@"{currDate.Year}{currDate.Month:00}{server.ServerID:00}.jpg").Substring(2);
                    string url = string.Concat(SenkaPicDownloadURL, fileName);
                    string filePath = Path.Combine(dir, fileName);
                    try
                    {
                        myWebClient.DownloadFile(url, filePath);
                    }
                    catch { }
                }
            });
        }
        public static async Task ExportClipboardToExcel(string templateFilePath)
        {
            await Task.Run(() =>
            {
                string dstFilePath = Path.Combine(Global.Config.ExcelPath, string.Format($@"{DateTime.Now:yyyyMMddhhmmss}.xlsx"));
                File.Copy(templateFilePath, dstFilePath);
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application {DisplayAlerts = false};
                Workbooks wbks = app.Workbooks;
                _Workbook wbk = wbks.Add(dstFilePath);
                Sheets shs = wbk.Sheets;
                _Worksheet wsh = (_Worksheet)shs.Item[1];
                Range range = wsh.Cells[2, SimpleExcelFileFormat.NameCol];
                range.Select();
                range.PasteSpecial();
                wsh.Columns.AutoFit();
                try
                {
                    wbk.SaveAs(dstFilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                catch(Exception) { }
                finally
                {
                    wbk.Close();
                    wbks.Close();
                    app.Quit();
                    Marshal.ReleaseComObject(app);
                }
                FileTool.OpenDirectory(Global.Config.ExcelPath);
            });
        }
        public static void LoadKeySeed()
        {
            if(File.Exists(SeedFilePath))
            {
                using(TextReader reader = new StreamReader(SeedFilePath))
                {
                    PORT_API_SEED = reader.ReadLine()?.Split(',').Select(int.Parse).ToList();
                    SENKA_API_SEED = reader.ReadLine()?.Split(',').Select(int.Parse).ToList();
                }
            }
        }
        public static void LoadServer()
        {
            if(File.Exists(ServerFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(Servers.GetType());
                using(XmlReader reader = XmlReader.Create(ServerFilePath))
                {
                    Servers = (List<Server>)serializer.Deserialize(reader);
                }
            }
            else { _GenerateServerFile(); }
        }
        public static void OpenSeedFile() => FileTool.OpenTextFile(SeedFilePath);
        public static void OpenServerFile() => FileTool.OpenTextFile(ServerFilePath);
        public static async Task RequestPlayerInfo(Server server, int startID, int endID, int singleTokenTimes, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog)
        {
            _stopRequest = false;
            int step = (int)Math.Ceiling((endID - startID + 1) / (double)singleTokenTimes);
            List<Task> tasks = new List<Task>();
            while(singleTokenTimes > 0)
            {
                tasks.Add(_RequestPlayerInfo(server.TakeOneToken(singleTokenTimes), startID, startID + step - 1, reportCurr, reportResult, reportLog));
                startID += step;
                --singleTokenTimes;
            }
            await Task.WhenAll(tasks);
        }
        public static async Task RequestPlayerInfoTextFile(Server server, string textFilePath, int singleTokenTimes, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog, ProcessType processType)
        {
            _stopRequest = false;
            TextFileTaskKit taskKit = new TextFileTaskKit {ProcessType = processType};
            List<Task> Tasks = new List<Task>();
            Task sqlTask = _SaveTextFile(reportLog, taskKit);
            Task readTask = _ReadTextFile(textFilePath, taskKit);
            while(singleTokenTimes > 0)
            {
                Tasks.Add(_ProcessTextFile(server.TakeOneToken(singleTokenTimes), reportCurr, reportResult, reportLog, taskKit));
                singleTokenTimes--;
            }
            await readTask;
            taskKit.TextFileReadCompleted = true;
            reportLog?.BeginInvoke(string.Format($@"读取文件完毕[{DateTime.Now.ToLongTimeString()}]"), null, null);
            await Task.WhenAll(Tasks);
            taskKit.TextFilePostCompleted = true;
            await sqlTask;
        }
        public static async Task RequestSenka(Server server, int startPage, int endPage, Action<string> report)
        {
            await Task.Run(() =>
            {
                for(int currPage = startPage; currPage <= endPage; currPage++)
                {
                    int memberID = int.Parse(server.MemberID);
                    byte[] data = Encoding.UTF8.GetBytes(string.Format($@"api_pageno={currPage}&api_verno=1&api_token={server.Token}&api_ranking={_CreateKey(memberID)}"));
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format($@"http://{server.IP}/kcsapi/api_req_ranking/mxltvkpyuklh"));
                    request.Method = "POST";
                    request.Accept = @"*/*";
                    request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                    request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.8,ja;q=0.6,en;q=0.4,zh-TW;q=0.2");
                    request.ContentLength = data.Length;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Host = server.IP;
                    request.Headers.Add("Origin", string.Format($@"http://{server.IP}"));
                    request.Headers.SetHeaderValue(@"Proxy-Connection", @"keep-alive");
                    request.Referer = string.Empty;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");
                    request.Proxy = ProxyACGPower;
                    Stream newStream = request.GetRequestStream();
                    newStream.Write(data, 0, data.Length);
                    newStream.Close();
                    string ret;
                    using(HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
                    {
                        using(StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                        {
                            ret = reader.ReadToEnd();
                        }
                    }
                    List<API_SenkaPlayer> players = JsonConvert.DeserializeObject<API_Senka>(ret.Substring(7)).api_data.api_list;
                    List<MemberSenka> members = new List<MemberSenka>();
                    foreach(API_SenkaPlayer player in players)
                    {
                        SenkaAndMedal sankaAndMedal = _DecodeSankaAndMedal(memberID, player.RankNo, player.Senka, player.Medal);
                        //report?.Invoke(string.Format($@"提督：{player.Name}    顺位：{player.RankNo}    战果值：{sankaAndMedal.Senka}    甲章数：{sankaAndMedal.Medal}"));
                        if(!members.Exists(m => m.Name == player.Name))
                        {
                            members.Add(new MemberSenka {Name = player.Name, ServerID = server.ServerID, Senka = sankaAndMedal.Senka, Medal = sankaAndMedal.Medal, Comment = player.Comment, RankNo = player.RankNo});
                        }
                    }
                    //List<MemberTemp> temps = new List<MemberTemp>();
                    foreach(MemberSenka member in members)
                    {
                        List<Member> mems = DataAccess.SearchMember(member.Name, member.ServerID);
                        foreach(Member mem in mems)
                        {
                            API_EnemyInfo kit = _ProcessPlayerInfo(Servers[mem.ServerID - 1], null, null, report, mem.MemberID);
                            if(kit.api_cmt == member.Comment)
                            {
                                double hand = (mem.CurrentExp - mem.MonthBeginExp) * 7d / 10000;
                                double inherit = (mem.MonthBeginExp - mem.YearBeginExp) / 50000d + mem.LastMonthBonus / 35d;
                                double bonus = member.Senka - hand - inherit;
                                report?.Invoke(string.Format($@"提督：{mem.Name}    ID：{mem.MemberID}    经验：{mem.CurrentExp}    当前战果：{member.Senka}    当月手刷：{hand:F}     当月继承：{inherit:F}    当月累计Bonus：{bonus:F}    季度已打Bonus：{mem.CurrSeasonBonusTotal}    甲章：{member.Medal}    上次记录时间：{TimeTool.UnixStampToDateTime(mem.UpdateTimeStamp).ToString(CultureInfo.CurrentCulture)}"));
                            }
                            //temps.Add(new MemberTemp {MemberID = mem.MemberID, CurrentComment = kit.api_cmt, Name = kit.api_nickname, CurrentExp = kit.api_experience[0], UpdateTimeStamp = TimeTool.NowUnixStamp});
                        }
                    }
                    //DataAccess.UpdateMemberCurr(temps);
                }
            });
        }
        public static async Task RequestServerAvailable(Action<string> report)
        {
            await Task.Run(() =>
            {
                byte[] data = Encoding.UTF8.GetBytes(@"api_verno=1&api_dmmuser_id=33737340");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://203.104.209.7/kcsapi/api_world/get_worldinfo");
                request.Method = "POST";
                request.Accept = @"*/*";
                request.Headers.Add("Accept-Encoding", @"gzip, deflate");
                request.Headers.Add("Accept-Language", @"zh-CN,zh;q=0.9,ja;q=0.8,en;q=0.7,zh-TW;q=0.6");
                request.ContentLength = data.Length;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Host = @"203.104.209.7";
                request.Headers.Add("Origin", @"http://203.104.209.7");
                request.Headers.SetHeaderValue(@"Proxy-Connection", @"keep-alive");
                request.Referer = string.Empty;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");
                request.Proxy = ProxyACGPower;
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
                string ret;
                using(HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
                {
                    using(StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        ret = reader.ReadToEnd();
                    }
                }
                List<API_ServerInfo> servers = JsonConvert.DeserializeObject<API_Server>(ret.Substring(7)).api_data.api_world_info;
                foreach(API_ServerInfo server in servers.Where(s => s.api_enabled && s.api_entry))
                {
                    report?.BeginInvoke(string.Format($@"{server.api_name}：可注册"), null, null);
                }
            });
        }
        public static void StopRequest() => _stopRequest = true;
        private static Server _AutoServer(string id)
        {
            int ID = int.Parse(id);
            switch(ID)
            {
                case 91397:
                case 91602:
                case 158405:
                case 167609:
                case 374572:
                case 165877:
                case 91174:
                case 157257:
                case 165551:
                case 168387:
                case 164597:
                case 150721:
                case 87779:
                case 93602:
                case 374133:
                case 158774:
                case 378740:
                case 149714:
                case 95026:
                case 167534:
                case 151906:
                    return Servers[4];
                case 86803:
                    return Servers[2];
            }
            return ID < 1000000 ? Servers[ID / 100000] : Servers[ID / 1000000 - 1];
        }
        private static string _CreateKey(int memberID)
        {
            Random ran = new Random();
            long p1 = PORT_API_SEED[memberID % 10],
                 p2 = DateTime.UtcNow.Utc1970InMillisec() / 1000,
                 p3 = (long)(1e3 * (Math.Floor(9 * ran.NextDouble()) + 1) + memberID % 1e3),
                 p4 = (long)(Math.Floor(8999 * ran.NextDouble()) + 1e3),
                 p5 = (long)Math.Floor(32767 * ran.NextDouble()) + 32768,
                 p6 = (long)Math.Floor(10 * ran.NextDouble()),
                 p7 = (long)Math.Floor(10 * ran.NextDouble()),
                 p8 = (long)Math.Floor(10 * ran.NextDouble()),
                 p9 = long.Parse(memberID.ToString().Substring(0, 4)),
                 p10 = (4132653 + p5) * (p9 + 1000) - p2 + 1875979 + 9 * p5,
                 p11 = p10 - memberID,
                 p12 = p11 * p1;
            string ret = (p6 + p3.ToString() + p12 + p4).Insert(8, p7.ToString()).Insert(18, p8.ToString()) + p5;
            return ret;
        }
        private static SenkaAndMedal _DecodeSankaAndMedal(int memberId, int rankNo, long obfuscatedRate, long obfuscatedMedal)
        {
            int n = SENKA_API_SEED[rankNo % 13];
            int medals = Convert.ToInt32(obfuscatedMedal / (n + 1853) - 157);
            int seed = _GetPortSeed(memberId);
            int senka = Convert.ToInt32(obfuscatedRate / n / seed - 91);
            return new SenkaAndMedal {Senka = senka, Medal = medals};
        }
        private static void _GenerateServerFile()
        {
            XmlSerializer serializer = new XmlSerializer(Servers.GetType());
            using(TextWriter writer = new StreamWriter(ServerFilePath))
            {
                serializer.Serialize(writer, Servers);
            }
        }
        private static int _GetPortSeed(int t)
        {
            int e = PORT_API_SEED[t % 10];
            return (e - e % 100) / 100;
        }
        private static void _HandleError(Server server, Action<string> reportLog, int id, string ret)
        {
            API_Error err = JsonConvert.DeserializeObject<API_Error>(ret.Substring(7));
            if(null != err)
            {
                switch(err.api_result)
                {
                    case 100:
                    {
                        reportLog?.BeginInvoke(string.Format($@"{id:D8}"), null, null);
                        break;
                    }
                    case 201:
                    {
                        reportLog?.BeginInvoke(string.Format($@"Token[{server.Token}] 猫了"), null, null);
                        break;
                    }
                    default:
                    {
                        reportLog?.BeginInvoke(ret, null, null);
                        break;
                    }
                }
            }
            else { reportLog?.BeginInvoke(ret, null, null); }
        }
        private static string _PostPlayerInfo(string IP, byte[] data)
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
            request.Headers.SetHeaderValue(@"Proxy-Connection", @"keep-alive");
            request.Referer = string.Empty;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
            request.Headers.Add("X-Requested-With", @"ShockwaveFlash/27.0.0.187");
            request.Proxy = ProxyACGPower;
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            string ret;
            using(HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
            {
                using(StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8))
                {
                    ret = reader.ReadToEnd();
                }
            }
            return ret;
        }
        private static void _ProcessPlayerInfo(PlayerInfo playerInfo, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog, TextFileTaskKit taskKit = null)
        {
            Server server = Servers[playerInfo.ServerID - 1];
            reportCurr?.BeginInvoke(string.Format($@"当前 {playerInfo.ID:D8}"), null, null);
            string postData = string.Format($@"api_verno=1&api_token={server.Token}&api_member_id={playerInfo.ID}");
            byte[] data = Encoding.UTF8.GetBytes(postData);
            try
            {
                string ret = _PostPlayerInfo(server.IP, data);
                API_EnemyInfo kit = JsonConvert.DeserializeObject<API_Practice>(ret.Substring(7)).api_data;
                if(null != kit)
                {
                    long timestamp = TimeTool.NowUnixStamp;
                    if(null != taskKit && taskKit.ProcessType == ProcessType.Year)
                    {
                        taskKit.Members.Enqueue(new Member
                        {
                                MemberID = playerInfo.ID,
                                Name = kit.api_nickname.FormatName(),
                                ServerID = server.ServerID,
                                CurrentExp = kit.api_experience[0],
                                YearBeginExp = kit.api_experience[0],
                                MonthBeginExp = kit.api_experience[0],
                                CurrentComment = kit.api_cmt,
                                UpdateTimeStamp = timestamp
                        });
                        reportResult?.BeginInvoke(string.Format("{0}\t{1}\t{2:D8}\t{3}\t{4}\t{5}\t{6}", kit.api_nickname.FormatName(), kit.api_experience[0], kit.api_member_id, kit.api_furniture, timestamp, server.ServerID, server.Name), null, null);
                    }
                    else
                    {
                        reportResult?.BeginInvoke(string.Format("{0}\t{1}\t{2:D8}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", kit.api_nickname.FormatName(), kit.api_experience[0], kit.api_member_id, kit.api_furniture, timestamp, server.ServerID, server.Name, playerInfo.DateStamp, kit.api_experience[0] - playerInfo.Exp, playerInfo.Flag), null, null);
                    }
                }
                else
                {
                    _HandleError(server, reportLog, playerInfo.ID, ret);
                }
            }
            catch(Exception ex) { reportLog?.BeginInvoke(string.Format($@"{ex.Message}[{playerInfo.ID:d8}]"), null, null); }
        }
        private static API_EnemyInfo _ProcessPlayerInfo(Server server, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog, int id)
        {
            API_EnemyInfo kit = null;
            reportCurr?.BeginInvoke(string.Format($@"当前 {id:D8}"), null, null);
            string postData = string.Format($@"api_verno=1&api_token={server.Token}&api_member_id={id}");
            byte[] data = Encoding.UTF8.GetBytes(postData);
            try
            {
                string ret = _PostPlayerInfo(server.IP, data);
                kit = JsonConvert.DeserializeObject<API_Practice>(ret.Substring(7)).api_data;
                if(null != kit)
                {
                    reportResult?.BeginInvoke(string.Format("{0}\t{1}\t{2:D8}\t{3}\t{4}\t{5}\t{6}", kit.api_nickname.FormatName(), kit.api_experience[0], kit.api_member_id, kit.api_furniture, TimeTool.NowUnixStamp, server.ServerID, server.Name), null, null);
                }
                else
                {
                    _HandleError(server, reportLog, id, ret);
                }
            }
            catch(Exception ex) { reportLog?.BeginInvoke(string.Format($@"{ex.Message}[{id:d8}]"), null, null); }
            return kit;
        }
        private static Task _ProcessTextFile(Server server, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog, TextFileTaskKit taskKit)
        {
            return Task.Run(() =>
            {
                reportLog.BeginInvoke(string.Format($@"Token[{server.Token}]开始工作"), null, null);
                while(true)
                {
                    if(_stopRequest) { break; }
                    if(taskKit.IDs.TryDequeue(out string data))
                    {
                        if(data.Contains('|'))
                        {
                            _ProcessPlayerInfo(PlayerInfo.Parse(data), reportCurr, reportResult, reportLog, taskKit);
                        }
                        else
                        {
                            _ProcessPlayerInfo(server, reportCurr, reportResult, reportLog, int.Parse(data));
                        }
                    }
                    else
                    {
                        if(taskKit.TextFileReadCompleted) { break; }
                        Thread.Sleep(1000);
                    }
                }
            });
        }
        private static Task _ReadTextFile(string textFilePath, TextFileTaskKit taskKit)
        {
            return Task.Run(() =>
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(textFilePath, FileMode.Open, FileAccess.Read);
                    using(TextReader reader = new StreamReader(fs))
                    {
                        string id;
                        while(null != (id = reader.ReadLine()))
                        {
                            if(_stopRequest) { break; }
                            taskKit.IDs.Enqueue(id);
                        }
                    }
                }
                finally
                {
                    fs?.Dispose();
                }
            });
        }
        private static async Task _RequestPlayerInfo(Server server, int startID, int endID, Action<string> reportCurr, Action<string> reportResult, Action<string> reportLog)
        {
            await Task.Run(() =>
            {
                reportLog.BeginInvoke(string.Format($@"Token[{server.Token}]开始工作"), null, null);
                for(int id = startID; id <= endID; id++)
                {
                    if(_stopRequest) { break; }
                    _ProcessPlayerInfo(server, reportCurr, reportResult, reportLog, id);
                }
            });
        }
        private static Task _SaveTextFile(Action<string> reportLog, TextFileTaskKit taskKit)
        {
            return Task.Run(() =>
            {
                if(taskKit.ProcessType == ProcessType.Year)
                {
                    reportLog?.BeginInvoke(string.Format($@"数据库线程启动[{DateTime.Now.ToLongTimeString()}]"), null, null);
                    while(true)
                    {
                        if(taskKit.Members.TryDequeue(out Member member))
                        {
                            try
                            {
                                DataAccess.NewMember(member);
                            }
                            catch(Exception ex)
                            {
                                reportLog?.BeginInvoke(ex.Message, null, null);
                            }
                        }
                        else
                        {
                            if(taskKit.TextFilePostCompleted) { break; }
                            Thread.Sleep(1000);
                        }
                    }
                }
            });
        }

        private sealed class SenkaAndMedal
        {
            public int Senka { get; set; }
            public int Medal { get; set; }
        }

        private static class SimpleExcelFileFormat
        {
            public const int NameCol = 1;
            public const int ExpCol = 2;
            public const int IdCol = 3;
            public const int FurnitureCol = 4;
            public const int DateStampCol = 5;
            public const int ServerIdCol = 6;
            public const int ServerCol = 7;
        }

        private static class ExcelFileFormat
        {
            public const int NameCol = 1;
            public const int ExpCol = 2;
            public const int IdCol = 3;
            public const int FurnitureCol = 4;
            public const int DateStampCol = 5;
            public const int ServerIdCol = 6;
            public const int ServerCol = 7;
            public const int LastDateStampCol = 8;
            public const int Increasement = 9;
            public const int FlagCol = 10;
        }

        private sealed class TextFileTaskKit
        {
            public readonly ConcurrentQueue<string> IDs = new ConcurrentQueue<string>();
            public bool TextFileReadCompleted;
            public bool TextFilePostCompleted;
            public ProcessType ProcessType;
            public readonly ConcurrentQueue<Member> Members = new ConcurrentQueue<Member>();
        }

        private sealed class PlayerInfo
        {
            private PlayerInfo() { }
            public int Exp { get; private set; }
            public int ID { get; private set; }
            public int ServerID { get; private set; }
            public string DateStamp { get; private set; }
            public string Flag { get; private set; }
            public static PlayerInfo Parse(string data)
            {
                List<string> datas = data.Split('|').ToList();
                PlayerInfo ret = datas.Count > 2 ? new PlayerInfo {Exp = int.Parse(datas[0]), ID = int.Parse(datas[1]), ServerID = int.Parse(datas[2]), DateStamp = datas[3], Flag = datas[4]} : new PlayerInfo {ID = int.Parse(datas[0]), ServerID = int.Parse(datas[1])};
                return ret;
            }
        }
    }

    public static class NameEx
    {
        public static string FormatName(this string name) => name.Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
    }
}