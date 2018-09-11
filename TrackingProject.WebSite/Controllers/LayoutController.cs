using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.WebSite.Controllers
{
    public class LayoutController : BaseController
    {
        public LayoutController(IUnitOfWork uow) : base(uow)
        {
        }

        public PartialViewResult Menu()
        {
            return PartialView("_Menu");
        }

        public PartialViewResult Header()
        {
            return PartialView("_Header");
        }
    }
}