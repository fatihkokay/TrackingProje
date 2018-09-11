using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class MovementMap : EntityTypeConfiguration<Movement>
    {
        public MovementMap()
        {
            ToTable("Movement");
            HasKey(x => x.Id);
            Property(x => x.Date).HasColumnType("date");

            HasRequired<Route>(m => m._Route).WithMany(m => m.MovementList).HasForeignKey(s => s.RouteId).WillCascadeOnDelete(false);
            HasRequired<Student>(m => m._Student).WithMany(m => m.MovementList).HasForeignKey(s => s.StudentId).WillCascadeOnDelete(false);

        }

    }
}
