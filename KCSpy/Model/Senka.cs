using System.Collections.Generic;

namespace KCSpy.Model
{
    public class SenkaItem
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
    }

    public class SenkaData
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
        public List<SenkaItem> api_list{ get; set; }
    }

    public class SenkaCover
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
        public SenkaData api_data { get; set; }
    }
}