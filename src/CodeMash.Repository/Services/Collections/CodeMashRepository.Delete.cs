using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.ServiceContracts.Api;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        /* Delete Async */
        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(ObjectId id, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            return await DeleteOneAsync(id.ToString(), deleteOneOptions);
        }
        
        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(string id, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            return await DeleteOneAsync(new ExpressionFilterDefinition<T>(x => x.Id == id), deleteOneOptions);
        }

        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(Expression<Func<T, bool>> filter, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await DeleteOneAsync(new ExpressionFilterDefinition<T>(filter), deleteOneOptions);
        }

        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(FilterDefinition<T> filter, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            var request = new DeleteOneRequest
            {
                Filter = filter?.FilterToJson(),
                CollectionName = GetCollectionName(),
                IgnoreTriggers = deleteOneOptions?.IgnoreTriggers ?? false,
                Cluster = Cluster
            };

            var response = await Client.DeleteAsync<DeleteOneResponse>(request);
            if (response?.Result == null) return new DatabaseDeleteOneResponse();
            
            return new DatabaseDeleteOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }

        public async Task<DatabaseDeleteManyResponse> DeleteManyAsync(Expression<Func<T, bool>> filter, DatabaseDeleteManyOptions deleteManyOptions = null)
        {
            return await DeleteManyAsync(new ExpressionFilterDefinition<T>(filter), deleteManyOptions);
        }

        public async Task<DatabaseDeleteManyResponse> DeleteManyAsync(FilterDefinition<T> filter, DatabaseDeleteManyOptions deleteManyOptions = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteManyRequest
            {
                Filter = filter.FilterToJson(),
                CollectionName = GetCollectionName(),
                IgnoreTriggers = deleteManyOptions?.IgnoreTriggers ?? false,
                Cluster = Cluster
            };

            var response = await Client.DeleteAsync<DeleteManyResponse>(request);
            if (response?.Result == null)
            {
                return new DatabaseDeleteManyResponse();
            }
            
            return new DatabaseDeleteManyResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }
        
        
        /* Delete */
        public DatabaseDeleteOneResponse DeleteOne(ObjectId id, DatabaseDeleteOneOptions deleteOneOptions = null) 
        {
            return DeleteOne(id.ToString(), deleteOneOptions);
        }
        
        public DatabaseDeleteOneResponse DeleteOne(string id, DatabaseDeleteOneOptions deleteOneOptions = null) 
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            return DeleteOne(new ExpressionFilterDefinition<T>(x => x.Id == id), deleteOneOptions);
        }

        public DatabaseDeleteOneResponse DeleteOne(Expression<Func<T, bool>> filter, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return DeleteOne(new ExpressionFilterDefinition<T>(filter), deleteOneOptions);
        }
        
        public DatabaseDeleteOneResponse DeleteOne(FilterDefinition<T> filter, DatabaseDeleteOneOptions deleteOneOptions = null)
        {
            var request = new DeleteOneRequest
            {
                Filter = filter?.FilterToJson(),
                CollectionName = GetCollectionName(),
                IgnoreTriggers = deleteOneOptions?.IgnoreTriggers ?? false,
                Cluster = Cluster
            };

            var response = Client.Delete<DeleteOneResponse>(request);
            if (response?.Result == null) return new DatabaseDeleteOneResponse();
            
            return new DatabaseDeleteOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }
        public DatabaseDeleteManyResponse DeleteMany(Expression<Func<T, bool>> filter, DatabaseDeleteManyOptions deleteManyOptions = null) 
        {
            return DeleteMany(new ExpressionFilterDefinition<T>(filter), deleteManyOptions);
        }
        
        public DatabaseDeleteManyResponse DeleteMany(FilterDefinition<T> filter, DatabaseDeleteManyOptions deleteManyOptions = null) 
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteManyRequest
            {
                Filter = filter.FilterToJson(),
                CollectionName = GetCollectionName(),
                IgnoreTriggers = deleteManyOptions?.IgnoreTriggers ?? false,
                Cluster = Cluster
            };

            var response = Client.Delete<DeleteManyResponse>(request);
            if (response?.Result == null)
            {
                return new DatabaseDeleteManyResponse();
            }
            
            return new DatabaseDeleteManyResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }
    }
}