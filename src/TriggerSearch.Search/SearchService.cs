using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TriggerSearch.Search
{
    public class SearchService : ISearchService
    {
        private readonly IElasticClient _elasticClient;
        public SearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
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
            //Call Method or implicit for convert Entity Db to Search Enity 
            object entityTarget = Activator.CreateInstance(target);
            entityTarget = entityTarget.GetType().GetMethod("FromEntity").Invoke(entityTarget, new object[] { entity });
            return entityTarget;
        }

    }
}
