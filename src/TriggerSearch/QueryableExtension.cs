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
using System.Collections;

namespace TriggerSearch
{
     public static class QueryableExtension
    {
        #region method

        internal static readonly MethodInfo IncludeMethodInfo
           = typeof(QueryableExtension)
               .GetTypeInfo().GetDeclaredMethods(nameof(SearchIncludeInternal))
               .FirstOrDefault();

        internal static readonly MethodInfo ThenIncludeMethodInfo
           = typeof(QueryableExtension)
               .GetTypeInfo().GetDeclaredMethods(nameof(SearchThenIncludeInternal))
               .FirstOrDefault();

        internal static readonly MethodInfo ThenIncludeMethodInfoEnumerable
          = typeof(QueryableExtension)
              .GetTypeInfo().GetDeclaredMethods(nameof(SearchThenIncludeEnumerable))
              .FirstOrDefault();


        internal static readonly MethodInfo ThenIncludeMethodInfoCollection
          = typeof(QueryableExtension)
              .GetTypeInfo().GetDeclaredMethods(nameof(SearchThenIncludeCollection))
              .FirstOrDefault();

        #endregion

        #region FirstOrDefault

        public static Task<TEntity> FirstOrDefaultAsync<TEntity>( this IQueryable<TEntity> source, string propertyName, object value)
        {

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "item");
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression rightSide = Expression.Constant(value);
            BinaryExpression operation = Expression.Equal(property, rightSide);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), typeof(bool));
            Expression<Func<TEntity,bool>> predicate = (Expression <Func<TEntity, bool>>)Expression.Lambda(delegateType, operation, parameter);

            return source.FirstOrDefaultAsync(predicate);

        }

        #endregion

        #region Include

        public static IQueryable<TEntity> SearchInclude<TEntity>(this IQueryable<TEntity> source, LambdaExpression expression)
        {

            MethodInfo genericMethod = IncludeMethodInfo.MakeGenericMethod(typeof(TEntity), expression.Body.Type);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { source, expression });
        }

        private static IQueryable<TEntity> SearchIncludeInternal<TEntity, TProperty>(IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.Include(navigationPropertyPath);
        }

        #endregion

        #region ThenInclude

        public static IQueryable<TEntity> SearchThenInclude<TEntity>(this IQueryable<TEntity> source, LambdaExpression expression)
        {
            MethodInfo genericMethod = null;
            var typePreviousProperty = source.GetType().GetGenericArguments()[1];
            if (typePreviousProperty.GetInterface(nameof(ICollection)) != null)
            {
                genericMethod = ThenIncludeMethodInfoCollection;
            }
            else if (typePreviousProperty.GetInterface(nameof(IEnumerable)) != null)
            {
                genericMethod = ThenIncludeMethodInfoEnumerable;
            }
            else
            {
                genericMethod = ThenIncludeMethodInfo;
            }
            genericMethod = genericMethod.MakeGenericMethod(typeof(TEntity), expression.Parameters[0].Type, expression.Body.Type);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { source, expression });
        }



        private static IIncludableQueryable<TEntity, TProperty> SearchThenIncludeCollection<TEntity, TPreviousProperty, TProperty>(IIncludableQueryable<TEntity, ICollection<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.ThenInclude(navigationPropertyPath);
        }

        private static IIncludableQueryable<TEntity, TProperty> SearchThenIncludeEnumerable<TEntity, TPreviousProperty, TProperty>(IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.ThenInclude(navigationPropertyPath);
        }

        private static IIncludableQueryable<TEntity, TProperty> SearchThenIncludeInternal<TEntity, TPreviousProperty, TProperty>(IIncludableQueryable<TEntity, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.ThenInclude(navigationPropertyPath);
        }

        #endregion


    }
}
