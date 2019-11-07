using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class SendiBlueTransaccional
    {
        public Sender sender { get; set; }

        public List<To> to { get; set; }

        public int templateId { get; set; }

        public Params paramse {get;set;}

        public Headers headers { get; set; }

        public class Sender
        {
            public string name { get; set; }

            public string email { get; set; }
        }

        public class To
        {
            public string email { get; set; }
            public string name { get; set; }
        }

        public class Params
        {
            public string name { get; set; }
            public string surname { get; set; }
        }
        public class Headers
        {
            public string xxxx { get; set; }
            public string charset { get; set; }
    }


}
}
