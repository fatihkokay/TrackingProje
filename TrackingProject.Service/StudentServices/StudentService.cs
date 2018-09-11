using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Student> _repository;
        public StudentService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Student>();

        }
        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Student Data)
        {
            _repository.Delete(Data);
        }

        public Student Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Student> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Student Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Student Data)
        {
            _repository.Update(Data);
        }
    }
}
