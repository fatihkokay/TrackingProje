using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;
        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.GetRequiredString("controller");
            if (BaseUtils.Users == null && controller != "Login")
            {
                filterContext.Result = new RedirectResult("~/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}