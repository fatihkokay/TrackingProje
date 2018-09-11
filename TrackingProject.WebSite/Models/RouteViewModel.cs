using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingProject.WebSite.Models
{
    public class RouteViewModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public int RouteType { get; set; }
        public string Name { get; set; }
        public string SchoolExitTime { get; set; }
        public string StartTime { get; set; }
        public List<SelectListItem> Firms { get; set; }
        public List<SelectListItem> Drivers { get; set; }
        public List<SelectListItem> Students { get; set; }
        public List<SelectListItem> HasStudents { get; set; }

        public int StudentCount { get; set; }
        public string SaveStudents { get; set; }

        public string LocationPoint { get; set; }

    }
}