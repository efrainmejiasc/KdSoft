using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class RespuestaMailChimp
    {
        public string account_id { get; set; }
        public string login_id { get; set; }
        public string account_name { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public string avatar_url { get; set; }
        public string role { get; set; }
        public DateTime member_since { get; set; }
        public string pricing_plan_type { get; set; }
        public string first_payment { get; set; }
        public string account_timezone { get; set; }
        public string account_industry { get; set; }
        public Contact contact { get; set; }
        public bool pro_enabled { get; set; }
        public DateTime last_login { get; set; }
        public int total_subscribers { get; set; }
        public List<Link> _links { get; set; }

        public class Contact
        {
            public string company { get; set; }
            public string addr1 { get; set; }
            public string addr2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
            public string country { get; set; }
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
