using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriggerSearch.Contract.Services;
using TriggerSearch.Core;
using TriggerSearch.Core.Hooks;
using TriggerSearch.Data;
using TriggerSearch.Data.Models;
using TriggerSearch.Search;

namespace TriggerSearch.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<TEntity> _repo;
        protected readonly ISearchService _searchService;

        public BaseService(IUnitOfWork unitOfWork, ISearchService searchService):this(unitOfWork)
        {
            _searchService = searchService;
            _repo.HookFunction(TriggerSave);
        }

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.GetRepository<TEntity>();
            
        }

        public virtual  async Task<object> TriggerSave(HookTrackingResult entities)
        {
            if(entities.EntriesAdded.Count > 0)
            {
                foreach (var entry in entities.EntriesAdded)
                {
                    await _searchService.IndexAsync(entry.Entity);
                }
            }

            if (entities.EntriesModified.Count > 0)
            {
                foreach (var entry in entities.EntriesModified)
                {
                   await  _searchService.UpdateAsync(entry.Entity);
                }
            }

            if (entities.EntriesDeleted.Count > 0)
            {
                foreach (var entry in entities.EntriesDeleted)
                {
                    await _searchService.DeleteAsync(entry.Entity);
                }
            }
            return string.Empty;
        }

        public async Task Add(TEntity entity)
        {
            await _repo.InsertAsync(entity);
            await SaveChangesAsync();
        }

        public IQueryable<TEntity> All()
        {
            return _repo.Entities;
        }

        public async Task Delete(TEntity entity)
        {
            _repo.Delete(entity);
            await SaveChangesAsync();
        }

        public async Task Update(TEntity entity, params string[] fields)
        {
            _repo.Modified(entity, fields);
            await SaveChangesAsync();
        }

        protected async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
