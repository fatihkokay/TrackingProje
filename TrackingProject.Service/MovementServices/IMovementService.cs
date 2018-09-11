using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.MovementServices
{
    public interface IMovementService
    {
        IQueryable<Movement> GetAll();
        Movement Find(int Id);
        void Insert(Movement Data);
        void Update(Movement Data);
        void Delete(Movement Data);
        void Delete(int Id);

    }
}
