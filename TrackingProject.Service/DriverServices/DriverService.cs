using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.DriverServices
{
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Driver> _repository;
        public DriverService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Driver>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Driver Data)
        {
            _repository.Delete(Data);
        }

        public Driver Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Driver> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Driver Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Driver Data)
        {
            _repository.Update(Data);
        }
        public string LastCode()
        {
            if (_repository.GetAll().Any())
            {
                return _repository.GetAll().OrderByDescending(m => m.Code).Select(m => m.Code).First();
            }

            return "";
        }
    }
}
