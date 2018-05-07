using System.Collections.Generic;

namespace KCSpy.Model
{
    public sealed class UserKit
    {
        public int api_member_id { get; set; }
        public List<int> api_experience { get; set; }
        public string api_nickname { get; set; }
        public string api_cmt { get; set; }
        public List<int> api_ship { get; set; }
        public int api_level { get; set; }
    }

    public sealed class UserKitCover
    {
        public int api_count { get; set; }
        public string api_result_msg { get; set; }
        public UserKit api_data { get; set; }
    }

    public sealed class ErrorKit
    {
        public int api_result { get; set; }
        public string api_result_msg { get; set; }
    }
}