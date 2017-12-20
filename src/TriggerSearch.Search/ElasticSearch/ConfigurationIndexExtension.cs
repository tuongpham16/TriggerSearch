using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using TriggerSearch.Data.Models;
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
            _client.EnsureIndex(_client.ConnectionSettings.DefaultIndex);
            services.AddSingleton(_client);
            services.AddScoped<IIndexService, IndexService>();
            services.AddScoped<ITriggerService, TriggerService>();

            SearchServiceLocator.SetServiceLocator(services.BuildServiceProvider());
            _client.Mapping<Group, GroupDTO>("groupdto", "ID");
            return services;
        }

        public static Configuration GetElasticConfiguration(this IConfiguration config)
        {
            var elasticConfig = new Configuration();
            config.GetSection("ElasticSearch").Bind(elasticConfig);
            return elasticConfig;
        }
    }
}
