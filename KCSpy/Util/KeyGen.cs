using System;
using System.Collections.Generic;
using Neetsonic.Tool.Extensions;

namespace KCSpy.Util
{
    public static class KeyGen
    {
        private static readonly int[] PORT_API_SEED = {4427, 6755, 3264, 7474, 2823, 6304, 6225, 8447, 3219, 4527};
        private static readonly int[] I = {8931, 1201, 1156, 5061, 4569, 4732, 3779, 4568, 5695, 4619, 4912, 5669, 6586};

        public static int _getPortSeed(int t)
        {
            int e = PORT_API_SEED[t % 10];
            return (e - e % 100) / 100;
        }

        public static string CreateKey(int memberID)
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

        /// <summary>
        /// A custom decoder for De-obfuscated ranking rate and medal number
        /// Source: 
        /// http://ch.nicovideo.jp/arisu_yaya/blomaga/ar941858
        /// https://github.com/yukixz/kctools/blob/master/rank.py
        /// </summary>
        /// <param name="memberId">Teitoku's member ID</param>
        /// <param name="rankNo">Actual ranking number from 1 to 990</param>
        /// <param name="obfuscatedRate">Obfuscated ranking rate from Kancolle server</param>
        /// <param name="obfuscatedMedal">Pbfuscated ranking medal number from Kancolle server</param>
        /// <returns></returns>
        public static Dictionary<string, double> DecodeRankAndMedal(int memberId, int rankNo, long obfuscatedRate, long obfuscatedMedal)
        {
            Dictionary<string, double> rateAndMedal = new Dictionary<string, double>();
            int n = I[rankNo % 13];
            long api_medals = obfuscatedMedal / (n + 1853) - 157;
            long r = obfuscatedRate;
            int _ = _getPortSeed(memberId);
            r = r / n / _ - 91;
            rateAndMedal["rate"] = r;
            rateAndMedal["medal"] = api_medals;
            return rateAndMedal;
        }
    }
}