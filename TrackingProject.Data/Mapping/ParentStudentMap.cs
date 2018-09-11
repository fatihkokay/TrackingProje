using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Data.Mapping
{
    public class ParentStudentMap : EntityTypeConfiguration<ParentStudent>
    {
        public ParentStudentMap()
        {
            ToTable("ParentStudent");
            HasKey(x => x.Id);

            HasRequired<Parent>(m => m._Parent).WithMany(m => m.ParentStudentList).HasForeignKey(s => s.ParentId);
            HasRequired<Student>(m => m._Student).WithMany(m => m.ParentStudentList).HasForeignKey(s => s.StudentId).WillCascadeOnDelete(false);
        }
    }
}
