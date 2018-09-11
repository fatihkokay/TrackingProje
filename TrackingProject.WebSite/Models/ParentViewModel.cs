using System.Collections.Generic;
using System.Web.Mvc;
namespace TrackingProject.WebSite.Models
{
    public class ParentViewModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public List<SelectListItem> Firms { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Active { get; set; }
        public string FirmName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }


    }
}