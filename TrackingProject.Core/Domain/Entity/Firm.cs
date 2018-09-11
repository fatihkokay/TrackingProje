using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Firm : BaseEntity
    {
        public Firm()
        {
            RouteList = new List<Route>();
            DriverList = new List<Driver>();
            StudentList = new List<Student>();
            ParentList = new List<Parent>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }

        public virtual ICollection<Route> RouteList { get; set; }
        public virtual ICollection<Driver> DriverList { get; set; }
        public virtual ICollection<Student> StudentList { get; set; }
        public virtual ICollection<Parent> ParentList { get; set; }
    }
}
