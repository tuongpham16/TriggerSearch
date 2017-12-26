using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using TriggerSearch.Search;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Internal;

namespace TriggerSearch
{
     public static class QueryableExtension
    {
        #region method

        internal static readonly MethodInfo IncludeMethodInfo
           = typeof(QueryableExtension)
               .GetTypeInfo().GetDeclaredMethods(nameof(PrivateInclude))
               .FirstOrDefault();

        #endregion

        public static IQueryable SearchInclude(this IQueryable source, LambdaExpression navigationPropertyPath)
        {

            var callExpr =  Expression.Call(
                           instance: null,
                           method: IncludeMethodInfo.MakeGenericMethod(navigationPropertyPath.Parameters[0].Type, navigationPropertyPath.Body.Type),
                           arguments: new[] { source.Expression, Expression.Quote(navigationPropertyPath) });

           return Expression.Lambda<Func<IQueryable>>(callExpr).Compile().Invoke();

        }
        public static IQueryable<TEntity> PrivateInclude<TEntity, TProperty>(IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
          
            return source.Include(navigationPropertyPath);
        }
    }
}
