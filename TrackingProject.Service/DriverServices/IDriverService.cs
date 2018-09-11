using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.DriverServices
{
    public interface IDriverService
    {
        IQueryable<Driver> GetAll();
        Driver Find(int Id);
        void Insert(Driver Data);
        void Update(Driver Data);
        void Delete(Driver Data);
        void Delete(int Id);
        string LastCode();

    }
}
