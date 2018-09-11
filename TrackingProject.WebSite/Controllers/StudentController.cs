using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.FirmServices;
using TrackingProject.Service.ParentServices;
using TrackingProject.Service.StudentServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _StudentService;
        private readonly IFirmService _FirmService;
        private readonly IParentService _ParentService;
        private readonly IParentStudentService _ParentStudentService;

        public StudentController(IStudentService studentService, IFirmService firmService, IParentService ParentService, IParentStudentService ParentStudentService, IUnitOfWork uow) : base(uow)
        {
            _StudentService = studentService;
            _ParentService = ParentService;
            _FirmService = firmService;
            _ParentStudentService = ParentStudentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new StudentViewModel();
            model.Distance = 2000;
            var list = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();

            model.Firms = list;
            var firmId = int.Parse(list.FirstOrDefault().Value);
            model.Parents = _ParentService.GetAll()
                .Where(i => i.FirmId == firmId).ToList()
                .Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() })
                .OrderBy(i => i.Text).ToList();
            //model.Parents = new List<Parent>().Select(i => new SelectListItem { Text = i.Name + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            model.Active = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel model)
        {
            try
            {
                //var checkDriver = _StudentService.GetAll().Any(i => i.Name == model.Name && i.Id != model.Id);
                //if (checkDriver)
                //{
                //    TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Sürücü Kullanıcı Daha Önceden Kaydedilmiş.", 2);
                //    model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                //    return View(model);
                //}
                //else
                //{
                Student Entity = new Student();
                Entity.Active = model.Active;
                Entity.FirmId = model.FirmId;
                Entity.ParentStudentList.Add(new ParentStudent() { ParentId = model.ParentId, StudentId = Entity.Id });
                Entity.Name = model.Name;
                Entity.Surname = model.Surname;
                Entity.Phone = model.Phone;
                Entity.Address = model.Address;
                Entity.Distance = model.Distance;
                _StudentService.Insert(Entity);
                _uow.SaveChanges();
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.Name + " " + model.Surname + " İsimli Öğrenci Kaydedildi.", 1);
                return RedirectToAction("Index", "Student", "");

                //}
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Öğrenci Kaydedilemedi.", 3);
                model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(model);
            }

        }

        public ActionResult Edit(int Id)
        {
            var model = new StudentViewModel();
            var entity = _StudentService.Find(Id);
            model.Active = entity.Active;
            model.FirmId = entity.FirmId;
            model.Id = Id;
            model.Name = entity.Name;
            model.Surname = entity.Surname;
            model.Phone = entity.Phone;
            model.Address = entity.Address;
            model.Distance = entity.Distance;
            var parent = _ParentStudentService.GetAll().FirstOrDefault(i => i.StudentId == Id);

            var list = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Code + "-" + i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();

            model.Firms = list;
            model.Parents = _ParentService.GetAll()
                .Where(i => i.FirmId == entity.FirmId).ToList()
                .Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() })
                .OrderBy(i => i.Text).ToList();
            if (parent != null)
                model.ParentId = parent.ParentId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel Model)
        {
            Student Entity = _StudentService.Find(Model.Id);
            Entity.Active = Model.Active;
            Entity.FirmId = Model.FirmId;
            Entity.Name = Model.Name;
            Entity.Surname = Model.Surname;
            Entity.Phone = Model.Phone;
            Entity.Address = Model.Address;
            Entity.Distance = Model.Distance;
            if (Entity.ParentStudentList.FirstOrDefault() != null)
                Entity.ParentStudentList.FirstOrDefault().ParentId = Model.ParentId;
            else
            {
                Entity.ParentStudentList.Add(new ParentStudent() { ParentId = Model.ParentId, StudentId = Model.Id });
            }
            _StudentService.Update(Entity);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", Model.Name + " " + Model.Surname + " İsimli Öğrenci Güncellendi.", 1);
            return RedirectToAction("Index", "Student", "");

        }

        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {

            var data = _StudentService.GetAll();

            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Name.Contains(searchValue) || m.Surname.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).Select(m => new StudentViewModel()
            {
                Active = m.Active,
                Id = m.Id,
                FirmId = m.FirmId,
                FirmName = m._Firm.Name,
                Name = m.Name,
                Surname = m.Surname,
                Phone = m.Phone,
                Distance = m.Distance
            }).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParent(int? FirmId)
        {
            try
            {
                var parents = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz...", Value = "", Selected = true } };
                parents.AddRange(_ParentService.GetAll().Where(i => i.FirmId == FirmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text));
                return Json(new { data = parents }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { data = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz...", Value = "", Selected = true } } }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int Id)
        {
            var pStudent = _ParentStudentService.GetAll().FirstOrDefault(i => i.StudentId == Id);
            _StudentService.Delete(Id);
            _ParentStudentService.Delete(pStudent.Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Öğrenci Silinmiştir.", 1);
            return RedirectToAction("Index", "Student", "");

        }
    }
}