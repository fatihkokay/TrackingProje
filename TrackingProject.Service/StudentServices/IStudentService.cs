using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.StudentServices
{
    public interface IStudentService
    {
        IQueryable<Student> GetAll();
        Student Find(int Id);
        void Insert(Student Data);
        void Update(Student Data);
        void Delete(Student Data);
        void Delete(int Id);
    }
}
