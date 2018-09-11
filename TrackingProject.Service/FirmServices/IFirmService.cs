using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;

namespace TrackingProject.Service.FirmServices
{
    public interface IFirmService
    {
        IQueryable<Firm> GetAll();
        Firm Find(int Id);
        void Insert(Firm Data);
        void Update(Firm Data);
        void Delete(Firm Data);
        void Delete(int Id);
        string LastCode();
    }
}
