using Nest;
using System;
using System.Collections.Generic;
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
                var id = entity.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entity, null);
                await _elasticClient.DeleteAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)));
            }
        }

        public async Task IndexAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                var id = entity.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entity, null);
                await _elasticClient.IndexAsync(entity, i => i.Index(docInfo.Index)
                                                              .Type(docInfo.Type)
                                                              .Id(Convert.ToString(id)));
            }
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var fullName = entity.GetType().FullName;
            if (MapTypeSearch.Map.ContainsKey(fullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                var id = entity.GetType().GetProperty(docInfo.KeyPropertyName).GetValue(entity, null);
                await _elasticClient.UpdateAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)), d => d
                    .Index(docInfo.Index)
                    .Type(docInfo.Type)
                    .DocAsUpsert(true)
                    .Doc(entity));
            }
        }

       
    }
}
