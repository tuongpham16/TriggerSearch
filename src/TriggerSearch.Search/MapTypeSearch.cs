using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    public static class MapTypeSearch
    {
        private static string _defaultIndex;

        public static Dictionary<string, DocumentInfo> Map { private set; get; } = new Dictionary<string, DocumentInfo>();
        public static void SetDefaultIndex(string indexName)
        {
            _defaultIndex = indexName;
        }

        public static void AddMap<TEntity>(string index, string type, string keyPropertyName)
        {
            var fullName = typeof(TEntity).FullName;
            Map.Add(fullName, new DocumentInfo()
            {
                Index = index,
                KeyPropertyName = keyPropertyName,
                Type = type
            });
        }
        public static void AddMap<TEntity>(string type, string keyPropertyName)
        {
            AddMap<TEntity>(_defaultIndex, type, keyPropertyName);
        }
    }
}
