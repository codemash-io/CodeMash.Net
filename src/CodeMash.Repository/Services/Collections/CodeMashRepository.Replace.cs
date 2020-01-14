using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Replace Async */
        public async Task<DatabaseReplaceOneResponse> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await ReplaceOneAsync(new ExpressionFilterDefinition<T>(filter), entity, updateOptions);
        }
        
        public async Task<DatabaseReplaceOneResponse> ReplaceOneAsync(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity is not defined");
            }
            
            var request = new ReplaceOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Document = JsonConverterHelper.SerializeEntity(entity),
                BypassDocumentValidation = updateOptions?.BypassDocumentValidation ?? false,
                IsUpsert = updateOptions?.IsUpsert ?? false,
            };

            var response = await Client.PutAsync<ReplaceOneResponse>(request);
            if (response?.Result == null)
            {
                return new DatabaseReplaceOneResponse
                {
                    IsAcknowledged = false
                };
            }
            
            return new DatabaseReplaceOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                IsModifiedCountAvailable = response.Result.IsModifiedCountAvailable,
                MatchedCount = response.Result.MatchedCount,
                ModifiedCount = response.Result.ModifiedCount,
                UpsertedId = response.Result.UpsertedId
            };
        }
        
        
        /* Replace */
        public DatabaseReplaceOneResponse ReplaceOne(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return ReplaceOne(new ExpressionFilterDefinition<T>(filter), entity, updateOptions);
        }
        
        public DatabaseReplaceOneResponse ReplaceOne(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity is not defined");
            }
            
            var request = new ReplaceOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Document = JsonConverterHelper.SerializeEntity(entity),
                BypassDocumentValidation = updateOptions?.BypassDocumentValidation ?? false,
                IsUpsert = updateOptions?.IsUpsert ?? false,
            };

            var response = Client.Put<ReplaceOneResponse>(request);
            if (response?.Result == null)
            {
                return new DatabaseReplaceOneResponse
                {
                    IsAcknowledged = false
                };
            }
            
            return new DatabaseReplaceOneResponse
            {
                IsAcknowledged = response.Result.IsAcknowledged,
                IsModifiedCountAvailable = response.Result.IsModifiedCountAvailable,
                MatchedCount = response.Result.MatchedCount,
                ModifiedCount = response.Result.ModifiedCount,
                UpsertedId = response.Result.UpsertedId
            };
        }
    }
}