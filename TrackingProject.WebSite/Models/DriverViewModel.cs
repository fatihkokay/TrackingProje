using System.Collections.Generic;
using System.Web.Mvc;
namespace TrackingProject.WebSite.Models
{
    public class DriverViewModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public List<SelectListItem> Firms { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DevinceId { get; set; }
        public int Active { get; set; }
        public string FirmName { get; set; }

    }
}