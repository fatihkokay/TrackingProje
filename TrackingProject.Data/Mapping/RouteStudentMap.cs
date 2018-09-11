using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class RouteStudentMap : EntityTypeConfiguration<RouteStudent>
    {
        public RouteStudentMap()
        {
            ToTable("RouteStudent");
            HasKey(x => x.Id);

            HasRequired<Route>(m => m._Route).WithMany(m => m.RouteStudentList).HasForeignKey(s => s.RouteId).WillCascadeOnDelete(false);
        }
    }
}
