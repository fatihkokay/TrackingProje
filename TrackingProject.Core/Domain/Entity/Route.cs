using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Route : BaseEntity
    {
        public Route()
        {
            RouteLineList = new List<RouteLine>();
            RouteStudentList = new List<RouteStudent>();
            MovementList = new List<Movement>();
            RouteMovementList = new List<RouteMovement>();
        }

        public int RouteType { get; set; }
        public int FirmId { get; set; }
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string SchoolExitTime { get; set; }
        public string StartTime { get; set; }

        public virtual Firm _Firm { get; set; }
        public virtual Driver _Driver { get; set; }
        public virtual ICollection<RouteLine> RouteLineList { get; set; }
        public virtual ICollection<RouteStudent> RouteStudentList { get; set; }
        public virtual ICollection<Movement> MovementList { get; set; }
        public virtual ICollection<RouteMovement> RouteMovementList { get; set; }
    }
}
