using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    public static class MapTypeSearch
    {
        public static Dictionary<string, IDocumentInfo> Map { private set; get; } = new Dictionary<string, IDocumentInfo>();

        internal static void AddMap<TEntity>(IDocumentInfo documentInfo)
        {
            Map.Add(typeof(TEntity).FullName, documentInfo);
        }

    }
}
