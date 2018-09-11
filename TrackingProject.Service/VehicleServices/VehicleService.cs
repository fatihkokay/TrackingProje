using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.VehicleServices
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Vehicle> _repository;
        public VehicleService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Vehicle>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Vehicle Data)
        {
            _repository.Delete(Data);
        }

        public Vehicle Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Vehicle Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Vehicle Data)
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
