using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingProject.Core.Domain.Entity;
using TrackingProject.Data.Repository;
using TrackingProject.Data.UnitOfWork;

namespace TrackingProject.Service.FirmServices
{
    public class FirmService : IFirmService
    {
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Firm> _repository;
        public FirmService(IUnitOfWork uow)
        {
            _uow = uow;
            _repository = uow.GetRepository<Firm>();
        }

        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

        public void Delete(Firm Data)
        {
            _repository.Delete(Data);
        }

        public Firm Find(int Id)
        {
            return _repository.Find(Id);
        }

        public IQueryable<Firm> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(Firm Data)
        {
            _repository.Insert(Data);
        }

        public void Update(Firm Data)
        {
            _repository.Update(Data);
        }

        public string LastCode()
        {
            if (_repository.GetAll().Any())
            {
                return _repository.GetAll().OrderByDescending(m => m.Code).Select(m => m.Code).First();
            }

            return "";
        }
    }
}
