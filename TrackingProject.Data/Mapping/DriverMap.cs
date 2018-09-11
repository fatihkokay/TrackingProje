using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class DriverMap : EntityTypeConfiguration<Driver>
    {
        public DriverMap()
        {
            ToTable("Driver");
            HasKey(x => x.Id);


            HasRequired<Firm>(m => m._Firm).WithMany(m => m.DriverList).HasForeignKey(s => s.FirmId).WillCascadeOnDelete(false);
        }
    }

}
