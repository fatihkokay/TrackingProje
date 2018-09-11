using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.RouteServices;

namespace TrackingProject.Api.Controllers
{
    public class RouteController : BaseController
    {
        private readonly IRouteService _RouteService;
        private readonly IRouteLineService _RouteLineService;
        private readonly IRouteMovementService _RouteMovementService;

        public RouteController(IRouteService routeService, IRouteLineService routeLineService, IRouteMovementService routeMovementService, IUnitOfWork uow) : base(uow)
        {
            _RouteService = routeService;
            _RouteLineService = routeLineService;
            _RouteMovementService = routeMovementService;
        }

        // GET: DriverRoute
        public JsonResult DriverRoute(int DriverId)
        {
            var model = _RouteService.GetAll().Where(m => m.DriverId == DriverId).Select(m => new RouteViewModel()
            {
                DriverId = m.DriverId,
                FirmId = m.FirmId,
                RouteType = m.RouteType,
                Id = m.Id,
                Name = m.Name,
                StartTime = m.StartTime,
                SchoolExitTime = m.SchoolExitTime,
                RouteMovement = m.RouteMovementList.Where(z => z.Date.Month == DateTime.Now.Month && z.Date.Day == DateTime.Now.Day).Select(d => new RouteMovementViewModel()
                {
                    Date = d.Date,
                    RouteId = d.RouteId,
                    Status = d.Status,
                    SchoolExitStatus = d.SchoolExitStatus
                }).FirstOrDefault()
            }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: DriverRouteStudent
        public JsonResult DriverRouteStudent(int DriverId, int RouteId = 0, int RouteType = -1)
        {
            RouteViewModel model = _RouteService.GetAll().Where(m => m.DriverId == DriverId && m.Id == (RouteId == 0 ? m.Id : RouteId) && m.RouteType == (RouteType == -1 ? m.RouteType : RouteType)).Select(m => new RouteViewModel()
            {
                DriverId = m.DriverId,
                FirmId = m.FirmId,
                RouteType = m.RouteType,
                Id = m.Id,
                Name = m.Name,
                StartTime = m.StartTime,
                SchoolExitTime = m.SchoolExitTime,
                RouteLineList = m.RouteLineList.OrderBy(z => z.CreateDateTime).Select(x => new RouteLineViewModel()
                {
                    Id = x.Id,
                    Latitute = x.Latitute,
                    Longitude = x.Longitude,
                    RouteId = x.RouteId,
                    LineType = x.LineType,
                    StudentId = x.StudentId,
                    CreateDateTime = x.CreateDateTime,
                }).ToList(),
                RouteStudentList = m.RouteStudentList.Select(x => new RouteStudentViewModel()
                {
                    Id = x.Id,
                    RouteId = x.RouteId,
                    StudentId = x.StudentId,
                    Student = new StudentViewModel
                    {
                        Active = x._Student.Active,
                        FirmId = x._Student.FirmId,
                        Id = x._Student.Id,
                        Name = x._Student.Name,
                        Surname = x._Student.Surname,
                        Address = x._Student.Address,
                        FirmName = x._Student._Firm.Name,
                        Phone = x._Student.Phone,
                        Distance = x._Student.Distance
                    }
                }).ToList(),
                RouteMovement = m.RouteMovementList.Where(z => z.Date.Year == DateTime.Now.Year && z.Date.Month == DateTime.Now.Month && z.Date.Day == DateTime.Now.Day).Select(d => new RouteMovementViewModel()
                {
                    Date = d.Date,
                    RouteId = d.RouteId,
                    Status = d.Status,
                    SchoolExitStatus = d.SchoolExitStatus
                }).FirstOrDefault()
            }).First();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DriverRouteStudentInMovement(int DriverId, int RouteId = 0, int RouteType = -1)
        {
            RouteViewModel model = _RouteService.GetAll().Where(m => m.DriverId == DriverId && m.Id == (RouteId == 0 ? m.Id : RouteId) && m.RouteType == (RouteType == -1 ? m.RouteType : RouteType)).Select(m => new RouteViewModel()
            {
                DriverId = m.DriverId,
                FirmId = m.FirmId,
                RouteType = m.RouteType,
                Id = m.Id,
                Name = m.Name,
                StartTime = m.StartTime,
                SchoolExitTime = m.SchoolExitTime,
                RouteLineList = m.RouteLineList.OrderBy(z => z.CreateDateTime).Select(x => new RouteLineViewModel()
                {
                    Id = x.Id,
                    Latitute = x.Latitute,
                    Longitude = x.Longitude,
                    RouteId = x.RouteId,
                    LineType = x.LineType,
                    StudentId = x.StudentId,
                    CreateDateTime = x.CreateDateTime,
                }).ToList(),
                RouteStudentList = m.RouteStudentList.Select(x => new RouteStudentViewModel()
                {
                    Id = x.Id,
                    RouteId = x.RouteId,
                    StudentId = x.StudentId,
                    Student = new StudentViewModel
                    {
                        Active = x._Student.Active,
                        FirmId = x._Student.FirmId,
                        Id = x._Student.Id,
                        Name = x._Student.Name,
                        Surname = x._Student.Surname,
                        Address = x._Student.Address,
                        FirmName = x._Student._Firm.Name,
                        Phone = x._Student.Phone,
                        Distance = x._Student.Distance
                    }
                }).ToList(),
                RouteMovement = m.RouteMovementList.Where(z => z.Date.Year == DateTime.Now.Year && z.Date.Month == DateTime.Now.Month && z.Date.Day == DateTime.Now.Day).Select(d => new RouteMovementViewModel()
                {
                    Date = d.Date,
                    RouteId = d.RouteId,
                    Status = d.Status,
                    SchoolExitStatus = d.SchoolExitStatus
                }).FirstOrDefault(),
                MovementList = m.MovementList.Where(i=> i.Date.Year == DateTime.Now.Year && i.Date.Month == DateTime.Now.Month && i.Date.Day == DateTime.Now.Day).Select(d=> new  MovementViewModel()
                {
                    Id = d.Id,
                    Date = d.Date,
                    RouteId = d.RouteId,
                    InCar = d.InCar,
                    InCarDate = d.InCarDate,
                    InCarNotification = d.InCarNotification,
                    InCarNotificationDate = d.InCarNotificationDate,
                    InCarSchool = d.InCarSchool,
                    InCarSchoolDate = d.InCarSchoolDate,
                    InCarSchoolNotification = d.InCarSchoolNotification,
                    InCarSchoolNotificationDate = d.InCarSchoolNotificationDate,
                    OutCar = d.OutCar,
                    OutCarDate = d.OutCarDate,
                    OutCarNotification = d.OutCarNotification,
                    OutCarNotificationDate = d.OutCarNotificationDate,
                    StudentId = d.StudentId
                }).ToList()
            }).First();

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Line(RouteLineViewModel Model)
        {
            try
            {
                if (_RouteLineService.GetAll().Where(m => m.StudentId == Model.StudentId && m.LineType == Model.LineType && m.RouteId == Model.RouteId).Any())
                {
                    RouteLine Entity = _RouteLineService.GetAll().Where(m => m.StudentId == Model.StudentId && m.LineType == Model.LineType && m.RouteId == Model.RouteId).First();
                    Entity.LineType = Model.LineType;
                    Entity.StudentId = Model.StudentId;
                    Entity.Latitute = Model.Latitute;
                    Entity.Longitude = Model.Longitude;
                    Entity.RouteId = Model.RouteId;
                    Entity.CreateDateTime = DateTime.Now;

                    _RouteLineService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    RouteLine Entity = new RouteLine();
                    Entity.LineType = Model.LineType;
                    Entity.StudentId = Model.StudentId;
                    Entity.Latitute = Model.Latitute;
                    Entity.Longitude = Model.Longitude;
                    Entity.RouteId = Model.RouteId;
                    Entity.CreateDateTime = DateTime.Now;

                    _RouteLineService.Insert(Entity);
                    _uow.SaveChanges();
                }




                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RouteMovementChange(RouteMovement Model)
        {
            try
            {
                if (_RouteMovementService.GetAll().Where(m => m.RouteId == Model.RouteId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    RouteMovement Entity = _RouteMovementService.GetAll().Where(m => m.RouteId == Model.RouteId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).First();
                    Entity.Status = Model.Status;

                    _RouteMovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    RouteMovement Entity = new RouteMovement();
                    Entity.RouteId = Model.RouteId;
                    Entity.Status = Model.Status;
                    Entity.SchoolExitStatus = -1;
                    Entity.Date = DateTime.Now;


                    _RouteMovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RouteMovementSchoolExitChange(RouteMovement Model)
        {
            try
            {
                if (_RouteMovementService.GetAll().Where(m => m.RouteId == Model.RouteId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    RouteMovement Entity = _RouteMovementService.GetAll().Where(m => m.RouteId == Model.RouteId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).First();
                    Entity.SchoolExitStatus = Model.SchoolExitStatus;

                    _RouteMovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    RouteMovement Entity = new RouteMovement();
                    Entity.Status = -1;
                    Entity.SchoolExitStatus = Model.SchoolExitStatus;
                    Entity.Date = DateTime.Now;


                    _RouteMovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}