using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TrackingProject.Data.UnitOfWork;
using TrackingProject.Service.DriverServices;
using TrackingProject.Service.FirmServices;
using TrackingProject.Service.RouteServices;
using TrackingProject.WebSite.Models;
using TrackingProject.WebSite.Utils;
using TrackingProject.Service.StudentServices;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.WebSite.Controllers
{
    public class RouteController : BaseController
    {
        private readonly IRouteService _RouteService;
        private readonly IFirmService _FirmService;
        private readonly IDriverService _DriverService;
        private readonly IRouteStudentService _RouteStudentService;
        private readonly IStudentService _StudentService;

        public RouteController(IDriverService driverService, IFirmService firmService, IRouteService routeService,
            IStudentService StudentService,
            IRouteStudentService routeStudentService,
            IUnitOfWork uow) : base(uow)
        {
            _RouteService = routeService;
            _DriverService = driverService;
            _FirmService = firmService;
            _StudentService = StudentService;
            _RouteStudentService = routeStudentService;
        }

        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = new RouteViewModel();
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            var firmId = int.Parse(model.Firms.FirstOrDefault().Value);
            model.Drivers = _DriverService.GetAll().Where(i => i.FirmId == firmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            model.Students = _StudentService.GetAll().Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RouteViewModel Model)
        {
            var checkRoute = _RouteService.GetAll().Any(i => i.FirmId == Model.FirmId && i.DriverId == Model.DriverId && i.Name == Model.Name);
            if (checkRoute)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Firma ve Sürücü Daha Önceden Kaydedilmiş.", 2);
                Model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                var firmId = int.Parse(Model.Firms.FirstOrDefault().Value);
                Model.Drivers = _DriverService.GetAll().Where(i => i.FirmId == firmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(Model);
            }
            TrackingProject.Core.Domain.Entity.Route Entity = new Core.Domain.Entity.Route();
            Entity.DriverId = Model.DriverId;
            Entity.FirmId = Model.FirmId;
            Entity.Name = Model.Name;
            Entity.StartTime = Model.StartTime;
            Entity.SchoolExitTime = Model.SchoolExitTime;
            #region SMS için rota başı veya sonu
            var lat = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[0].Replace(".", ",") : "0");
            var lon = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[1].Replace(".", ",") : "0");
            Entity.RouteLineList.Add(new RouteLine()
            {
                CreateDateTime = DateTime.Now,
                Latitute = Convert.ToDouble(lat),
                Longitude = Convert.ToDouble(lon),
                LineType = 3,
                RouteId = Entity.Id,
                StudentId = 0
            });
            #endregion
            foreach (var item in Model.SaveStudents.Split(','))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    Entity.RouteStudentList.Add(new Core.Domain.Entity.RouteStudent
                    {
                        RouteId = Entity.Id,
                        StudentId = int.Parse(item)
                    });
                }
            }
            _RouteService.Insert(Entity);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", Model.Name + " İsimli Rota Kaydedildi.", 1);
            return RedirectToAction("Index", "Route", "");

        }

        public ActionResult Edit(int Id)
        {
            var model = new RouteViewModel();
            var entity = _RouteService.Find(Id);
            model.RouteType = entity.RouteType;
            model.DriverId = entity.DriverId;
            model.FirmId = entity.FirmId;
            model.Id = Id;
            model.Name = entity.Name;
            model.StartTime = entity.StartTime;
            model.SchoolExitTime = entity.SchoolExitTime;
            model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            model.Drivers = _DriverService.GetAll().Where(i => i.FirmId == model.FirmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
            var stu = _RouteStudentService.GetAll().Where(i => i.RouteId == Id).Select(i => i.StudentId).ToList();
            if (stu.Count > 0)
            {
                model.SaveStudents = string.Join(",", stu) + ",";
            }
            var data = entity.RouteLineList.FirstOrDefault(i => i.StudentId == 0 && i.LineType == 3);
            if(data != null)
            {
                model.LocationPoint = data.Latitute.ToString().Replace(",", ".") + ":" + data.Longitude.ToString().Replace(",", ".");

            }
            else
            {
                model.LocationPoint = "0:0";

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(RouteViewModel Model)
        {
            //var checkRoute = _RouteService.GetAll().Any(i => i.FirmId == Model.FirmId && i.DriverId == Model.DriverId && i.Id != Model.Id);
            var checkRoute = _RouteService.GetAll().Any(i => i.Name == Model.Name && i.Id != Model.Id);
            if (checkRoute)
            {
                TempData["SweetAlert"] = BaseUtils.SweetAlert("Uyarı!", "Bu Firma ve Sürücü Daha Önceden Kaydedilmiş.", 2);
                Model.Firms = _FirmService.GetAll().Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                var firmId = int.Parse(Model.Firms.FirstOrDefault().Value);
                Model.Drivers = _DriverService.GetAll().Where(i => i.FirmId == firmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text).ToList();
                return View(Model);
            }
            TrackingProject.Core.Domain.Entity.Route Entity = _RouteService.Find(Model.Id);
            Entity.DriverId = Model.DriverId;
            Entity.FirmId = Model.FirmId;
            Entity.Name = Model.Name;
            Entity.RouteType = Model.RouteType;
            Entity.StartTime = Model.StartTime;
            Entity.SchoolExitTime = Model.SchoolExitTime;
            var data = Entity.RouteLineList.FirstOrDefault(i => i.StudentId == 0 && i.LineType == 3);
            if (data !=null)
            {
                var lat = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[0].Replace(".", ",") : "0");
                var lon = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[1].Replace(".", ",") : "0");
                //Entity.RouteLineList.Remove(data);
                data.Latitute = Convert.ToDouble(lat);
                data.Longitude= Convert.ToDouble(lon);
            }
            else
            {
                var lat = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[0].Replace(".", ",") : "0");
                var lon = Convert.ToDecimal(!string.IsNullOrEmpty(Model.LocationPoint) ? Model.LocationPoint.Split(':')[1].Replace(".", ",") : "0");

                Entity.RouteLineList.Add(new RouteLine()
                {
                    CreateDateTime = DateTime.Now,
                    Latitute = Convert.ToDouble(lat),
                    Longitude = Convert.ToDouble(lon),
                    LineType = 3,
                    RouteId = Entity.Id,
                    StudentId = 0
                });
            }
            
            _RouteService.Update(Entity);
            _uow.SaveChanges();
            #region Listede Olmayanlar Ekleniyor
            if (!string.IsNullOrEmpty(Model.SaveStudents))
            {
                foreach (var item in Model.SaveStudents.Split(','))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var id = int.Parse(item);
                        var check = Entity.RouteStudentList.FirstOrDefault(i => i.StudentId == id);

                        if (check == null)
                        {
                            Entity.RouteStudentList.Add(new Core.Domain.Entity.RouteStudent
                            {
                                RouteId = Entity.Id,
                                StudentId = int.Parse(item)
                            });
                            _RouteService.Update(Entity);
                            _uow.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                foreach (var silinecek in Entity.RouteStudentList.ToList())
                {
                    _RouteStudentService.Delete(silinecek);
                    _uow.SaveChanges();
                }
            }
            #endregion
            #region Listeden Kaldırılanlar Siliniyor
            foreach (var silinecek in Entity.RouteStudentList.ToList())
            {
                if (!Model.SaveStudents.Split(',').Any(i => i == silinecek.StudentId.ToString()))
                {
                    _RouteStudentService.Delete(silinecek);
                    _uow.SaveChanges();
                }
            }
            #endregion

            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", Model.Name + " İsimli Rota Güncellendi.", 1);
            return RedirectToAction("Index", "Route", "");

        }

        [HttpPost]
        public JsonResult List(int draw = 1, int start = 0, int length = 10)
        {
            var data = _RouteService.GetAll();

            string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].ToString() + "][name]"].ToString();
            string sortColumnDirection = Request.Form["order[0][dir]"].ToString();
            var afterOtherColumn = "";
            if (sortColumn == "StudentCount" || sortColumn == "DriverName")
            {
                afterOtherColumn = sortColumn + " " + sortColumnDirection;
                sortColumn = "";
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                data = EntityUtils.OrderBy(data, sortColumn + " " + sortColumnDirection);

            string searchValue = Request.Form["search[value]"].ToString();

            if (!string.IsNullOrEmpty(searchValue))
                data = data.Where(m => m.Name.Contains(searchValue));


            int recordsTotal = data.Count();
            var list = data.Select(m => new RouteViewModel()
            {
                DriverId = m.DriverId,
                DriverName = m._Driver.Name + " " + m._Driver.Surname,
                FirmId = m.FirmId,
                Id = m.Id,
                FirmName = m._Firm.Name,
                Name = m.Name,
                StartTime = m.StartTime,
                SchoolExitTime = m.SchoolExitTime,
                StudentCount = m.RouteStudentList.Count
            });
            if (!string.IsNullOrEmpty(afterOtherColumn))
            {
                list = list.OrderBy(afterOtherColumn);
            }
            var result = list.Skip(start).Take(length).ToList();
            return Json(new { draw = draw, recordsTotal = recordsTotal, recordsFiltered = recordsTotal, data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDriver(int? FirmId)
        {
            try
            {
                var drivers = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz...", Value = "", Selected = true } };
                drivers.AddRange(_DriverService.GetAll().Where(i => i.FirmId == FirmId).Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).OrderBy(i => i.Text));
                return Json(new { data = drivers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = new List<SelectListItem>() { new SelectListItem { Text = "Seçiniz...", Value = "", Selected = true } } }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(int Id)
        {
            _RouteService.Delete(Id);
            _uow.SaveChanges();
            TempData["SweetAlert"] = BaseUtils.SweetAlert("Başarılı!", "Rota Silinmiştir.", 1);
            return RedirectToAction("Index", "Route", "");

        }
        [HttpGet]
        public PartialViewResult GetStudents(int FirmId, int? RouteId)
        {
            var model = new RouteViewModel();
            model.Students = _StudentService.GetAll()
                .Where(i => i.FirmId == FirmId)
                .Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() })
                .OrderBy(i => i.Text).ToList();
            if (RouteId.HasValue)
            {
                var hasStudents = _RouteStudentService.GetAll().Where(i => i.RouteId == RouteId.Value).Select(i => i._Student);
                model.HasStudents = hasStudents.Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() })
                .OrderBy(i => i.Text).ToList();
            }
            else
            {
                model.HasStudents = new List<Student>().Select(i => new SelectListItem { Text = i.Name + " " + i.Surname, Value = i.Id.ToString() }).ToList();
            }
            return PartialView("Partial/_PartialPageStudent", model);

        }
    }
}