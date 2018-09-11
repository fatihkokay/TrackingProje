using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.MovementServices;
using TrackingProject.Service.RouteServices;
using TrackingProject.WebSite.ComplexType;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRouteService _RouteService;
        private readonly IMovementService _MovementService;
        public ReportController(IRouteService routeService, IMovementService movementService, IUnitOfWork uow) : base(uow)
        {
            _RouteService = routeService;
            _MovementService = movementService;
        }
        public ActionResult RollCall()
        {
            var model = new ReportViewModel();
            model.Routes = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Routes.AddRange(_RouteService.GetAll().OrderBy(i => i.Name)
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() }).ToList());
            return View(model);
        }
        [HttpPost]
        public string RollCallAjax(ReportViewModel model)
        {
            var data = _MovementService.GetAll();
            if (!string.IsNullOrEmpty(Request.QueryString["DateRange"]))
            {
                var date1 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[0]);
                var date2 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[1]).AddHours(23).AddMinutes(59).AddSeconds(59);

                data = data.Where(i => i.Date >= date1 && i.Date <= date2);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["RouteId"]))
            {
                var routeId = int.Parse(Request.QueryString["RouteId"]);
                data = data.Where(i => i.RouteId == routeId);
            }
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();
            var afterOtherColumn = "";
            if (sortColumn == "Student_Name" || sortColumn == "Route_Name" || sortColumn == "Date_Short")
            {
                afterOtherColumn = sortColumn + " " + sortColumnDirection;
                sortColumn = "";
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m._Route.Name.Contains(searchValue) || m._Student.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Select(i => new CtMovement()
            {
                RouteId = i.RouteId,
                Route_DriverId = i._Route.DriverId,
                Route_FirmId = i._Route.FirmId,
                Route_Name = i._Route.Name,
                StudentId = i.StudentId,
                Student_FirmId = i._Student.FirmId,
                Student_Name = i._Student.Name + " "+i._Student.Surname,
                Student_Surname = i._Student.Surname,
                Date=i.Date,
                InCar = i.InCar,
                InCarDate =i.InCarDate,
                Id = i.Id,

            });
            if (!string.IsNullOrEmpty(afterOtherColumn))
            {
                list = list.OrderBy(afterOtherColumn);
            }
            var result = list.Skip(model.start).Take(model.length).ToList();
            return JsonConvert.SerializeObject(new { draw = model.draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = result }, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm" });
        }

        public ActionResult Duration()
        {
            var model = new ReportViewModel();
            model.Routes = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Routes.AddRange(_RouteService.GetAll().OrderBy(i => i.Name)
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() }).ToList());
            return View(model);
        }
        [HttpPost]
        public string DurationAjax(ReportViewModel model)
        {
            var data = _MovementService.GetAll();
            if (!string.IsNullOrEmpty(Request.QueryString["DateRange"]))
            {
                var date1 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[0]);
                var date2 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[1]).AddHours(23).AddMinutes(59).AddSeconds(59);

                data = data.Where(i => i.Date >= date1 && i.Date <= date2 && i.OutCarDate.HasValue && i.InCarDate.HasValue);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["RouteId"]))
            {
                var routeId = int.Parse(Request.QueryString["RouteId"]);
                data = data.Where(i => i.RouteId == routeId);
            }
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();
            var afterOtherColumn = "";
            if (sortColumn == "Student_Name" || sortColumn == "Route_Name" || sortColumn == "Date_Short")
            {
                afterOtherColumn = sortColumn + " " + sortColumnDirection;
                sortColumn = "";
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m._Route.Name.Contains(searchValue) || m._Student.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Select(i => new CtMovement()
            {
                RouteId = i.RouteId,
                Route_DriverId = i._Route.DriverId,
                Route_FirmId = i._Route.FirmId,
                Route_Name = i._Route.Name,
                StudentId = i.StudentId,
                Student_FirmId = i._Student.FirmId,
                Student_Name = i._Student.Name + " " + i._Student.Surname,
                Student_Surname = i._Student.Surname,
                Date = i.Date,
                InCar = i.InCar,
                InCarDate = i.InCarDate,
                Id = i.Id,
                OutCarDate=i.OutCarDate                
            });
            if (!string.IsNullOrEmpty(afterOtherColumn))
            {
                list = list.OrderBy(afterOtherColumn);
            }
            var result = list.Skip(model.start).Take(model.length).ToList();
            result.ForEach(i => i.Duration = BaseUtils.DurationCalc(i.InCarDate.Value, i.OutCarDate.Value));
            return JsonConvert.SerializeObject(new { draw = model.draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = result }, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm" });
        }

        public ActionResult RouteDuration()
        {
            var model = new ReportViewModel();
            model.Routes = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Routes.AddRange(_RouteService.GetAll().OrderBy(i => i.Name)
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() }).ToList());
            return View(model);
        }
        [HttpPost]
        public string RouteDurationAjax(ReportViewModel model)
        {
            var data = _MovementService.GetAll();
            if (!string.IsNullOrEmpty(Request.QueryString["DateRange"]))
            {
                var date1 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[0]);
                var date2 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[1]).AddHours(23).AddMinutes(59).AddSeconds(59);

                data = data.Where(i => i.Date >= date1 && i.Date <= date2 && i.InCarSchoolDate.HasValue && i.InCarDate.HasValue);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["RouteId"]))
            {
                var routeId = int.Parse(Request.QueryString["RouteId"]);
                data = data.Where(i => i.RouteId == routeId);
            }
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();
            var afterOtherColumn = "";
            if (sortColumn == "Student_Name" || sortColumn == "Route_Name" || sortColumn == "Date_Short")
            {
                afterOtherColumn = sortColumn + " " + sortColumnDirection;
                sortColumn = "";
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m._Route.Name.Contains(searchValue) || m._Student.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Select(i => new CtMovement()
            {
                RouteId = i.RouteId,
                Route_DriverId = i._Route.DriverId,
                Route_FirmId = i._Route.FirmId,
                Route_Name = i._Route.Name,
                StudentId = i.StudentId,
                Student_FirmId = i._Student.FirmId,
                Student_Name = i._Student.Name + " " + i._Student.Surname,
                Student_Surname = i._Student.Surname,
                Date = i.Date,
                InCar = i.InCar,
                InCarDate = i.InCarDate,
                Id = i.Id,
                InCarSchoolDate = i.InCarSchoolDate
            });
            if (!string.IsNullOrEmpty(afterOtherColumn))
            {
                list = list.OrderBy(afterOtherColumn);
            }
            var result = list.Skip(model.start).Take(model.length).ToList();
            result.ForEach(i => i.Duration = BaseUtils.DurationCalc(i.InCarDate.Value, i.InCarSchoolDate.Value));
            return JsonConvert.SerializeObject(new { draw = model.draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = result }, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm" });
        }

        public ActionResult Notify()
        {
            var model = new ReportViewModel();
            model.Routes = new List<SelectListItem>() { new SelectListItem() { Value = "", Text = "Tümü" } };
            model.Routes.AddRange(_RouteService.GetAll().OrderBy(i => i.Name)
                .Select(i => new SelectListItem() { Text = i.Name, Value = i.Id.ToString() }).ToList());
            return View(model);
        }
        [HttpPost]
        public string NotifyAjax(ReportViewModel model)
        {
            var data = _MovementService.GetAll();
            if (!string.IsNullOrEmpty(Request.QueryString["DateRange"]))
            {
                var date1 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[0]);
                var date2 = Convert.ToDateTime(Request.QueryString["DateRange"].Split('-')[1]).AddHours(23).AddMinutes(59).AddSeconds(59);

                data = data.Where(i => i.Date >= date1 && i.Date <= date2);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["RouteId"]))
            {
                var routeId = int.Parse(Request.QueryString["RouteId"]);
                data = data.Where(i => i.RouteId == routeId);
            }
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();
            var afterOtherColumn = "";
            if (sortColumn == "Student_Name" || sortColumn == "Route_Name" || sortColumn == "Date_Short")
            {
                afterOtherColumn = sortColumn + " " + sortColumnDirection;
                sortColumn = "";
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m._Route.Name.Contains(searchValue) || m._Student.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Select(i => new CtMovement()
            {
                RouteId = i.RouteId,
                Route_DriverId = i._Route.DriverId,
                Route_FirmId = i._Route.FirmId,
                Route_Name = i._Route.Name,
                StudentId = i.StudentId,
                Student_FirmId = i._Student.FirmId,
                Student_Name = i._Student.Name + " " + i._Student.Surname,
                Student_Surname = i._Student.Surname,
                Date = i.Date,
                InCar = i.InCar,
                InCarDate = i.InCarDate,
                Id = i.Id,
                InCarNotificationDate = i.InCarNotificationDate,
                InCarSchoolNotificationDate = i.InCarSchoolNotificationDate,
                OutCarNotificationDate = i.OutCarNotificationDate
            });
            if (!string.IsNullOrEmpty(afterOtherColumn))
            {
                list = list.OrderBy(afterOtherColumn);
            }
            var result = list.Skip(model.start).Take(model.length).ToList();
            return JsonConvert.SerializeObject(new { draw = model.draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = result }, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy HH:mm" });
        }
    }
}