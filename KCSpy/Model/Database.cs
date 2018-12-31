using System;
using System.Data;
using Neetsonic.Tool.Extensions;

namespace KCSpy.Model
{
    public class Member
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public int ServerID { get; set; }
        public int CurrentExp { get; set; }
        public int YearBeginExp { get; set; }
        public int MonthBeginExp { get; set; }
        public string CurrentComment { get; set; }
        public int CurrentMedal { get; set; }
        public int LastMonthBonus { get; set; }
        public int CurrSeasonBonusTotal { get; set; }
        public long UpdateTimeStamp { get; set; }
        // 月次戦果＝[引き継ぎ戦果(1)]＋[期間内の提督経験値から算出される戦果(2)]＋[ボーナス戦果(3)]
        // (1)引き継ぎ戦果＝[同年1月～前月までの累計提督経験値]/50000 + 先月分のボーナス戦果/35
        // (2)戦果は期間内に得た提督経験値に比例していると言われている（[当月に稼いだ提督経験値]*7/10000 ）
        // (3)ボーナス戦果：1-5などの【Extra Operation】海域のゲージを破壊した際や、一部の任務で大量の戦果を得られる

        // 年初 录入ID Name YearBeginExp CurrMonthBeginExp InheritSenka(0) LastMonthBonus(0) CurrSeasonBonusTotal(0) CurrentMedal(未知) CurrentComment CurrentExp UpdateTimeStamp

        // 每次查询战果时：
        // 配对按照名字唯一->签名唯一->信息筛选的流程来（算出手刷和继承与当前战果值的可能性）
        // 配对成功后计算显示
        // 本月手刷值 = [查询出的当前经验 -  MonthBeginExp]*7/10000
        // 本月继承值 = [MonthBeginExp -  YearBeginExp] / 50000 +  LastMonthBonus / 35 
        // 本月已经取得的Bonus战果值 = 查询出的战果（显示）-  本月手刷值 - 本月继承值
        // 更新 查询出的当前经验->CurrentExp, CurrentComment, CurrentMedal, UpdateTimeStamp

        // 月末14时查询战果时 追加操作
        // 记录 本月Bonus->LastMonthBonus, CurrSeasonBonusTotal+=本月Bonus

        // 月末21时 
        // 更新 CurrentComment, MonthBeginExp, UpdateTimeStamp
    }

    public class MemberTemp
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public int CurrentExp { get; set; }
        public string CurrentComment { get; set; }
        public long UpdateTimeStamp { get; set; }
    }

    public sealed class MemberSenka
    {
        public int ServerID { get; set; }
        public int RankNo { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int Medal { get; set; }
        public int Senka { get; set; }
        public int Exp { get; set; }
        public DateTime RecordTime { get; set; }
    }
}