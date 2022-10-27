using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.ServiceContracts.Api;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
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

            var request = FormRequest(filter, projection, findOneOptions);
            var clientResponse = await Client.PostAsync<FindOneResponse>(request);
            return FormResponse<TP>(clientResponse, findOneOptions);
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

            var request = FormRequest(filter, projection, findOneOptions);
            var clientResponse = Client.Post<FindOneResponse>(request);
            return FormResponse<TP>(clientResponse, findOneOptions);
        }
        
         private FindOneRequest FormRequest<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, DatabaseFindOneOptions findOneOptions = null)
        {
            var request = new FindOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter.FilterToJson(),
                Projection = projection?.ProjectionToJson(),
                CultureCode = findOneOptions?.CultureCode,
                ExcludeCulture = findOneOptions?.ExcludeCulture ?? false,
                IncludeSchema = findOneOptions?.IncludeSchema ?? false,
                ReferencedFields = findOneOptions?.ReferencedFields?.ConvertAll(x => new ReferencingField
                {
                    Name = x.Name,
                    PageSize = x.PageSize,
                    PageNumber = x.PageNumber,
                    Projection = x.GetProjection,
                    Sort = x.GetSort
                }),
                AddReferencesFirst = findOneOptions?.AddReferencesFirst ?? false,
                IncludeCollectionNames = findOneOptions?.IncludeCollectionNames ?? false,
                IncludeRoleNames = findOneOptions?.IncludeRoleNames ?? false,
                IncludeTermNames = findOneOptions?.IncludeTermNames ?? false,
                IncludeUserNames = findOneOptions?.IncludeUserNames ?? false,
                Cluster = Cluster
            };

            return request;
        }
        
        private TP FormResponse<TP>(FindOneResponse clientResponse, DatabaseFindOneOptions findOneOptions = null)
        {
            if (clientResponse?.Result == null)
            {
                return default(TP);
            }

            var result = JsonConverterHelper.DeserializeEntity<TP>(clientResponse.Result, Client.Settings.CultureCode ?? findOneOptions?.CultureCode);
            return result;
        }
    }
}