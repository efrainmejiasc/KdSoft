using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Models
{
    public class SendiBlueCampaing
    {
        public int Id { get; set; }

        public int IdSendiBlueContact { get; set; }

        public string NameCampaing { get; set; }

        public string HtmlCode { get; set; }

        public DateTime DateSend { get; set; }
    }
}
