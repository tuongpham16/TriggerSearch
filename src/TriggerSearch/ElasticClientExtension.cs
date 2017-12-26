using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TriggerSearch.Search.ElasticSearch
{
    public static class ConnectionSettingsExtension
    {
        public static DocumentInfo<TEntity> Mapping<TEntity>(this IElasticClient client, Func<DocumentInfo<TEntity>, DocumentInfo<TEntity>> documentInfo) where TEntity : class
        {
            var doc = documentInfo(new DocumentInfo<TEntity>());
            if (string.IsNullOrEmpty(doc.Index))
                doc.SetIndex(client.ConnectionSettings.DefaultIndex);
            MapTypeSearch.AddMap<TEntity>(doc);
            return doc;
        }


        public static DocumentInfo<TEntity> Mapping<TEntity, TEntityTarget>(this IElasticClient client, Func<DocumentInfo<TEntity>, DocumentInfo<TEntity>> documentInfo) where TEntity : class
                        where TEntityTarget : class
        {
            var doc = documentInfo(new DocumentInfo<TEntity>());
            if (string.IsNullOrEmpty(doc.Index))
                doc.SetIndex(client.ConnectionSettings.DefaultIndex);
            doc.SetEntityTarget<TEntityTarget>();
            MapTypeSearch.AddMap<TEntity>(doc);
            return doc;
        }
    }
}
