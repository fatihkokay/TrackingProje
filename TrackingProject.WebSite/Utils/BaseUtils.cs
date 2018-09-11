using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using TrackingProject.WebSite.ComplexType;

namespace TrackingProject.WebSite.Utils
{
    public static class BaseUtils
    {
        public static string NextKeyCode(string KeyCode)
        {
            byte[] ASCIIValues = Encoding.UTF8.GetBytes(KeyCode.ToUpper());
            int StringLength = ASCIIValues.Length;
            bool isAllZed = true;
            bool isAllNine = true;

            for (int i = 0; i < StringLength - 1; i++)
            {
                if (ASCIIValues[i] != 90)
                {
                    isAllZed = false;
                    break;
                }
            }
            if (isAllZed && ASCIIValues[StringLength - 1] == 57)
            {
                ASCIIValues[StringLength - 1] = 64;
            }

            for (int i = 0; i < StringLength; i++)
            {
                if (ASCIIValues[i] != 57)
                {
                    isAllNine = false;
                    break;
                }
            }
            if (isAllNine)
            {
                ASCIIValues[StringLength - 1] = 47;
                ASCIIValues[0] = 65;
                for (int i = 1; i < StringLength - 1; i++)
                {
                    ASCIIValues[i] = 48;
                }
            }

            for (int i = StringLength; i > 0; i--)
            {
                if (i - StringLength == 0)
                {
                    ASCIIValues[i - 1] += 1;
                }
                if (ASCIIValues[i - 1] == 58)
                {
                    ASCIIValues[i - 1] = 48;
                    if (i - 2 == -1)
                    {
                        break;
                    }
                    ASCIIValues[i - 2] += 1;
                }
                else if (ASCIIValues[i - 1] == 91)
                {
                    ASCIIValues[i - 1] = 65;
                    if (i - 2 == -1)
                    {
                        break;
                    }
                    ASCIIValues[i - 2] += 1;

                }
                else
                {
                    break;
                }

            }
            KeyCode = System.Text.Encoding.UTF8.GetString(ASCIIValues);
            return KeyCode;
        }

        public static CtIdentity Users
        {
            get
            {
                //User Bilgilerini Taşımak İçin Ticketa Ulaştım
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var claimsIdentity = HttpContext.Current.User.Identity as System.Security.Claims.ClaimsIdentity;
                    CtIdentity userData = new CtIdentity();
                    DateTime authTime = DateTime.MinValue;
                    if (claimsIdentity != null)
                    {
                        var c = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name);

                        if (!string.IsNullOrEmpty(c.ToString()))
                        {
                            var x = (FormsIdentity)c.Subject;
                            userData = JsonConvert.DeserializeObject<CtIdentity>(x.Ticket.UserData);
                            return userData;
                        }
                    }
                }
                return null;
            }

        }
        /// <summary>
        /// 0|Default,  1|Başarılı, 2|Uyarı,    3|Hata
        /// </summary>
        /// <param name="title">ss</param>
        /// <param name="msg">dd</param>
        /// <param name="type">1 Başarılı</param>
        /// <returns>fsf</returns>
        public static string SweetAlert(string title, string msg, int type = 0)
        {
            var vtype = ",type:";
            switch (type)
            {
                case 1:
                    vtype += "'success'";
                    break;
                case 2:
                    vtype += "'warning'";
                    break;
                case 3:
                    vtype += "'error'";
                    break;
                case 0:
                    vtype = "";
                    break;
            }
            return "swal({ title: '" + title + "', text: '" + msg + "' " + vtype + " });";
        }

        public static string DurationCalc(DateTime t1, DateTime t2)
        {
            var duration = "";
            TimeSpan calc = t2 - t1;

            var h = calc.Hours;
            var mm = calc.Minutes;
            var ss = calc.Seconds;

            return (h > 0 ? h + " saat " : "") + (mm > 0 ? mm + " dk " : "") + (ss > 0 ? ss + " sn" : "");
        }

    }
}