using KdSoft.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KdSoft.Engine
{
    public class EngineProyect
    {
   

        //************************SENDIBLUE CREAR CONTACTO *********************************************************************************************************************************

        public async Task<string> CreateContactSendiBlue(string endPoint)
        {
            string respuesta = string.Empty;
            RespuestaMailChimp resultado = new RespuestaMailChimp();
            string jsonData = CreateMember();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", EngineData.ApiKeySendiBlue);
                HttpResponseMessage response = await client.PostAsync(endPoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    //Retorna solo el ID del nuevo contacto
                }
                else
                {
                    respuesta = response.IsSuccessStatusCode.ToString();
                }
            }
            return respuesta;
        }

        private string CreateMember()
        {
            NewMemberSendiBlue jsonData = new NewMemberSendiBlue()
            {
                email = "efrainmejiasnuevo2@hotmail.com",
                updateEnabled = false,
                attributes = new NewMemberSendiBlue.Attributes()
                {
                    NOMBRE = "EEMejias",
                    SURNAME = "MejiasCastillo",
                    SMS = "584127568941"
                }
            };
            string resultado = JsonConvert.SerializeObject(jsonData);
            return resultado;
        }

        //************************SENDIBLUE OBTENER LISTA DE CONTACTOS *********************************************************************************************************************************

        public async Task<string> GetAllContactSendiBlue(string endPoint)
        {
            EngineData Valor = EngineData.Instance();
            string respuesta = string.Empty;
            ContactsSendiBlue resultado = new ContactsSendiBlue();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", EngineData.ApiKeySendiBlue);
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    resultado = JsonConvert.DeserializeObject<ContactsSendiBlue>(respuesta);
                }
                else
                {
                    respuesta = response.IsSuccessStatusCode.ToString();
                }
            }
            Valor.ListaSendiBlueContacto = resultado;
            return respuesta;
        }

        //************************SENDIBLUE ENVIAR CORREO TRANSACCIONAL*********************************************************************************************************************************

        public async Task<string> SendMailSendiBlue(string endPoint)
        {
            string respuesta = string.Empty;
            RespuestaMailChimp resultado = new RespuestaMailChimp();
            string jsonData = CreateTransaccional();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", EngineData.ApiKeySendiBlue);
                HttpResponseMessage response = await client.PostAsync(endPoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    //Retorna solo el ID del nuevo contacto
                }
                else
                {
                    respuesta = response.IsSuccessStatusCode.ToString();
                }
            }
            return respuesta;
        }


        private string CreateTransaccional()
        {
            string resultado = string.Empty;
            SendiBlueTransaccional I = new SendiBlueTransaccional()
            {
                sender = new SendiBlueTransaccional.Sender()
                {
                    name = "SocialWifi",
                    email = "sudokuparatodos2@gmail.com"

                },
                to = ListaTo(),
                templateId = 8,
                paramse =new SendiBlueTransaccional.Params()
                {
                    name="santiago",
                    surname ="mejias"
                },
                headers = new SendiBlueTransaccional.Headers()
                {
                    xxxx = "custom_header_1:custom_value_1|custom_header_2:custom_value_2|custom_header_3:custom_value_3",
                    charset= "iso-8859-1"
                }
            };
            
            resultado = JsonConvert.SerializeObject(I);
            resultado = resultado.Replace("paramse", "params");
            resultado = resultado.Replace("xxxx", "X-Mailin-custom");
            return resultado;
        }


        private List<SendiBlueTransaccional.To> ListaTo()
        {
            EngineData Valor = EngineData.Instance();
            ContactsSendiBlue listaContacts = Valor.ListaSendiBlueContacto;
            List<SendiBlueTransaccional.To> l = new List<SendiBlueTransaccional.To>();
            string[] p = new string[2];
            foreach (ContactsSendiBlue.Contact item in listaContacts.contacts)
            {
                p = item.email.Split('@');
                SendiBlueTransaccional.To i = new SendiBlueTransaccional.To()
                {
                    email = item.email,
                    name = p[0]
                };
                l.Add(i);
             }
          
            return l;
        }

        //************************SENDIBLUE ENVIAR DESDE SERVIDOR SMTP *********************************************************************************************************************************
        public bool EnviarEmail()
        {
            bool result = false;
            try
            {

                MailMessage mensaje = new MailMessage();
                SmtpClient servidor = new SmtpClient();
                mensaje.From = new MailAddress("SocialWifi <sudokuparatodos2@gmail.com>");
                mensaje.Subject = "Test SMTP SendiBlue SocialWifi";
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = "Test SMTP SendiBlue SocialWifi";
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                mensaje.To.Add(new MailAddress("efrainmejiasc@yahoo.com"));
                servidor.Credentials = new System.Net.NetworkCredential("sudokuparatodos2@gmail.com", EngineData.ApiClaveSendiBlue);
                servidor.Port = 587;
                servidor.Host = "smtp-relay.sendinblue.com";
                servidor.EnableSsl = true;
                servidor.Send(mensaje);
                mensaje.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                string n = ex.ToString();
            }

            return result;

        }

        public bool EnviarEmail(string body)
        {
            bool result = false;
            EngineDb Metodo = new EngineDb();
            List<SendiBlueContact> Contact = Metodo.GetSendiBlueContacts();
            body = File.ReadAllText(body);
            try
            {

                MailMessage mensaje = new MailMessage();
                SmtpClient servidor = new SmtpClient();
                mensaje.From = new MailAddress("SocialWifi <sudokuparatodos2@gmail.com>");
                mensaje.Subject = "Test SMTP SendiBlue SocialWifi  Transaccional";
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = body;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                foreach(SendiBlueContact item in Contact)
                { 
                   mensaje.To.Add(new MailAddress(item.Email));
                }
                servidor.Credentials = new System.Net.NetworkCredential("sudokuparatodos2@gmail.com", EngineData.ApiClaveSendiBlue);
                //servidor.Credentials = new System.Net.NetworkCredential("sudokuparatodos2@gmail.com", "1234santiago");
                servidor.Port = 587;
                servidor.Host = "smtp-relay.sendinblue.com";
                //servidor.Host = "smtp.gmail.com";
                servidor.EnableSsl = true;
                servidor.Send(mensaje);
                mensaje.Dispose();
                EngineDb Metodo2 = new EngineDb();
                result = Metodo2.InsertSendiBlueCampaing(SaveInfoCampaingSend(Contact, body));
                
            }
            catch (Exception ex)
            {
                string n = ex.ToString();
            }

            return result;

        }


        private List<SendiBlueCampaing> SaveInfoCampaingSend (List<SendiBlueContact> Contact, string htmlCode)
        {
            List<SendiBlueCampaing> info = new List<SendiBlueCampaing>();
            int n = 0;
            foreach(SendiBlueContact item in Contact)
            {
                SendiBlueCampaing x = new SendiBlueCampaing();
                x.IdSendiBlueContact = item.Id;
                x.NameCampaing = "Test SocialWifi";
                x.HtmlCode = htmlCode;
                x.DateSend = DateTime.UtcNow;
                info.Insert(n, x);
                n++;
            }
            return info;
        }

        private List<string> EmailTo()
        {
            List<string> To = new List<string>();
            To.Add("efrainmejiasc@gmail.com");
            To.Add("efrainmejiasc@hotmail.com");
            To.Add("efrain.mejiac@kdsoft.io");
            To.Add("emcingenieriadesoftware@outlook.com");
            return To;
        }
        //***********************************************************SENDIBLUEND**********************************************************************************************
        //********************************************************************************************************************************************************************
        //********************************************************************************************************************************************************************


        //************************MAILCHIMP **********************************************************************************************************************************

        public async Task<string> LogMailChimp(string endPoint)
        {
            string respuesta = string.Empty;
            RespuestaMailChimp resultado = new RespuestaMailChimp();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(EngineData.ClientIdMailChimp + ":" + EngineData.ApiKeyMailChimp));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encoded);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("accept-language", "en_ES");
                HttpResponseMessage response = await client.GetAsync(endPoint);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    resultado = JsonConvert.DeserializeObject<RespuestaMailChimp>(respuesta);
                }
                else
                {
                    respuesta = response.IsSuccessStatusCode.ToString();
                }
            }
            return respuesta;
        }

        public async Task<string> AddMemberMailChimp(string endPoint)
        {
            string respuesta = string.Empty;
            RespuestaMailChimpAddMember resultado = new RespuestaMailChimpAddMember();
            string jsonData = DataMember();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(EngineData.ClientIdMailChimp + ":" + EngineData.ApiKeyMailChimp));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encoded);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("accept-language", "en_ES");
                HttpResponseMessage response = await client.PostAsync(endPoint, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    resultado = JsonConvert.DeserializeObject<RespuestaMailChimpAddMember>(respuesta);
                }
                else
                {
                    respuesta = response.IsSuccessStatusCode.ToString();
                }
            }
            return respuesta;
        }


        private string DataMember()
        {
            NewMemberMailChimp jsonData = new NewMemberMailChimp()
            {
                email_address = "efrainmejias@yahoo.com",
                status = "subscribed",
                merge_fields = new NewMemberMailChimp.MergeFields
                {
                    FNAME = "emejias",
                    LNAME = "castillo"
                }
            };
            string resultado = JsonConvert.SerializeObject(jsonData);
            return resultado;
        }




        //********************************************************************************************************************************************************************

        public async Task<string> GetToken (string endPoint)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(EngineData.ClientIdMailChimp + ":" + EngineData.ClientSecretMailChimp);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Add("accept-language", "en_ES");
                client.DefaultRequestHeaders.Add("accept", "application/json");
                Uri url = new Uri(endPoint, UriKind.Absolute);
                List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                HttpContent content = new FormUrlEncodedContent(formData);
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    respuesta = response.StatusCode.ToString();
                }
            }
            return respuesta;
        }
    }
}
