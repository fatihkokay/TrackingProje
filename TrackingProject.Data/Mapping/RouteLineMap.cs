using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class RouteLineMap : EntityTypeConfiguration<RouteLine>
    {
        public RouteLineMap()
        {
            ToTable("RouteLine");
            HasKey(x => x.Id);

            HasRequired<Route>(m => m._Route).WithMany(m => m.RouteLineList).HasForeignKey(s => s.RouteId);
        }
    }
}
