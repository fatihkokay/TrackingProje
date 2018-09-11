using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class RouteMap : EntityTypeConfiguration<Route>
    {
        public RouteMap()
        {
            ToTable("Route");
            HasKey(x => x.Id);

            HasRequired<Firm>(m => m._Firm).WithMany(m => m.RouteList).HasForeignKey(s => s.FirmId);
            HasRequired<Driver>(m => m._Driver).WithMany(m => m.RouteList).HasForeignKey(s => s.DriverId);

        }
    }
}
