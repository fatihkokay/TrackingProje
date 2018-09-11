using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.ParentServices
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Parent> _repository;
        public ParentService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Parent>();
        }


        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Parent Data)
        {
            _repository.Delete(Data);
        }

        public Parent Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Parent> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Parent Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Parent Data)
        {
            _repository.Update(Data);
        }
    }
}
