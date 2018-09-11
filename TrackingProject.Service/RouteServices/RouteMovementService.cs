using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.RouteServices
{
    public class RouteMovementService : IRouteMovementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<RouteMovement> _repository;

        public RouteMovementService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<RouteMovement>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(RouteMovement Data)
        {
            _repository.Delete(Data);
        }

        public RouteMovement Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<RouteMovement> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(RouteMovement Data)
        {
            _repository.Insert(Data);
        }

        public void Update(RouteMovement Data)
        {
            _repository.Update(Data);
        }
    }
}
