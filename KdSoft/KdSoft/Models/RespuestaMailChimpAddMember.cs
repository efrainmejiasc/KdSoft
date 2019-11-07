using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class RespuestaMailChimpAddMember
    {
        public string id { get; set; }
        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public int web_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public MergeFields merge_fields { get; set; }
        public Stats stats { get; set; }
        public string ip_signup { get; set; }
        public string timestamp_signup { get; set; }
        public string ip_opt { get; set; }
        public DateTime timestamp_opt { get; set; }
        public int member_rating { get; set; }
        public DateTime last_changed { get; set; }
        public string language { get; set; }
        public bool vip { get; set; }
        public string email_client { get; set; }
        public Location location { get; set; }
        public string source { get; set; }
        public int tags_count { get; set; }
        public List<object> tags { get; set; }
        public string list_id { get; set; }
        public List<Link> _links { get; set; }

        public class MergeFields
        {
            public string FNAME { get; set; }
            public string LNAME { get; set; }
            public string ADDRESS { get; set; }
            public string PHONE { get; set; }
            public string BIRTHDAY { get; set; }
        }

        public class Stats
        {
            public int avg_open_rate { get; set; }
            public int avg_click_rate { get; set; }
        }

        public class Location
        {
            public int latitude { get; set; }
            public int longitude { get; set; }
            public int gmtoff { get; set; }
            public int dstoff { get; set; }
            public string country_code { get; set; }
            public string timezone { get; set; }
        }

        public class Link
        {
            public string rel { get; set; }
            public string href { get; set; }
            public string method { get; set; }
            public string targetSchema { get; set; }
            public string schema { get; set; }
        }

    }
}
