using System.Collections.Generic;

namespace KCSpy.Model
{
    public sealed class Server
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public string Token { get; set; }
        public string TokenString { get; set; }
        public string MemberID { get; set; }
        public int ServerID { get; set; }
        public IEnumerable<string> Tokens => TokenString?.Split(';');
        public Server TakeOneToken(string token)
        {
            Server ret = (Server)MemberwiseClone();
            ret.Token = token;
            return ret;
        }
    }
}