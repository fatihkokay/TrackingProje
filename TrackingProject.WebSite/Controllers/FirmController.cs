using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.FirmServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class FirmController : BaseController
    {
        private readonly IFirmService _FirmService;

        public FirmController(IFirmService firmService, IUnitOfWork uow) : base(uow)
        {
            _FirmService = firmService;
        }

        // GET: Firm
        public ActionResult Index()
        {
            return View();
        }

        // GET: Firm
        public ActionResult Create()
        {
            var model = new FirmViewModel();
            model.Active = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FirmViewModel Model)
        {
            try
            {
                Firm Entity = new Firm();
                Entity.Code = BaseUtils.NextKeyCode(string.IsNullOrEmpty(_FirmService.LastCode()) ? "0001" : _FirmService.LastCode());
                Entity.Name = Model.Name;
                Entity.Active = Model.Active;

                _FirmService.Insert(Entity);
                _uow.SaveChanges();
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", Model.Name + " İsimli Firma Kaydedildi.", 1);

                return RedirectToAction("Index", "Firm", "");
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Firma Kaydedilemedi.", 3);
                return View(Model);
            }
        }


        // GET: Firm
        public ActionResult Edit(int Id)
        {
            Firm Entity = _FirmService.Find(Id);

            FirmViewModel Model = new FirmViewModel();
            Model.Active = Entity.Active;
            Model.Code = Entity.Code;
            Model.Id = Entity.Id;
            Model.Name = Entity.Name;

            return View(Model);
        }

        [HttpPost]
        public ActionResult Edit(FirmViewModel model)
        {
            try
            {
                Firm Entity = _FirmService.Find(model.Id);
                Entity.Name = model.Name;
                Entity.Active = model.Active;

                _FirmService.Update(Entity);
                _uow.SaveChanges();
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " İsimli Firma Bilgileri Güncellendi.", 1);

                return RedirectToAction("Index", "Firm", "");
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Firma Güncellenemedi.", 3);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {

            var data = _FirmService.GetAll();

            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Code.Contains(searchValue) || m.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).Select(m => new FirmViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Code = m.Code,
                Active = m.Active
            }).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            _FirmService.Delete(Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Firma Silinmiştir.", 1);
            return RedirectToAction("Index", "Firm", "");

        }
    }

}