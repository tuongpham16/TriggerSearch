using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Expressions
{
    public static class QueryExpression
    {
        public static BuildQuery<TEntity> BuildQuery<TEntity>()
        {
            return new BuildQuery<TEntity>();
        }
    }
}
