using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class NewMemberMailChimp
    {
        public string email_address { get; set; }
        public string status { get; set; }
        public MergeFields merge_fields { get; set; }

        public class MergeFields
        {
            public string FNAME { get; set; }
            public string LNAME { get; set; }
           /* public string ADDRESS { get; set; }
            public string PHONE { get; set; }
            public string BIRTHDAY { get; set; }*/
        }
    }
}
