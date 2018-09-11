using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.ParentServices
{
    public interface IParentStudentService
    {
        IQueryable<ParentStudent> GetAll();
        ParentStudent Find(int Id);
        void Insert(ParentStudent Data);
        void Update(ParentStudent Data);
        void Delete(ParentStudent Data);
        void Delete(int Id);
    }
}
