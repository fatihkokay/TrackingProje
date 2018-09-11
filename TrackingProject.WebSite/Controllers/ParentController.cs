using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.DriverServices;
using TrackingProject.Service.FirmServices;
using TrackingProject.Service.ParentServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class ParentController : BaseController
    {
        private readonly IParentService _ParentService;
        private readonly IFirmService _FirmService;
        private readonly IParentStudentService _ParentStudentService;


        public ParentController(IParentService ParentService, IFirmService firmService, IParentStudentService ParentStudentService, IUnitOfWork uow) : base(uow)
        {
            _ParentService = ParentService;
            _FirmService = firmService;
            _ParentStudentService = ParentStudentService;
        }

        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ParentViewModel model = new ParentViewModel();
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            model.Active = 1;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ParentViewModel model)
        {
            try
            {
                Parent Entity = new Parent();
                Entity.Active = model.Active;
                Entity.FirmId = model.FirmId;
                Entity.Name = model.Name;
                Entity.Surname = model.Surname;
                Entity.Phone = model.Phone;
                if (!string.IsNullOrEmpty(model.Password))
                    Entity.Password = model.Password;
                _ParentService.Insert(Entity);
                _uow.SaveChanges();
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " " + model.Surname + " İsimli Ebeveyn Kaydedildi.", 1);
                return RedirectToAction("Index", "Parent", "");

            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Ebeveyn Kaydedilemedi.", 3);
                model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(model);
            }
        }

        public ActionResult Edit(int Id)
        {
            Parent Entity = _ParentService.Find(Id);

            ParentViewModel model = new ParentViewModel();
            model.Id = Entity.Id;
            model.Name = Entity.Name;
            model.Surname = Entity.Surname;
            model.Active = Entity.Active;
            model.FirmId = Entity.FirmId;
            model.Phone = Entity.Phone;
            model.Password = Entity.Password;
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ParentViewModel model)
        {
            try
            {

                Parent Entity = _ParentService.Find(model.Id);
                Entity.Name = model.Name;
                Entity.Surname = model.Surname;
                Entity.Active = model.Active;
                Entity.FirmId = model.FirmId;
                Entity.Phone = model.Phone;
                if (!string.IsNullOrEmpty(model.Password))
                    Entity.Password = model.Password;
                _ParentService.Update(Entity);
                _uow.SaveChanges();
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " " + model.Surname + " İsimli Ebeveyn Güncellendi.", 1);
                return RedirectToAction("Index", "Parent", "");

            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Ebeveyn Güncellenemedi.", 3);
                model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(model);
            }
        }
        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {
            var data = _ParentService.GetAll();

            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Name.Contains(searchValue) || m.Surname.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).Select(m => new ParentViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Active = m.Active,
                Surname = m.Surname,
                FirmName = m._Firm.Name,
                Phone = m.Phone
            }).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int Id)
        {
            var list = _ParentStudentService.GetAll().Where(i => i.ParentId == Id);
            foreach (var item in list)
            {
                _ParentStudentService.Delete(item.Id);
            }
            _ParentService.Delete(Id);

            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Ebeveyn Silinmiştir.", 1);
            return RedirectToAction("Index", "Parent", "");

        }
    }
}