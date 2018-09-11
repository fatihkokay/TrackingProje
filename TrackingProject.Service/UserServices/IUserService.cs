using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.UserServices
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        User Find(int Id);
        void Insert(User Data);
        void Update(User Data);
        void Delete(User Data);
        void Delete(int Id);
    }
}
