using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace TrackingProject.Api.Utils
{
    public class Sms
    {
        public void IstegiGonder(SmsIstegi istek)
        {

            string payload = JsonConvert.SerializeObject(istek);

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            wc.Headers["Content-Type"] = "application/json";

            try
            {
                string campaign_id = wc.UploadString("http://sms.verimor.com.tr/v2/send.json", payload);
            }
            catch (WebException ex) // 400 hatalarında response body'de hatanın ne olduğunu yakalıyoruz
            {
                if (ex.Status == WebExceptionStatus.ProtocolError) // 400 hataları
                {
                    var responseBody = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                }

                else // diğer hatalar
                {


                }
            }

        } 
    }

    public class Mesaj
    {
        public Mesaj(string msg, string dest)
        {
            this.msg = msg;
            this.dest = dest;
        }

        public string msg { get; set; }
        public string dest { get; set; }
    }



    public class SmsIstegi
    {
        public string username { get; set; }
        public string password { get; set; }
        public string source_addr { get; set; }
        public Mesaj[] messages { get; set; }
    }
}