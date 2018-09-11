using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.RouteServices;
using TrackingProject.Service.VehicleServices;
using TrackingProject.WebSite.Models;

namespace TrackingProject.WebSite.Controllers
{
    public class WatchController : BaseController
    {
        private readonly IRouteService _RouteService;
        private readonly IVehicleService _VehicleService;
        public WatchController(IVehicleService vehicleService, IRouteService routeService, IUnitOfWork uow) : base(uow)
        {
            _RouteService = routeService;
            _VehicleService = vehicleService;
        }
        public ActionResult Car()
        {
            var model = new WatchViewModel();
            model.Cars = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Cars.AddRange(_VehicleService.GetAll().OrderBy(i => i.Plate)
                .Select(i => new SelectListItem() { Text = i.Plate, Value = i.Id.ToString() }).ToList());
            return View(model);
        }

        public ActionResult Route()
        {
            var model = new WatchViewModel();
            model.Routes = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Routes.AddRange(_RouteService.GetAll().OrderBy(i => i.Name)
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() }).ToList());
            return View(model);
        }
    }
}