using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingProject.Api.Models
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
    
        public RouteMovementViewModel RouteMovement { get; set; }
        public List<RouteLineViewModel> RouteLineList { get; set; }
        public List<RouteStudentViewModel> RouteStudentList { get; set; }
        public List<MovementViewModel> MovementList { get; set; }

    }

    public class RouteLineViewModel
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StudentId { get; set; }
        public int LineType { get; set; }
        public double Longitude { get; set; }
        public double Latitute { get; set; }
        public DateTime CreateDateTime { get; set; }

        public StudentViewModel Student { get; set; }
    }

    public class RouteStudentViewModel
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StudentId { get; set; }

        public StudentViewModel Student { get; set; }
    }

    public class RouteMovementViewModel
    {
        public int RouteId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int SchoolExitStatus { get; set; }
    }
}