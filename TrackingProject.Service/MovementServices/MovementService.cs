using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.MovementServices
{
    public class MovementService : IMovementService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Movement> _repository;
        public MovementService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Movement>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Movement Data)
        {
            _repository.Delete(Data);
        }

        public Movement Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Movement> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Movement Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Movement Data)
        {
            _repository.Update(Data);
        }

    }
}
