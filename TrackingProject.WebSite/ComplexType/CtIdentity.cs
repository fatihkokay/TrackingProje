using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.WebSite.ComplexType
{
    public class CtIdentity
    {
        public User Users{get;set;}
        public IList<string> Roles { get; set; }
    }
}
