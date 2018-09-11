using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class RouteLine : BaseEntity
    {
        public int RouteId { get; set; }
        public int StudentId { get; set; }
        public int LineType { get; set; }
        public double Longitude { get; set; }
        public double Latitute { get; set; }
        public DateTime CreateDateTime { get; set; }
        public virtual Route _Route { get; set; }
    }
}
