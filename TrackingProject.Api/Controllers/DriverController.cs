using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.DriverServices;

namespace TrackingProject.Api.Controllers
{
    public class DriverController : BaseController
    {
        private readonly IDriverService _DriverService;
        public DriverController(IDriverService driverService, IUnitOfWork uow) : base(uow)
        {
            _DriverService = driverService;
        }

        // POST: Driver/Login
        [HttpPost]
        public JsonResult Login(DriverViewModel model)
        {
            try
            {
                if (_DriverService.GetAll().Where(m => m.DevinceId == model.DevinceId).Any())//model.DevinceId).Any())
                {
                    DriverViewModel result = _DriverService.GetAll().Where(m => m.DevinceId == model.DevinceId).Select(m => new DriverViewModel()
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
                            SchoolExitTime = x.SchoolExitTime,
                            RouteMovement = x.RouteMovementList.Where(z => z.Date.Month == DateTime.Now.Month && z.Date.Day == DateTime.Now.Day).Select(d => new RouteMovementViewModel()
                            {
                                Date = d.Date,
                                RouteId = d.RouteId,
                                Status = d.Status,
                                SchoolExitStatus = d.SchoolExitStatus
                            }).FirstOrDefault()
                        }).ToList(),

                    }).First();

                    return Json(new { Status = 1, Data = result, ErrorCode = 0, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = 0, Data = new DriverViewModel(), ErrorCode = 0, ErrorMessage = "Cihaza tanımlı bir kullanıcı bulunamadı!!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, Data = new DriverViewModel(), ErrorCode = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
