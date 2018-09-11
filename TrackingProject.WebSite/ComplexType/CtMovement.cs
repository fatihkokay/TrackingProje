using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingProject.WebSite.ComplexType
{
    public class CtMovement
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public string Date_Short {
            get
            {
                return Date.ToString("dd.MM.yyyy");
            }
        }
        public bool InCarNotification { get; set; }
        public DateTime? InCarNotificationDate { get; set; }
        public bool InCar { get; set; }
        public DateTime? InCarDate { get; set; }
        public bool InCarSchoolNotification { get; set; }
        public DateTime? InCarSchoolNotificationDate { get; set; }
        public bool InCarSchool { get; set; }
        public DateTime? InCarSchoolDate { get; set; }
        public bool OutCarNotification { get; set; }
        public DateTime? OutCarNotificationDate { get; set; }
        public bool OutCar { get; set; }
        public DateTime? OutCarDate { get; set; }

        public string Duration { get; set; }


        public int Route_FirmId { get; set; }
        public int Route_DriverId { get; set; }
        public string Route_Name { get; set; }

        public int Student_FirmId { get; set; }
        public string Student_Name { get; set; }
        public string Student_Surname { get; set; }

        public int StudentCount { get; set; }
    }
}