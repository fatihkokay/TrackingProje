using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class ParentMap : EntityTypeConfiguration<Parent>
    {
        public ParentMap()
        {
            ToTable("Parent");
            HasKey(x => x.Id);

            HasRequired<Firm>(m => m._Firm).WithMany(m => m.ParentList).HasForeignKey(s => s.FirmId);
        }
    }
}
