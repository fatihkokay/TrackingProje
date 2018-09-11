using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Parent : BaseEntity
    {
        public Parent()
        {
            ParentStudentList = new List<ParentStudent>();
        }

        public int FirmId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Active { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public virtual Firm _Firm { get; set; }
        public virtual ICollection<ParentStudent> ParentStudentList { get; set; }

    }
}
