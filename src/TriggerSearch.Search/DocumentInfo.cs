using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    public class DocumentInfo<TEntity> : DocumentInfo
    {
        public DocumentInfo<TEntity> SetIndex(string index)
        {
            Index = index;
            return this;
        }
        public DocumentInfo<TEntity> SetType(string type)
        {
            Type = type;
            return this;
        }

        public DocumentInfo<TEntity> SetKeyPropertyName(string keyPropertyName)
        {
            KeyPropertyName = keyPropertyName;
            return this;
        }

        public DocumentInfo<TEntity> SetReferences(params string[] references)
        {
            References = references;
            return this;
        }

        public DocumentInfo<TEntity> SetCollections(params string[] collections)
        {
            Collections = collections;
            return this;
        }

        public DocumentInfo<TEntity> SetEntityTarget<TEntityTarget>() where TEntityTarget : class
        {
            EntityTarget = typeof(TEntityTarget);
            return this;
        }

        public DocumentInfo<TEntity> AutoRefeshAfterIndex(bool auto)
        {
            RefeshAfterIndex = auto;
            return this;
        }

        public DocumentInfo<TEntity> AutoRefeshAfterUpdate(bool auto)
        {
            RefeshAfterUpdate = auto;
            return this;
        }
        public DocumentInfo<TEntity> AutoRefeshAfterDeleted(bool auto)
        {
            RefeshAfterDeleted = auto;
            return this;
        }
    }
    public class DocumentInfo
    {
        public string Index { get; set; }
        public string Type { get; set; }
        public string KeyPropertyName { get; set; }
        public Type EntityTarget { get; set; }
        public string[] References { get; set; }
        public string[] Collections { get; set; }
        public bool RefeshAfterIndex { get; set; }
        public bool RefeshAfterUpdate { get; set; }
        public bool RefeshAfterDeleted { get; set; }
    }
}
