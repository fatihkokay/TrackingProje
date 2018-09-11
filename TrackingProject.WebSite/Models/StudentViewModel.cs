using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackingProject.WebSite.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double Distance { get; set; }
        public int Active { get; set; }
        public List<SelectListItem> Firms { get; set; }
        public List<SelectListItem> Parents { get; set; }
        public int ParentId { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }

    }
}