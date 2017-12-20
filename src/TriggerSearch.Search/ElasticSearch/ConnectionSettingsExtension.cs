using Nest;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
