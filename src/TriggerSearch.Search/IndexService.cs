using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TriggerSearch.Search
{
    public class IndexService : IIndexService
    {
        private readonly IElasticClient _elasticClient;
        private DbContext _dbContext;
        public IndexService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void SetDbContext(DbContext context)
        {
            _dbContext = context;
        }

        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                object entitySave = entity;
                if (docInfo.EntityTarget != null)
                {
                    entitySave = Entity2Target(entity, docInfo.EntityTarget);
                }

                await DeleteAsync(entitySave, docInfo);
            }
        }

        private async Task DeleteAsync<TEntity>(TEntity entity, DocumentInfo docInfo) where TEntity : class
        {
            object entitySave = entity;
            if (docInfo.EntityTarget != null)
            {
                entitySave = Entity2Target(entity, docInfo.EntityTarget);
            }

            var id = entitySave.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entitySave, null);
            await _elasticClient.DeleteAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)));

        }

        public async Task IndexAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                object entitySave = entity;
                if (docInfo.EntityTarget != null)
                {
                    entitySave = Entity2Target(entity, docInfo.EntityTarget);
                }

                var id = entitySave.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entitySave, null);
                await _elasticClient.IndexAsync(entitySave, i => i.Index(docInfo.Index)
                                                              .Type(docInfo.Type)
                                                              .Id(Convert.ToString(id)));
            }
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                if (docInfo.References != null && docInfo.References.Length > 0)
                {
                    foreach (var property in docInfo.References)
                    {
                        _dbContext.Entry(entity).Reference(property).Load();
                    }
                }

                if (docInfo.Collections != null && docInfo.Collections.Length > 0)
                {
                    foreach (var property in docInfo.Collections)
                    {
                        _dbContext.Entry(entity).Collection(property).Load();
                    }
                }

                object entitySave = entity;
                if (docInfo.EntityTarget != null)
                {
                    entitySave = Entity2Target(entity, docInfo.EntityTarget);
                }

                await UpdateAsync(entitySave, docInfo);
            }
        }

        private async Task UpdateAsync<TEntity>(TEntity entity, DocumentInfo docInfo) where TEntity : class
        {
            var id = entity.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entity, null);
            await _elasticClient.UpdateAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)), d => d
                .Index(docInfo.Index)
                .Type(docInfo.Type)
                .DocAsUpsert(true)
                .Doc(entity));
        }

        private object Entity2Target<TEntity>(TEntity entity, Type target)
        {
            object entityTarget = Activator.CreateInstance(target);
            return CastEntity(entity, entityTarget);
        }

        private TEntityTarget CastEntity<TEntity, TEntityTarget>(TEntity entity, TEntityTarget entityTarget)
        {

            var conversionOperator = entityTarget.GetType().GetMethods(BindingFlags.Static | BindingFlags.Public)
                                    .Where(m => m.Name == "op_Implicit")
                                    .Where(m => m.ReturnType == entityTarget.GetType())
                                    .Where(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == entity.GetType())
                                    .FirstOrDefault();

            if (conversionOperator != null)
                entityTarget = (TEntityTarget)conversionOperator.Invoke(null, new object[] { entity });
            else
                throw new Exception($"{entityTarget.GetType().FullName} has no implicit method to {entity.GetType().FullName}");

            return entityTarget;
        }


    }
}
