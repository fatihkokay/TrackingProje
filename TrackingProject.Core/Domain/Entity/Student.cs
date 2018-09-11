using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Student : BaseEntity
    {
        public Student()
        {
            ParentStudentList = new List<ParentStudent>();
            MovementList = new List<Movement>();
        }

        public int FirmId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Active { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public double Distance { get; set; }
        public Firm _Firm { get; set; }
        public virtual ICollection<ParentStudent> ParentStudentList { get; set; }
        public virtual ICollection<Movement> MovementList { get; set; }
    }
}
