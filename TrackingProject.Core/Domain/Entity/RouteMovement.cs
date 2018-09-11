using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class RouteMovement : BaseEntity
    {
        public int RouteId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int SchoolExitStatus { get; set; }

        public virtual Route _Route { get; set; }
    }
}
