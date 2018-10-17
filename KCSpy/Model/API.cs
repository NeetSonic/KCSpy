using System.Collections.Generic;

namespace KCSpy.Model
{
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