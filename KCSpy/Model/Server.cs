using System.Collections.Generic;
using System.Linq;

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
        public Server TakeOneToken(int seed)
        {
            if(Tokens?.Count() > 1)
            {
                List<string> tokens = Tokens.ToList();
                Server ret = (Server)MemberwiseClone();
                ret.Token = tokens[seed % tokens.Count];
                return ret;
            }
            return this; 
        }
    }
}