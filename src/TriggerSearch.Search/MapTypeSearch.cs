using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    internal static class MapTypeSearch
    {
        internal static Dictionary<string, DocumentInfo> Map { private set; get; } = new Dictionary<string, DocumentInfo>();

        internal static void AddMap<TEntity>(string index, string type, string keyPropertyName)
        {
            Map.Add(typeof(TEntity).FullName, new DocumentInfo()
            {
                Index = index,
                KeyPropertyName = keyPropertyName,
                Type = type
            });
        }

        internal static void AddMap<TEntity, TEntityTarget>(string index, string type, string keyPropertyName)
        {

            Map.Add(typeof(TEntity).FullName, new DocumentInfo()
            {
                Index = index,
                KeyPropertyName = keyPropertyName,
                Type = type,
                EntityTarget = typeof(TEntityTarget)

            });
        }
    }
}
