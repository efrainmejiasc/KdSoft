using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class ContactsSendiBlue
    {
        public List<Contact> contacts { get; set; }
        public int count { get; set; }

        public class Contact
        {
            public string email { get; set; }
            public int id { get; set; }
            public bool emailBlacklisted { get; set; }
            public bool smsBlacklisted { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime modifiedAt { get; set; }
            public List<object> listIds { get; set; }
            public Attributes attributes { get; set; }
        }


        public class Attributes
        {
        }
    }
}
