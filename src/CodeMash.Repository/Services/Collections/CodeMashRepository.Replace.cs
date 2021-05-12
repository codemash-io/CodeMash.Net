using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts.Api;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        /* Replace Async */
        public async Task<DatabaseReplaceOneResponse> ReplaceOneAsync(string id, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            return await ReplaceOneAsync(x => x.Id == id, entity, updateOptions);
        }
        
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
                IgnoreTriggers = updateOptions?.IgnoreTriggers ?? false,
                WaitForFileUpload = updateOptions?.WaitForFileUpload ?? false,
                Cluster = Cluster
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
        public DatabaseReplaceOneResponse ReplaceOne(string id, T entity, DatabaseReplaceOneOptions updateOptions = null)
        {
            return ReplaceOne(x => x.Id == id, entity, updateOptions);
        }
        
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
                IgnoreTriggers = updateOptions?.IgnoreTriggers ?? false,
                WaitForFileUpload = updateOptions?.WaitForFileUpload ?? false,
                Cluster = Cluster
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