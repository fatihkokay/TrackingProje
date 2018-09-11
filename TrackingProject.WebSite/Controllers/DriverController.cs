using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.DriverServices;
using TrackingProject.Service.FirmServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class DriverController : BaseController
    {
        private readonly IDriverService _DriverService;
        private readonly IFirmService _FirmService;


        public DriverController(IDriverService driverService, IFirmService firmService, IUnitOfWork uow) : base(uow)
        {
            _DriverService = driverService;
            _FirmService = firmService;
        }

        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            DriverViewModel model = new DriverViewModel();
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            model.Active = 1;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(DriverViewModel model)
        {
            try
            {
                var checkDriver = _DriverService.GetAll().Any(i => i.Username == model.Username && i.Id != model.Id);
                if (checkDriver)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Sürücü Kullanıcı Daha Önceden Kaydedilmiş.", 2);
                    model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                    return View(model);
                }
                else
                {
                    Driver Entity = new Driver();
                    Entity.Active = model.Active;
                    Entity.Code = BaseUtils.NextKeyCode(string.IsNullOrEmpty(_DriverService.LastCode()) ? "0001" : _DriverService.LastCode());
                    Entity.FirmId = model.FirmId;
                    Entity.Name = model.Name;
                    Entity.Password = model.Password;
                    Entity.Surname = model.Surname;
                    Entity.Username = model.Username;
                    Entity.DevinceId = model.DevinceId;

                    _DriverService.Insert(Entity);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " " + model.Surname + " İsimli Sürücü Kaydedildi.", 1);
                    return RedirectToAction("Index", "Driver", "");
                }
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Sürücü Kaydedilemedi.", 3);
                model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(model);
            }
        }

        public ActionResult Edit(int Id)
        {
            Driver Entity = _DriverService.Find(Id);

            DriverViewModel model = new DriverViewModel();
            model.Id = Entity.Id;
            model.Name = Entity.Name;
            model.Password = Entity.Password;
            model.Surname = Entity.Surname;
            model.Username = Entity.Username;
            model.Active = Entity.Active;
            model.Code = Entity.Code;
            model.FirmId = Entity.FirmId;
            model.DevinceId = Entity.DevinceId;
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(DriverViewModel model)
        {
            try
            {
                var checkDriver = _DriverService.GetAll().Any(i => i.Username == model.Username && i.Id != model.Id);
                if (checkDriver)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Sürücü Kullanıcısı Daha Önceden Kaydedilmiş.", 2);
                    model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                    return View(model);
                }
                else
                {
                    Driver Entity = _DriverService.Find(model.Id);
                    Entity.Name = model.Name;
                    if (!string.IsNullOrEmpty(model.Password))
                        Entity.Password = model.Password;
                    Entity.Surname = model.Surname;
                    Entity.Username = model.Username;
                    Entity.Active = model.Active;
                    Entity.FirmId = Entity.FirmId;
                    Entity.DevinceId = model.DevinceId;
                    _DriverService.Update(Entity);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " " + model.Surname + " İsimli Sürücü Güncellendi.", 1);
                    return RedirectToAction("Index", "Driver", "");
                }
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Sürücü Güncellenemedi.", 3);
                model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {
            var data = _DriverService.GetAll();

            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Name.Contains(searchValue) || m.Surname.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).Select(m => new DriverViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Code = m.Code,
                Active = m.Active,
                Surname = m.Surname,
                FirmName = m._Firm.Name,
                DevinceId = m.DevinceId
            }).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int Id)
        {
            _DriverService.Delete(Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Sürücü Silinmiştir.", 1);
            return RedirectToAction("Index", "Driver", "");

        }
    }
}