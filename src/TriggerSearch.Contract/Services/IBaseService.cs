using System;
using System.Linq;
using System.Threading.Tasks;

namespace TriggerSearch.Contract.Services
{
    public interface IBaseService<TEntity> where TEntity: class
    {
        IQueryable<TEntity> All();
        Task Add(TEntity entity);
        Task Update(TEntity entity, params string[] fields);
        Task Delete(TEntity entity);

    }
}