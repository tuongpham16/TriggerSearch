using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    internal static class MapTypeSearch
    {
        internal static Dictionary<string, DocumentInfo> Map { private set; get; } = new Dictionary<string, DocumentInfo>();

        internal static void AddMap<TEntity>(DocumentInfo documentInfo)
        {
            Map.Add(typeof(TEntity).FullName, documentInfo);

        }
    }
}
