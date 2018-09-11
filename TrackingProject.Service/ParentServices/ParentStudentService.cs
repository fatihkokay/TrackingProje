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
    public class ParentStudentService : IParentStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<ParentStudent> _repository;
        public ParentStudentService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<ParentStudent>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(ParentStudent Data)
        {
            _repository.Delete(Data);
        }

        public ParentStudent Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<ParentStudent> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(ParentStudent Data)
        {
            _repository.Insert(Data);
        }

        public void Update(ParentStudent Data)
        {
            _repository.Update(Data);
        }
    }
}
