using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Api.Utils;

namespace TrackingProject.Api.Controllers
{
    public class SmsController : Controller
    {
        public JsonResult Test()
        {
            SmsController con = new SmsController();
            var result = con.SendAudio(new Api.Models.SmsViewModel() { Message = "FATİH KOKAY isimli öğrenciniz servise bindi.", Phone = "5066059064" });

            return Json(new { Status = 1 ,result}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Send(SmsViewModel model)
        {
            return Json(new { Status = 1, Result = SmsSend(model.Phone, model.Message) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendAudio(SmsViewModel model) {
            var xml = "<?xml version='1.0'?><mainbody><header><company>Netgsm</company><usercode>8508404250</usercode><password>123580</password><startdate></startdate><starttime></starttime><stopdate></stopdate><stoptime></stoptime><key>0</key><version>1</version></header><body><audioid>10603977</audioid><no>" + model.Phone+"</no></body></mainbody>";

            return Json(new { Status = 1, Result = XMLPOST("https://api.netgsm.com.tr/xmlbulkhttppost_seslisms.asp", xml) }, JsonRequestBehavior.AllowGet);
        }

        public string SmsSend(string phone, string message)
        {
            String[] phoneArray = new String[] { phone };

            var client = new Netgsm.smsnnClient();
            return karakterCevir(message)+"&&&&" +client.smsGonder1NV2("8508404250", "123580", "F.YANARDAG", karakterCevir(message), phoneArray, "", "", "", "", 0);
        }

        public static string karakterCevir(string kelime)
        {
            string mesaj = kelime;
            char[] oldValue = new char[] { 'ö', 'Ö', 'ü', 'Ü', 'ç', 'Ç', 'İ', 'ı', 'Ğ', 'ğ', 'Ş', 'ş' };
            char[] newValue = new char[] { 'o', 'O', 'u', 'U', 'c', 'C', 'I', 'i', 'G', 'g', 'S', 's' };
            for (int sayac = 0; sayac < oldValue.Length; sayac++)
            {
                mesaj = mesaj.Replace(oldValue[sayac], newValue[sayac]);
            }
            return mesaj;
        }

        private string XMLPOST(string PostAddress, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(PostAddress) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResponse = wUpload.UploadData(PostAddress, "POST", bPostArray);
                Char[] sReturnChars = Encoding.UTF8.GetChars(bResponse);
                string sWebPage = new string(sReturnChars);
                return sWebPage;
            }
            catch
            {
                return "-1";
            }
        }
    }
}