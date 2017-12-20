using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Search.ElasticSearch
{
    public static class ConnectionSettingsExtension
    {
        public static IElasticClient Mapping<TEntity>(this IElasticClient client, string index, string type, string keyPropertyName) where TEntity : class
        {
            MapTypeSearch.AddMap<TEntity>(index, type, keyPropertyName);
            return client;
        }

        public static IElasticClient Mapping<TEntity>(this IElasticClient client, string type, string keyPropertyName) where TEntity : class
        {
            MapTypeSearch.AddMap<TEntity>(client.ConnectionSettings.DefaultIndex, type, keyPropertyName);
            return client;
        }

        public static IElasticClient Mapping<TEntity, TEntityTarget>(this IElasticClient client, string index, string type, string keyPropertyName) where TEntity : class
        {
            MapTypeSearch.AddMap<TEntity, TEntityTarget>(index, type, keyPropertyName);
            return client;
        }

        public static IElasticClient Mapping<TEntity, TEntityTarget>(this IElasticClient client, string type, string keyPropertyName) where TEntity : class
        {
            MapTypeSearch.AddMap<TEntity, TEntityTarget>(client.ConnectionSettings.DefaultIndex, type, keyPropertyName);
            return client;
        }

        public static void EnsureIndex(this IElasticClient client, string indexName)
        {
            var isExist = client.IndexExists(indexName).Exists;

            if (isExist)
                return;

            var newIndex = client.CreateIndex(indexName, c => c.Settings(s => s
                                                                   .NumberOfShards(2)
                                                                   .NumberOfReplicas(0))
                                                               .Mappings(m => m
                                                                    .Map<Group>(GroupMapper)));
            if (!newIndex.Acknowledged)
            {
                throw new Exception($"Cannot create Elastic Index: {newIndex.DebugInformation}");
            }
        }

        private static TypeMappingDescriptor<Group> GroupMapper(TypeMappingDescriptor<Group> mapper) => mapper.AutoMap();
    }
}
