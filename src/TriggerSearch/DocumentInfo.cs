using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;
using System.Reflection;
using Nest;
using System.Threading.Tasks;

namespace TriggerSearch.Search
{
    public class DocumentInfo<TEntity> : IDocumentInfo
    {
        public DocumentInfo()
        {
            MakeMethods = new HashSet<MakeMethod>()
            {
                MakeMethod.Update,
                MakeMethod.Insert,
                MakeMethod.Delete
            };
        }
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

        public DocumentInfo<TEntity> SetKeyProperty<TResult>(Expression<Func<TEntity, TResult>> keySelector)
        {
            KeyProperty = ((MemberExpression)keySelector.Body).Member;
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

        public DocumentInfo<TEntity> SetReferenceBehavior(BehaviorChange referenceBehavior)
        {
            LoadReferenceBehavior = referenceBehavior;
            return this;
        }

        public DocumentInfo<TEntity> SetLoadQueryBehavior(BehaviorChange referenceBehavior)
        {
            LoadQueryBehavior = referenceBehavior;
            return this;
        }

        public DocumentInfo<TEntity> SetQuery(IBuildQuery<TEntity> query)
        {
            Query = query;
            return this;
        }

        public DocumentInfo<TEntity> SetMethodIndex(Func<IElasticClient, TEntity, Task> method)
        {
            IndexMethod = method;
            return this;
        }

        public DocumentInfo<TEntity> SetMethodUpdate(Func<IElasticClient, TEntity, Task> method)
        {
            UpdateMethod = method;
            return this;
        }

        public DocumentInfo<TEntity> SetMethodDelete(Func<IElasticClient, TEntity, Task> method)
        {
            DeleteMethod = method;
            return this;
        }

        public DocumentInfo<TEntity> SetMakeMethod(params MakeMethod[] makeMethods)
        {
            MakeMethods = new HashSet<MakeMethod>(makeMethods);
            return this;
        }

        public IBuildQuery Query { get; set; }

        public string Index { get; internal set; }
        public string Type { get; private set; }
        public MemberInfo KeyProperty { get; private set; }
        public Type EntityTarget { get; private set; }
        public string[] References { get; private set; }
        public string[] Collections { get; private set; }
        public bool RefeshAfterIndex { get; private set; }
        public bool RefeshAfterUpdate { get; private set; }
        public bool RefeshAfterDeleted { get; private set; }
        public BehaviorChange LoadReferenceBehavior { get; private set; }
        public BehaviorChange LoadQueryBehavior { get; private set; }
        public Func<IElasticClient, TEntity, Task> IndexMethod { get; private set; }
        public Func<IElasticClient, TEntity, Task> UpdateMethod { get; private set; }
        public Func<IElasticClient, TEntity, Task> DeleteMethod { get; private set; }
        public HashSet<MakeMethod> MakeMethods { get; private set; }

    }
}
