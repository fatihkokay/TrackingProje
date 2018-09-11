using System.Collections.Generic;
using System.Web.Mvc;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.WebSite.Models
{
    public class WatchViewModel
    {
        public List<RouteLine> RouteLines { get; set; }
        public List<SelectListItem> Cars { get; set; }
        public int CarId { get; set; }

        public List<SelectListItem> Routes { get; set; }
        public int RouteId { get; set; }
        public string DateRange { get; set; }

        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }


        public int iDisplayLength { get; set; }
        public int iDisplayStart { get; set; }
        public string SortType { get; set; }
        public string SortColumn { get; set; }
        public string Filter { get; set; }
        public string SortColumnName { get; set; }
    }
}