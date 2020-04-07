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
    public partial class CodeMashRepository<T>
    {
        /* Update Async */
        public async Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            return await UpdateOneAsync(x => x.Id == id, update, updateOptions);
        }

        public async Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            return await UpdateOneAsync(x => x.Id == id.ToString(), update, updateOptions);
        }

        public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            DatabaseUpdateOneOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await UpdateOneAsync(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }

        public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            DatabaseUpdateOneOptions updateOptions = null)
        {
            var request = FormUpdateRequest(filter, update, updateOptions);

            var response = await Client.PatchAsync<UpdateOneResponse>(request);
            return FormUpdateResponse(response);
        }

        
        
        /* Update */
        public UpdateResult UpdateOne(string id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            return UpdateOne(x => x.Id == id, update, updateOptions);
        }

        public UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            return UpdateOne(x => x.Id == id.ToString(), update, updateOptions);
        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return UpdateOne(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            var request = FormUpdateRequest(filter, update, updateOptions);

            var response = Client.Patch<UpdateOneResponse>(request);
            return FormUpdateResponse(response);
        }
        
        private UpdateOneRequest FormUpdateRequest(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "Update is not defined");
            }
            
            return new UpdateOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Update = update?.UpdateToJson(),
                BypassDocumentValidation = updateOptions?.BypassDocumentValidation ?? false,
                IsUpsert = updateOptions?.IsUpsert ?? false,
                IgnoreTriggers = updateOptions?.IgnoreTriggers ?? false,
                WaitForFileUpload = updateOptions?.WaitForFileUpload ?? false
            };
        }
        
        private UpdateResult FormUpdateResponse(UpdateOneResponse clientResponse)
        {
            if (clientResponse?.Result == null)
            {
                return new UpdateResult
                {
                    IsAcknowledged = false
                };
            }
            
            return new UpdateResult
            {
                IsAcknowledged = clientResponse.Result.IsAcknowledged,
                MatchedCount = clientResponse.Result.MatchedCount,
                ModifiedCount = clientResponse.Result.ModifiedCount,
                UpsertedId = clientResponse.Result.UpsertedId
            };
        }
    }
}