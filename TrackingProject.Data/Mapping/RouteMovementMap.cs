using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class RouteMovementMap : EntityTypeConfiguration<RouteMovement>
    {
        public RouteMovementMap()
        {
            ToTable("RouteMovement");
            HasKey(x => x.Id);
            Property(x => x.Date).HasColumnType("date");

            HasRequired<Route>(m => m._Route).WithMany(m => m.RouteMovementList).HasForeignKey(s => s.RouteId).WillCascadeOnDelete(false);
        }
    }
}
