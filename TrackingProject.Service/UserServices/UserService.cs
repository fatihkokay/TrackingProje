using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<User> _repository;
        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<User>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(User Data)
        {
            _repository.Delete(Data);
        }

        public User Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<User> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(User Data)
        {
            _repository.Insert(Data);
        }

        public void Update(User Data)
        {
            _repository.Update(Data);
        }
    }
}
