using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.UserServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;

namespace TrackingProject.WebSite.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IUnitOfWork uow) : base(uow)
        {
            _userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new UserViewModel() { User = new Core.Domain.Entity.User() };
            model.User.Active = 1;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            var checkUser = _userService.GetAll().Any(i => i.Username == model.User.Username);
            if (checkUser)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Kullanıcı Daha Önceden Kaydedilmiş.", 2);
                return View(model);
            }
            else
            {
                try
                {
                    var newUser = new User()
                    {
                        Username = model.User.Username,
                        Password = model.User.Password,
                        Active = model.User.Active
                    };
                    _userService.Insert(newUser);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Kullanıcı Kaydedildi.", 1);
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Kullanıcı Kaydedilemedi.", 3);
                    return View(model);
                }
            }
        }

        public ActionResult Edit(int Id)
        {
            var model = new UserViewModel();
            model.User = _userService.Find(Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            try
            {
                var checkUser = _userService.GetAll().Any(i => i.Username == model.User.Username && i.Id != model.User.Id);
                if (checkUser)
                {
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Kullanıcı Daha Önceden Kaydedilmiş.", 2);
                    return View(model);
                }
                else
                {
                    User entity = _userService.Find(model.User.Id);
                    entity.Username = model.User.Username;
                    if (!string.IsNullOrEmpty(model.User.Password))
                        entity.Password = model.User.Password;
                    entity.Active = model.User.Active;
                    _userService.Update(entity);
                    _uow.SaveChanges();
                    TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", model.User.Username + " İsimli Kullanıcı Bilgileri Güncellendi.", 1);
                    return RedirectToAction("Index", "User", "");
                }
            }
            catch (Exception)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Hata!", "Kullanıcı Güncellenemedi.", 3);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {
            var data = _userService.GetAll();
            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Username.Contains(searchValue));

            int recordsTotal = data.Count();
            var list = data.Skip(start).Take(length).ToList();

            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            _userService.Delete(Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Kullanıcı Silinmiştir.", 1);
            return RedirectToAction("Index", "User", "");

        }

        public ActionResult Profile()
        {
            var model = new UserViewModel() { User = new Core.Domain.Entity.User() };
            model.User = BaseUtils.Users.Users;
            return View(model);
        }
    }
}