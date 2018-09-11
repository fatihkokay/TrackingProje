using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.WebSite.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow) : base(uow)
        {
                
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}