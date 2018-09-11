using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.VehicleServices
{
    public interface IVehicleService
    {
        IQueryable<Vehicle> GetAll();
        Vehicle Find(int Id);
        void Insert(Vehicle Data);
        void Update(Vehicle Data);
        void Delete(Vehicle Data);
        void Delete(int Id);
        string LastCode();

    }
}
