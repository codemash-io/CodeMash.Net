using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts.Api;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        /* Find Async */
        public async Task<DatabaseFindResponse<T>> FindAsync(Expression<Func<T, bool>> filter = null, DatabaseFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await FindAsync<T>(new ExpressionFilterDefinition<T>(filter), null, null, findOptions);
        }
        
        public async Task<DatabaseFindResponse<T>> FindAsync(FilterDefinition<T> filter, DatabaseFindOptions findOptions = null)
        {
            return await FindAsync<T>(filter, null, null, findOptions);
        }

        public async Task<DatabaseFindResponse<T>> FindAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null)
        {
            return filter == null
                ? await FindAsync<T>(null, null, sort, findOptions)
                : await FindAsync<T>(new ExpressionFilterDefinition<T>(filter), null, sort, findOptions);
        }

        public async Task<DatabaseFindResponse<T>> FindAsync(FilterDefinition<T> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null)
        {
            return filter == null
                ? await FindAsync<T>(null, null, sort, findOptions)
                : await FindAsync<T>(filter, null, sort, findOptions);
        }
        
        public async Task<DatabaseFindResponse<TP>> FindAsync<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await FindAsync<TP>(new ExpressionFilterDefinition<T>(filter), projection, sort, findOptions);
        }

        public async Task<DatabaseFindResponse<TP>> FindAsync<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null)
        {
            var request = FormRequest(filter, projection, sort, findOptions);
            var clientResponse = await Client.PostAsync<FindResponse>(request);;
            return FormResponse<TP>(clientResponse, findOptions);
        }
        
        
        
        /* Find */

        public DatabaseFindResponse<T> Find(Expression<Func<T, bool>> filter = null, DatabaseFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<T>(new ExpressionFilterDefinition<T>(filter), null, null, findOptions);
        }
        
        public DatabaseFindResponse<T> Find(FilterDefinition<T> filter, DatabaseFindOptions findOptions = null)
        {
            return Find<T>(filter, null, null, findOptions);
        }

        public DatabaseFindResponse<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null)
        {
            return filter == null
                ? Find<T>(new BsonDocument(), null, sort, findOptions)
                : Find<T>(new ExpressionFilterDefinition<T>(filter), null, sort, findOptions);
        }

        public DatabaseFindResponse<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null)
        {
            return filter == null
                ? Find<T>(new BsonDocument(), null, sort, findOptions)
                : Find<T>(filter, null, sort, findOptions);
        }
        
        public DatabaseFindResponse<TP> Find<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<TP>(new ExpressionFilterDefinition<T>(filter), projection, sort, findOptions);
        }

        public DatabaseFindResponse<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null)
        {
            var request = FormRequest(filter, projection, sort, findOptions);
            var clientResponse = Client.Post<FindResponse>(request);;
            return FormResponse<TP>(clientResponse, findOptions);
        }

        private FindRequest FormRequest<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null)
        {
            var request = new FindRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Projection = projection?.ProjectionToJson(),
                Sort = sort?.SortToJson(),
                PageSize = findOptions?.PageSize ?? 1000,
                PageNumber = findOptions?.PageNumber ?? 0,
                CultureCode = findOptions?.CultureCode,
                ExcludeCulture = findOptions?.ExcludeCulture ?? false,
                ReferencedFields = findOptions?.ReferencedFields?.ConvertAll(x => new ReferencingField
                {
                    Name = x.Name,
                    PageSize = x.PageSize,
                    PageNumber = x.PageNumber,
                    Projection = x.GetProjection,
                    Sort = x.GetSort
                }),
                AddReferencesFirst = findOptions?.AddReferencesFirst ?? false,
                IncludeCollectionNames = findOptions?.IncludeCollectionNames ?? false,
                IncludeRoleNames = findOptions?.IncludeRoleNames ?? false,
                IncludeTermNames = findOptions?.IncludeTermNames ?? false,
                IncludeUserNames = findOptions?.IncludeUserNames ?? false,
                Cluster = Cluster,
            };

            return request;
        }
        
        private DatabaseFindResponse<TP> FormResponse<TP>(FindResponse clientResponse, DatabaseFindOptions findOptions = null)
        {
            if (clientResponse?.Result == null)
            {
                return new DatabaseFindResponse<TP>()
                {
                    Items = new List<TP>()
                };
            }

            var list = JsonConverterHelper.DeserializeEntity<List<TP>>(clientResponse.Result, Client.Settings.CultureCode ?? findOptions?.CultureCode);
            return new DatabaseFindResponse<TP>()
            {
                Items = list,
                TotalCount = clientResponse.TotalCount
            };
        }
    }
}