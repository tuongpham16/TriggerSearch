using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TriggerSearch.Search;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Internal;

namespace TriggerSearch.Search
{
    public class IndexService : IIndexService
    {
        private readonly IElasticClient _elasticClient;
        private DbContext _dbContext;
        public IndexService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void SetDbContext(DbContext context)
        {
            _dbContext = context;
        }

        #region Method Info

        protected internal static readonly MethodInfo IndexAsyncMethodInfo
            = typeof(IndexService)
            .GetTypeInfo().GetDeclaredMethods(nameof(CommitIndexAsync))
            .First();

        protected internal static readonly MethodInfo UpdateAsyncMethodInfo
           = typeof(IndexService)
           .GetTypeInfo().GetDeclaredMethods(nameof(UpdateAsync))
           .Single(m => m.GetParameters().Length == 2);

        protected internal static readonly MethodInfo CommitUpdateAsyncMethodInfo
           = typeof(IndexService)
           .GetTypeInfo().GetDeclaredMethods(nameof(CommitUpdateAsync))
           .First();

        protected internal static readonly MethodInfo DeleteAsyncMethodInfo
          = typeof(IndexService)
          .GetTypeInfo().GetDeclaredMethods(nameof(CommitDeleteAsync))
          .First();

        #endregion

        #region Delete
        public async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];
                var id = entity.GetType().GetProperty(docInfo.KeyProperty.Name).GetValue(entity, null);

                object entitySave = entity;
                if (docInfo.EntityTarget != null)
                {
                    entitySave = Entity2Target(entity, docInfo.EntityTarget);
                }
                MethodInfo genericMethod = DeleteAsyncMethodInfo.MakeGenericMethod(entitySave.GetType());
                Task task = (Task)genericMethod.Invoke(this, new object[] { entitySave, docInfo, id });
                await task;
            }
        }

        private async Task CommitDeleteAsync<TEntity>(TEntity entity, IDocumentInfo docInfo, object id) where TEntity : class
        {
            await _elasticClient.DeleteAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)),
                d => d.Index(docInfo.Index)
                      .Type(docInfo.Type));
            if (docInfo.RefeshAfterDeleted)
                await _elasticClient.RefreshAsync(docInfo.Index);
        }
        #endregion

        #region Index
        public async Task IndexAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];

                MethodInfo genericMethod = IndexAsyncMethodInfo.MakeGenericMethod(entity.GetType());
                Task task = (Task)genericMethod.Invoke(this, new object[] { entity, docInfo });
                await task;
            }
        }

        private async Task CommitIndexAsync<TEntity>(TEntity entity, IDocumentInfo docInfo) where TEntity : class
        {
            var id = entity.GetType().GetProperty(docInfo.KeyProperty.Name).GetValue(entity, null);

            if ((docInfo.LoadQueryBehavior == BehaviorChange.ALL
                 || docInfo.LoadQueryBehavior == BehaviorChange.Insert)
                 && docInfo.Query.IncludeExpression?.Count > 0)
            {
                var expressions = docInfo.Query.IncludeExpression;
                IQueryable<TEntity> source = _dbContext.Set<TEntity>();

                foreach (var expression in expressions)
                {
                    if (expression.Value == TypeExpression.Include)
                        source = source.SearchInclude(expression.Key);
                    else
                        source = source.SearchThenInclude(expression.Key);
                }

                entity = await source.FirstOrDefaultAsync(docInfo.KeyProperty.Name, id);
            }


            if ((docInfo.LoadReferenceBehavior == BehaviorChange.ALL
                || docInfo.LoadReferenceBehavior == BehaviorChange.Insert)
                && docInfo.References?.Length > 0)
            {
                foreach (var property in docInfo.References)
                {
                   await _dbContext.Entry(entity).Reference(property).LoadAsync();
                }
            }

            object entitySave = entity;
            if (docInfo.EntityTarget != null)
            {
                entitySave = Entity2Target(entity, docInfo.EntityTarget);
            }

            await _elasticClient.IndexAsync(entitySave, i => i.Index(docInfo.Index)
                                                          .Type(docInfo.Type)
                                                          .Id(Convert.ToString(id)));
            if (docInfo.RefeshAfterIndex)
                await _elasticClient.RefreshAsync(docInfo.Index);
        }

        #endregion

        #region Update

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (MapTypeSearch.Map.ContainsKey(entity.GetType().FullName))
            {
                var docInfo = MapTypeSearch.Map[entity.GetType().FullName];

                MethodInfo genericMethod = UpdateAsyncMethodInfo.MakeGenericMethod(entity.GetType());
                Task task = (Task)genericMethod.Invoke(this, new object[] { entity, docInfo });
                await task;
            }
        }

        private async Task UpdateAsync<TEntity>(TEntity entity, IDocumentInfo docInfo) where TEntity : class
        {
            var id = entity.GetType().GetProperty(docInfo.KeyProperty.Name).GetValue(entity, null);

            if ((docInfo.LoadQueryBehavior == BehaviorChange.ALL
                || docInfo.LoadQueryBehavior == BehaviorChange.Insert)
                && docInfo.Query.IncludeExpression?.Count > 0)
            {
                var expressions = docInfo.Query.IncludeExpression;
                IQueryable<TEntity> source = _dbContext.Set<TEntity>();

                foreach (var expression in expressions)
                {
                    if (expression.Value == TypeExpression.Include)
                        source = source.SearchInclude(expression.Key);
                    else
                        source = source.SearchThenInclude(expression.Key);
                }
                entity = await source.FirstOrDefaultAsync(docInfo.KeyProperty.Name, id);
            }

            if ((docInfo.LoadReferenceBehavior == BehaviorChange.ALL
                || docInfo.LoadReferenceBehavior == BehaviorChange.Update)
                && docInfo.References?.Length > 0)
            {
                foreach (var property in docInfo.References)
                {
                    await _dbContext.Entry(entity).Reference(property).LoadAsync();
                }
            }

            if (docInfo.Collections?.Length > 0)
            {
                foreach (var property in docInfo.Collections)
                {
                    await _dbContext.Entry(entity).Collection(property).LoadAsync();
                }
            }

            object entitySave = entity;
            if (docInfo.EntityTarget != null)
            {
                entitySave = Entity2Target(entity, docInfo.EntityTarget);
            }


            MethodInfo genericMethod = CommitUpdateAsyncMethodInfo.MakeGenericMethod(entitySave.GetType());
            Task task = (Task)genericMethod.Invoke(this, new object[] { entitySave, docInfo, id });
            await task;
        }

        private async Task CommitUpdateAsync<TEntity>(TEntity entity, IDocumentInfo docInfo, object id) where TEntity : class
        {
            await _elasticClient.UpdateAsync(DocumentPath<TEntity>.Id(Convert.ToString(id)), d => d
                              .Index(docInfo.Index)
                              .Type(docInfo.Type)
                              .DocAsUpsert(true)
                              .Doc(entity));
            if (docInfo.RefeshAfterUpdate)
                await _elasticClient.RefreshAsync(docInfo.Index);
        }
        #endregion

        private object Entity2Target<TEntity>(TEntity entity, Type typeTarget)
        {

            var conversionOperator = typeTarget.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                  .Where(m => m.Name == "op_Implicit")
                                  .Where(m => m.ReturnType == typeTarget)
                                  .Where(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == entity.GetType())
                                  .FirstOrDefault();

            if (conversionOperator != null)
                return conversionOperator.Invoke(null, new object[] { entity });
            else
                throw new Exception($"{typeTarget.FullName} has no implicit method to {entity.GetType().FullName}");
        }

    }
}
