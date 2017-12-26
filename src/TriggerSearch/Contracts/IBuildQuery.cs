using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TriggerSearch
{
    public interface IBuildQuery
    {
        Dictionary<LambdaExpression, TypeExpression> IncludeExpression { get; }
    }

    public interface IBuildQuery<out TEntity> : IBuildQuery
    {

    }

    public interface IIncludableBuilder<out TEntity, out TProperty> : IIncludableQueryBuilder<TEntity>
    {
    }

    public interface IIncludableQueryBuilder<out TEntity> : IBuildQuery<TEntity>
    {
        
    }
}
