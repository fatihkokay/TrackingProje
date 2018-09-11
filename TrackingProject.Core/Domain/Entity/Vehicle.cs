using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Core.Domain.Entity
{
    public class Vehicle : BaseEntity
    {
        public Vehicle()
        {

        }
        public string Code { get; set; }
        public string Plate { get; set; }
        public int Active { get; set; }

    }
}
