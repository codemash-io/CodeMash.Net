using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Find One Async */
        public async Task<T> FindOneByIdAsync(string id, DatabaseFindOneOptions findOneOptions = null)
        {
            return await FindOneAsync<T>(x => x.Id == id, null, findOneOptions);
        }

        public async Task<T> FindOneByIdAsync(ObjectId id, DatabaseFindOneOptions findOneOptions = null)
        {
            return await FindOneAsync<T>(x => x.Id == id.ToString(), null, findOneOptions);
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null)
        {
            return await FindOneAsync<T>(filter, null, findOneOptions);
        }

        public async Task<T> FindOneAsync(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null)
        {
            return await FindOneAsync<T>(filter, null, findOneOptions);
        }

        public async Task<TP> FindOneAsync<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null,
            DatabaseFindOneOptions findOneOptions = null)
        {
            return await FindOneAsync(new ExpressionFilterDefinition<T>(filter), projection, findOneOptions);
        }

        public async Task<TP> FindOneAsync<TP>(FilterDefinition<T> filter, 
            ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter must be set");
            }

            var request = new FindOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter.FilterToJson(),
                Projection = projection?.ProjectionToJson(),
                CultureCode = findOneOptions?.CultureCode,
                ExcludeCulture = findOneOptions?.ExcludeCulture ?? false,
                IncludeSchema = findOneOptions?.IncludeSchema ?? false,
            };

            var clientResponse = await Client.PostAsync<FindOneResponse>(request);
            if (clientResponse?.Result == null)
            {
                return default(TP);
            }

            var result = JsonConverterHelper.DeserializeEntity<TP>(clientResponse.Result, Client.Settings.CultureCode ?? findOneOptions?.CultureCode);
            return result;
        }
        
        
        
        /* Find One */
        public T FindOneById(string id, DatabaseFindOneOptions findOneOptions = null)
        {
            return FindOne<T>(x => x.Id == id, null, findOneOptions);
        }

        public T FindOneById(ObjectId id, DatabaseFindOneOptions findOneOptions = null)
        {
            return FindOne<T>(x => x.Id == id.ToString(), null, findOneOptions);
        }

        public T FindOne(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null)
        {
            return FindOne<T>(filter, null, findOneOptions);
        }

        public T FindOne(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null)
        {
            return FindOne<T>(filter, null, findOneOptions);
        }

        public TP FindOne<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null,
            DatabaseFindOneOptions findOneOptions = null)
        {
            return FindOne(new ExpressionFilterDefinition<T>(filter), projection, findOneOptions);
        }

        public TP FindOne<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter must be set");
            }

            var request = new FindOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter.FilterToJson(),
                Projection = projection?.ProjectionToJson(),
                CultureCode = findOneOptions?.CultureCode,
                ExcludeCulture = findOneOptions?.ExcludeCulture ?? false,
                IncludeSchema = findOneOptions?.IncludeSchema ?? false,
            };

            var clientResponse = Client.Post<FindOneResponse>(request);
            if (clientResponse?.Result == null)
            {
                return default(TP);
            }

            var result = JsonConverterHelper.DeserializeEntity<TP>(clientResponse.Result, Client.Settings.CultureCode ?? findOneOptions?.CultureCode);
            return result;
        }
    }
}