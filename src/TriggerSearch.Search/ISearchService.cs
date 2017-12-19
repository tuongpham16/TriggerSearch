using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Search
{
   public interface ISearchService
    {
        Task IndexAsync<TEntity>(TEntity entity) where TEntity : class;

        Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
