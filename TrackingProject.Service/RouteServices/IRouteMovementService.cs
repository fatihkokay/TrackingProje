using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.RouteServices
{
    public interface IRouteMovementService
    {

        IQueryable<RouteMovement> GetAll();
        RouteMovement Find(int Id);
        void Insert(RouteMovement Data);
        void Update(RouteMovement Data);
        void Delete(RouteMovement Data);
        void Delete(int Id);
    }
}
