using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using KdSoft.Models;

namespace KdSoft.Engine
{
    public class EngineDb
    {
        public static string DefaultConnection { get; set; }
        private SqlConnection Conexion = new SqlConnection(EngineDb.DefaultConnection);

        public List<SendiBlueContact> GetSendiBlueContacts ()
        {
            List<SendiBlueContact> dataList = new List<SendiBlueContact>();

            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetSendiBlueContact", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    SendiBlueContact data = new SendiBlueContact();
                    data.Id = lector.GetInt32(0);
                    data.Name = lector.GetString(1);
                    data.SurName = lector.GetString(2);
                    data.Sms = lector.GetString(3);
                    data.Email = lector.GetString(4);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }

            return dataList;
        }



        public bool InsertSendiBlueCampaing (List<SendiBlueCampaing> modelo)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                foreach (SendiBlueCampaing model in modelo)
                {
                    SqlCommand command = new SqlCommand("Sp_InsertSendiBlueCampaing", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IdSendiBlueContact", model.IdSendiBlueContact);
                    command.Parameters.AddWithValue("@NameCampaing", model.NameCampaing);
                    command.Parameters.AddWithValue("@HtmlCode", model.HtmlCode);
                    command.Parameters.AddWithValue("@DateSend", model.DateSend);
                    command.ExecuteNonQuery();
                    resultado = true;
                }
                Conexion.Close();
            }

            return resultado;
        }

    }
}
