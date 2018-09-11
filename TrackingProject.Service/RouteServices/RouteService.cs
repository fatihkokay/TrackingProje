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
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Route> _repository;

        public RouteService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Route>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Route Data)
        {
            _repository.Delete(Data);
        }

        public Route Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Route> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Route Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Route Data)
        {
            _repository.Update(Data);
        }
    }
}
