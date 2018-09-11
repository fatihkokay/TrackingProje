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
    public class RouteLineService : IRouteLineService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<RouteLine> _repository;

        public RouteLineService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<RouteLine>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(RouteLine Data)
        {
            _repository.Delete(Data);
        }

        public RouteLine Find(int Id)
        {
           return  _repository.Find(Id);
        }

        public IQueryable<RouteLine> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(RouteLine Data)
        {
            _repository.Insert(Data);
        }

        public void Update(RouteLine Data)
        {
            _repository.Update(Data);
        }
    }
}
