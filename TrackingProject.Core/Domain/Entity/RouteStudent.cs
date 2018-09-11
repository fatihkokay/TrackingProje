using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class RouteStudent : BaseEntity
    {
        public int RouteId { get; set; }
        public int StudentId { get; set; }

        public virtual Route _Route { get; set; }
        public virtual Student _Student { get; set; }
    }
}
