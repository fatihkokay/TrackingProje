using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.VehicleServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IVehicleService _VehicleService;

        public VehicleController(IVehicleService VehicleService, IUnitOfWork uow) : base(uow)
        {
            _VehicleService = VehicleService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new VehicleViewModel() { Vehicle = new Core.Domain.Entity.Vehicle() };
            model.Vehicle.Active = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(VehicleViewModel model)
        {
            var checkVehicle = _VehicleService.GetAll().Any(i => i.Plate == model.Vehicle.Plate);
            if (checkVehicle)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Araç Daha Önceden Kaydedilmiş.", 2);
                return View(model);
            }
            else
            {
                try
                {
                    var newVehicle = new Vehicle()
                    {
                        Plate = model.Vehicle.Plate,
                        Code = BaseUtils.NextKeyCode(string.IsNullOrEmpty(_VehicleService.LastCode()) ? "0000" : _VehicleService.LastCode()),
                        Active = model.Vehicle.Active
                    };
                    _VehicleService.Insert(newVehicle);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Araç Kaydedildi.", 1);
                    return RedirectToAction("Index", "Vehicle");
                }
                catch (Exception ex)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Araç Kaydedilemedi.", 3);
                    return View(model);
                }
            }
        }

        public ActionResult Edit(int Id)
        {
            var model = new VehicleViewModel();
            model.Vehicle = _VehicleService.Find(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(VehicleViewModel model)
        {
            try
            {
                var checkVehicle = _VehicleService.GetAll().Any(i => i.Plate == model.Vehicle.Plate && i.Id != model.Vehicle.Id);
                if (checkVehicle)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Araç Daha Önceden Kaydedilmiş.", 2);
                    return View(model);
                }
                else
                {
                    Vehicle entity = _VehicleService.Find(model.Vehicle.Id);
                    entity.Plate = model.Vehicle.Plate;
                    entity.Active = model.Vehicle.Active;
                    _VehicleService.Update(entity);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Vehicle.Plate + " Plakalı Araç Bilgileri Güncellendi.", 1);
                    return RedirectToAction("Index", "Vehicle", "");
                }
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Araç Güncellenemedi.", 3);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {
            var data = _VehicleService.GetAll();
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Plate.Contains(searchValue));

            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            _VehicleService.Delete(Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Araç Silinmiştir.", 1);
            return RedirectToAction("Index", "Vehicle", "");

        }
    }
}