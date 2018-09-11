using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Driver : BaseEntity
    {
        public Driver()
        {
            RouteList = new List<Route>();
        }
        public int FirmId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DevinceId { get; set; }
        public int Active { get; set; }

        public virtual Firm _Firm { get; set; }
        public virtual ICollection<Route> RouteList { get; set; }
    }
}
