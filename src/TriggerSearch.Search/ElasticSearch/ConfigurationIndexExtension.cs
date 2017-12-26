using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriggerSearch.Core.Hooks;
using TriggerSearch.Data.Models;
using TriggerSearch.Expressions;
using TriggerSearch.Search.DTO;

namespace TriggerSearch.Search.ElasticSearch
{
    public static class ConfigurationIndexExtension
    {
        private static IElasticClient _client;
        public static IServiceCollection AddElasticSearchClient(this IServiceCollection services, Configuration configuration)
        {
            var pool = new StaticConnectionPool(configuration.NodesUri);
            var settings = new ConnectionSettings(pool);
            settings.DefaultIndex(configuration.DefaultIndex);
            _client = new ElasticClient(settings);
            EnsureIndex(_client, _client.ConnectionSettings.DefaultIndex);
            services.AddSingleton(_client);
            services.AddScoped<IIndexService, IndexService>();
            services.AddScoped<ITriggerService, TriggerService>();
            IHookFunction hookFunction = new HookFunction();
            services.AddSingleton(hookFunction);
            services.AddSingleton<IExecuteTrigger, ExecuteTrigger>();
            services.BuildServiceProvider().GetService<IExecuteTrigger>();

            _client.Mapping<Group, GroupDTO>(s => s.SetType("group")
                                             .SetKeyProperty(item => item.ID)
                                             .SetMethodDelete(DeleteGroupIndex)
                                             .SetMakeMethod(MakeMethod.Delete, MakeMethod.Insert)
                                             .SetQuery(QueryExpression.BuildQuery<Group>()
                                                        .Include(item => item.GroupRoles)
                                                        .ThenInclude(item => item.Role)
                                                        .Include(item => item.GroupUsers)
                                                        .ThenInclude(item => item.User)));
            return services;
        }

        public static Configuration GetElasticConfiguration(this IConfiguration config)
        {
            var elasticConfig = new Configuration();
            config.GetSection("ElasticSearch").Bind(elasticConfig);
            return elasticConfig;
        }

        private static async Task DeleteGroupIndex(IElasticClient client, Group group)
        {
            await client.DeleteAsync(DocumentPath<GroupDTO>.Id(Convert.ToString(group.ID)),
                   d => d.Index(client.ConnectionSettings.DefaultIndex)
                         .Type("group"));
        }

        public static void EnsureIndex(IElasticClient client, string indexName)
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
