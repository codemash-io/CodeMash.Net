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
    public partial class CodeMashRepository<T> where T : IEntity
    { 
        /* Update Async */
        public async Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            DatabaseUpdateManyOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await UpdateManyAsync(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }
        
        public async Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            DatabaseUpdateManyOptions updateOptions = null)
        {
            var request = FormUpdateManyRequest(filter, update, updateOptions);

            var response = await Client.PatchAsync<UpdateManyResponse>(request);
            return FormUpdateManyResponse(response);
        }
        
        
        /* Update */
        public UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return UpdateMany(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }
        
        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null)
        {
            var request = FormUpdateManyRequest(filter, update, updateOptions);

            var response = Client.Patch<UpdateManyResponse>(request);
            return FormUpdateManyResponse(response);
        }

        
        private UpdateManyRequest FormUpdateManyRequest(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "Update is not defined");
            }
            
            return new UpdateManyRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Update = update?.UpdateToJson(),
                BypassDocumentValidation = updateOptions?.BypassDocumentValidation ?? false,
                IgnoreTriggers = updateOptions?.IgnoreTriggers ?? false,
                Cluster = Cluster
            };
        }
        
        private UpdateResult FormUpdateManyResponse(UpdateManyResponse clientResponse)
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