using KdSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KdSoft.Engine
{
    public class EngineData
    {
        private static EngineData valor;
        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        public static string ClientIdMailChimp { get; set; }

        public static string ClientSecretMailChimp { get; set; }

        public static string PasswordMailChimp { get; set; }

        public static string ApiKeyMailChimp { get; set; }

        public static string ApiKeySendiBlue { get; set; }

        public static string ApiClaveSendiBlue { get; set; }

        public ContactsSendiBlue ListaSendiBlueContacto { get; set; }

    }
}
