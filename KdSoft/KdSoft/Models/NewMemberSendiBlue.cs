using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class NewMemberSendiBlue
    {
        public string email { get; set; }
        public bool updateEnabled { get; set; }
        public Attributes attributes { get; set; }

        public class Attributes
        {
            public string NOMBRE { get; set; }

            public string SURNAME { get; set; }

            public string SMS { get; set; }
        }
    }
}
