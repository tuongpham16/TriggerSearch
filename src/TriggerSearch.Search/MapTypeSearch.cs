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
            var fullName = typeof(TEntity).FullName;
            Map.Add(fullName, new DocumentInfo()
            {
                Index = index,
                KeyPropertyName = keyPropertyName,
                Type = type
            });
        }
    }
}
