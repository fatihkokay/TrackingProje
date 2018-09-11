using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.UserServices;
using TrackingProject.WebSite.ComplexType;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserService userService;

        public LoginController(IUserService _userService, IUnitOfWork uow) : base(uow)
        {
            userService = _userService;
        }
        public ActionResult Index()
        {

            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            var checkUser = userService.GetAll().Where(i => i.Username == model.User.Username && i.Password == model.User.Password).FirstOrDefault();
            if (checkUser != null)
            {
                List<string> Roles = new List<string>();
                var identityInfo = new CtIdentity()
                {
                    Users = checkUser,
                    //Roles
                };

                var time = DateTime.Now;
                var data = JsonConvert.SerializeObject(identityInfo);
                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, 
                    checkUser.Username,
                    time,
                    time.AddHours(1),
                    model.RememberMe,
                    data,
                    FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(
                    FormsAuthentication.FormsCookieName,
                    hash);

                // Cookie ye süre tanımlaması
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
                // Cookie içerisine verilerin yüklenmesi
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket),
                    Roles.ToArray());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.Message = "Hatalı giriş, bilgilerinizi kontrol ediniz.";
            }
            return View(model);
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, "INVALID"));
            System.Web.HttpContext.Current.Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}