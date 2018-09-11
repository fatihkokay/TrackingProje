using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.RouteServices
{
    public interface IRouteLineService
    {
        IQueryable<RouteLine> GetAll();
        RouteLine Find(int Id);
        void Insert(RouteLine Data);
        void Update(RouteLine Data);
        void Delete(RouteLine Data);
        void Delete(int Id);
    }
}
