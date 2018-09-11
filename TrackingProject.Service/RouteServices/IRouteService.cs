using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.RouteServices
{
    public interface IRouteService
    {
        IQueryable<Route> GetAll();
        Route Find(int Id);
        void Insert(Route Data);
        void Update(Route Data);
        void Delete(Route Data);
        void Delete(int Id);
    }
}
