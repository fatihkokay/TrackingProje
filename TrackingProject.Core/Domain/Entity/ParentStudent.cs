using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class ParentStudent : BaseEntity
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }

        public Parent _Parent { get; set; }
        public Student _Student { get; set; }
    }
}
