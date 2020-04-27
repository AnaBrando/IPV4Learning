using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        TEntity GetById(string id);
        void Add(TEntity e);
        bool Update(TEntity e);
        Task Delete(TEntity e);
       
    }
}
