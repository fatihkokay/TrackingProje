
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
    public class RouteStudentService : IRouteStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<RouteStudent> _repository;

        public RouteStudentService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<RouteStudent>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(RouteStudent Data)
        {
            _repository.Delete(Data);
        }

        public RouteStudent Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<RouteStudent> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(RouteStudent Data)
        {
            _repository.Insert(Data);
        }

        public void Update(RouteStudent Data)
        {
            _repository.Update(Data);
        }
    }
}
