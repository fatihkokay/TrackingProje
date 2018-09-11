using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingProject.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TEntity Find(int id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(int id);
        void Delete(TEntity entityToDelete);
    }
}
