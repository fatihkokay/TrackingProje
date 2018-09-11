using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class FirmMap : EntityTypeConfiguration<Firm>
    {
        public FirmMap()
        {
            ToTable("Firm");
            HasKey(x => x.Id);

        }
    }
}
