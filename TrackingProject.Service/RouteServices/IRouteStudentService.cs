using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.RouteServices
{
    public interface IRouteStudentService
    {
        IQueryable<RouteStudent> GetAll();
        RouteStudent Find(int Id);
        void Insert(RouteStudent Data);
        void Update(RouteStudent Data);
        void Delete(RouteStudent Data);
        void Delete(int Id);
    }
}
