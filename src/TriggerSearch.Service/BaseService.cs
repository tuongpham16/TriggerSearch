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

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = unitOfWork.GetRepository<TEntity>();
            //RegisterHookFunction();
        }

       
        private void RegisterHookFunction()
        {
            var indexService = SearchServiceLocator.GetService<ITriggerService>();
            _repo.HookFunction(indexService.TriggerSave);
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
