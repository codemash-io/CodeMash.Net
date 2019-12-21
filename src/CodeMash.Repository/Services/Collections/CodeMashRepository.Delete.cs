using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Delete Async */
        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(ObjectId id)
        {
            return await DeleteOneAsync(id.ToString());
        }
        
        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(string id)
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            return await DeleteOneAsync(new ExpressionFilterDefinition<T>(x => x.Id == id));
        }

        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await DeleteOneAsync(new ExpressionFilterDefinition<T>(filter));
        }

        public async Task<DatabaseDeleteOneResponse> DeleteOneAsync(FilterDefinition<T> filter)
        {
            var request = new DeleteOneRequest
            {
                Filter = filter?.FilterToJson(),
                CollectionName = GetCollectionName(),
            };

            var response = await Client.DeleteAsync<DeleteOneResponse>(request);
            if (response?.Result == null) return new DatabaseDeleteOneResponse();
            
            return new DatabaseDeleteOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }

        public async Task<DatabaseDeleteManyResponse> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            return await DeleteManyAsync(new ExpressionFilterDefinition<T>(filter));
        }

        public async Task<DatabaseDeleteManyResponse> DeleteManyAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteManyRequest
            {
                Filter = filter.FilterToJson(),
                CollectionName = GetCollectionName(),
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
        public DatabaseDeleteOneResponse DeleteOne(ObjectId id) 
        {
            return DeleteOne(id.ToString());
        }
        
        public DatabaseDeleteOneResponse DeleteOne(string id) 
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            return DeleteOne(new ExpressionFilterDefinition<T>(x => x.Id == id));
        }

        public DatabaseDeleteOneResponse DeleteOne(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return DeleteOne(new ExpressionFilterDefinition<T>(filter));
        }
        
        public DatabaseDeleteOneResponse DeleteOne(FilterDefinition<T> filter)
        {
            var request = new DeleteOneRequest
            {
                Filter = filter?.FilterToJson(),
                CollectionName = GetCollectionName(),
            };

            var response = Client.Delete<DeleteOneResponse>(request);
            if (response?.Result == null) return new DatabaseDeleteOneResponse();
            
            return new DatabaseDeleteOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                DeletedCount = response.Result.DeletedCount
            };
        }
        public DatabaseDeleteManyResponse DeleteMany(Expression<Func<T, bool>> filter) 
        {
            return DeleteMany(new ExpressionFilterDefinition<T>(filter));
        }
        
        public DatabaseDeleteManyResponse DeleteMany(FilterDefinition<T> filter) 
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteManyRequest
            {
                Filter = filter.FilterToJson(),
                CollectionName = GetCollectionName(),
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