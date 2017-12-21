using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Search.ElasticSearch
{
    public static class ConnectionSettingsExtension
    {
        public static IElasticClient Mapping<TEntity>(this IElasticClient client, Func<DocumentInfo<TEntity>, DocumentInfo> documentInfo) where TEntity : class
        {
            var doc = documentInfo(new DocumentInfo<TEntity>());
            if (string.IsNullOrEmpty(doc.Index))
                doc.Index = client.ConnectionSettings.DefaultIndex;
            MapTypeSearch.AddMap<TEntity>(doc);
            return client;
        }

        public static IElasticClient Mapping<TEntity, TEntityTarget>(this IElasticClient client, Func<DocumentInfo<TEntity>, DocumentInfo> documentInfo) where TEntity : class
        {
            var doc = documentInfo(new DocumentInfo<TEntity>());
            if (string.IsNullOrEmpty(doc.Index))
                doc.Index = client.ConnectionSettings.DefaultIndex;
            doc.EntityTarget = typeof(TEntityTarget);
            MapTypeSearch.AddMap<TEntity>(doc);
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
