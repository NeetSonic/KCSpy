using System.Collections.Generic;

namespace KCSpy.Model
{
    public sealed class API_Senka
    {
        /// <summary>
        /// 
        /// </summary>
        public int api_result{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string api_result_msg{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public API_SenkaData api_data { get; set; }
    }
    public sealed class API_SenkaData
    {
        /// <summary>
        /// 
        /// </summary>
        public int api_count{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int api_page_count{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int api_disp_page{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<API_SenkaPlayer> api_list{ get; set; }
    }
    public sealed class API_SenkaPlayer
    {
        /// <summary>
        /// 顺位
        /// </summary>
        public int api_mxltvkpyuklh{ get; set; }

        /// <summary>
        /// 提督名
        /// </summary>
        public string api_mtjmdcwtvhdr{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int api_pbgkfylkbjuy{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int api_pcumlrymlujh{ get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string api_itbrdpdbkynm{ get; set; }

        /// <summary>
        /// 甲章数（加密）
        /// </summary>
        public long api_itslcqtmrxtf{ get; set; }

        /// <summary>
        /// 战果值（加密）
        /// </summary>
        public long api_wuhnhojjxmke{ get; set; }

        public int RankNo => api_mxltvkpyuklh;
        public string PlayerName => api_mtjmdcwtvhdr;
        public string Comment => api_itbrdpdbkynm;
        public long Medal => api_itslcqtmrxtf;
        public long Senka => api_wuhnhojjxmke;
    }
    public sealed class API_EnemyInfo
    {
        public int api_member_id { get; set; }
        public string api_nickname { get; set; }
        public string api_nickname_id { get; set; }
        public string api_cmt_id { get; set; }
        public List<int> api_experience { get; set; }
        public List<int> api_ship { get; set; }
        public List<int> api_slotitem { get; set; }
        public string api_cmt { get; set; }
        public int api_level { get; set; }
        public int api_rank { get; set; }
        public int api_friend { get; set; }
        public int api_furniture { get; set; }
        public string api_deckname { get; set; }
        public string api_deckname_id { get; set; }
        public API_Deck api_deck { get; set; }
        public Record ToRecord() => new Record
        {
                MemberID = api_member_id,
                Exp = api_experience[0],
                Name = api_nickname,
                Comment = api_cmt
        };
    }
    public sealed class API_Deck
    {
        public List<API_Ship> api_ships { get; set; }
    }
    public sealed class API_Ship
    {
        public int api_id { get; set; }
        public int api_ship_id { get; set; }
        public int api_level { get; set; }
        public int api_star { get; set; }
    }
    public sealed class API_Practice
    {
        public int api_count { get; set; }
        public string api_result_msg { get; set; }
        public API_EnemyInfo api_data { get; set; }
    }
    public sealed class API_Error
    {
        public int api_result { get; set; }
        public string api_result_msg { get; set; }
    }
}