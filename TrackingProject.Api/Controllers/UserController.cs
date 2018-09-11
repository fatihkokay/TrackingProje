using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.DriverServices;

namespace TrackingProject.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IDriverService _DriverService;
        public UserController(IDriverService driverService, IUnitOfWork uow)
        {
            _DriverService = driverService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Test()
        {
            if (_DriverService.GetAll().Where(m => m.DevinceId == "e38160991931a7e0").Any())//model.DevinceId).Any())
            {
                DriverViewModel result = _DriverService.GetAll().Where(m => m.DevinceId == "e38160991931a7e0").Select(m => new DriverViewModel()
                {
                    Active = m.Active,
                    Code = m.Code,
                    FirmId = m.FirmId,
                    Id = m.Id,
                    Name = m.Name,
                    Password = m.Password,
                    Surname = m.Surname,
                    Username = m.Username,
                    DevinceId = m.DevinceId,
                    RouteList = m.RouteList.Select(x => new RouteViewModel()
                    {
                        DriverId = x.DriverId,
                        FirmId = x.FirmId,
                        RouteType = x.RouteType,
                        Id = x.Id,
                        Name = x.Name,
                        StartTime = x.StartTime,
                    }).ToList()
                }).First();

                return Json(new { Status = 1, Data = result, ErrorCode = 0, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Status = 0, Data = new DriverViewModel(), ErrorCode = 0, ErrorMessage = "Cihaza tanımlı bir kullanıcı bulunamadı!!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}