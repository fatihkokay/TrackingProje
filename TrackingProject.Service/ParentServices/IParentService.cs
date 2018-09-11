using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.ParentServices
{
    public interface IParentService
    {
        IQueryable<Parent> GetAll();
        Parent Find(int Id);
        void Insert(Parent Data);
        void Update(Parent Data);
        void Delete(Parent Data);
        void Delete(int Id);
    }
}
