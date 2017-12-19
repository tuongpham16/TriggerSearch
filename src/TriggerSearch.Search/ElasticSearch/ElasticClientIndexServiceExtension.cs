using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TriggerSearch.Data.Models;

namespace TriggerSearch.Search.ElasticSearch
{
    public static class ElasticClientIndexServiceExtension
    {
        public static void EnsureIndex(this IElasticClient client, string indexName)
        {
            var isExist = client.IndexExists(indexName).Exists;

            if (isExist)
                return;

            var newIndex = client.CreateIndex(indexName, c => c.Settings(s => s
                                                                   .NumberOfShards(2)
                                                                   .NumberOfReplicas(0)
                                                                   .RefreshInterval(-1))
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
