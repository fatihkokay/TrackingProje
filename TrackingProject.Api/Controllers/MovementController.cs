using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Api.Models;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.MovementServices;

namespace TrackingProject.Api.Controllers
{
    public class MovementController : BaseController
    {
        private readonly IMovementService _MovementService;

        public MovementController(IMovementService movementService,IUnitOfWork uow) : base(uow)
        {
            _MovementService = movementService;
        }

        public ActionResult RouteMovementList(int RouteId)
        {
            var model = _MovementService.GetAll().Where(m => m.RouteId == RouteId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Select(m => new MovementViewModel()
            {
                Id = m.Id,
                Date = m.Date,
                RouteId = m.RouteId,
                InCar = m.InCar,
                InCarDate = m.InCarDate,
                InCarNotification = m.InCarNotification,
                InCarNotificationDate = m.InCarNotificationDate,
                InCarSchool = m.InCarSchool,
                InCarSchoolDate = m.InCarSchoolDate,
                InCarSchoolNotification = m.InCarSchoolNotification,
                InCarSchoolNotificationDate = m.InCarSchoolNotificationDate,
                OutCar = m.OutCar,
                OutCarDate = m.OutCarDate,
                OutCarNotification = m.OutCarNotification,
                OutCarNotificationDate = m.OutCarNotificationDate,
                StudentId = m.StudentId
            }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Movement
        public ActionResult InCarNotification(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.InCarNotification = Model.InCarNotification;
                    Entity.InCarNotificationDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.InCarNotification = Entity.InCarNotification;
                    Entity.InCarNotificationDate = DateTime.Now;


                    _MovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InCar(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.InCar = Model.InCar;
                    Entity.InCarDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.InCar = Model.InCar;
                    Entity.InCarDate = DateTime.Now;


                    _MovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InCarSchool(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.Date = DateTime.Now;
                    Entity.InCarSchool = Model.InCarSchool;
                    Entity.InCarSchoolDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.InCarSchool = Model.InCarSchool;
                    Entity.InCarSchoolDate = DateTime.Now;


                    _MovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InCarSchoolNotification(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.Date = DateTime.Now;
                    Entity.InCarSchoolNotification = Model.InCarSchool;
                    Entity.InCarSchoolNotificationDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.InCarSchoolNotification = Model.InCarSchool;
                    Entity.InCarSchoolNotificationDate = DateTime.Now;


                    _MovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OutCarNotification(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.OutCarNotification = Model.OutCarNotification;
                    Entity.OutCarNotificationDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.OutCarNotification = Model.OutCarNotification;
                    Entity.OutCarNotificationDate = DateTime.Now;

                    _MovementService.Insert(Entity);
                    _uow.SaveChanges();
                }

                return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = 0, ErrorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OutCar(MovementViewModel Model)
        {
            try
            {
                if (_MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId && (m.Date.Year == DateTime.Now.Year && m.Date.Month == DateTime.Now.Month && m.Date.Day == DateTime.Now.Day)).Any())
                {
                    Movement Entity = _MovementService.GetAll().Where(m => m.RouteId == Model.RouteId && m.StudentId == Model.StudentId).First();
                    Entity.OutCar = Model.OutCar;
                    Entity.OutCarDate = DateTime.Now;

                    _MovementService.Update(Entity);
                    _uow.SaveChanges();
                }
                else
                {
                    Movement Entity = new Movement();
                    Entity.RouteId = Model.RouteId;
                    Entity.StudentId = Model.StudentId;
                    Entity.Date = DateTime.Now;
                    Entity.OutCar = Model.OutCar;
                    Entity.OutCarDate = DateTime.Now;


                    _MovementService.Insert(Entity);
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