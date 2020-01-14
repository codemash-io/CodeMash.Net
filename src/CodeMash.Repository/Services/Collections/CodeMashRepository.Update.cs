﻿using System;
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
            var request = FormRequest(filter, update, updateOptions);

            var response = await Client.PatchAsync<UpdateOneResponse>(request);
            return FormResponse(response);
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
            var request = FormRequest(filter, update, updateOptions);

            var response = Client.Patch<UpdateOneResponse>(request);
            return FormResponse(response);
        }
        
        private UpdateOneRequest FormRequest(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null)
        {
            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "Update is not defined");
            }
            
            return new UpdateOneRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Update = update.ToString(),
                BypassDocumentValidation = updateOptions?.BypassDocumentValidation ?? false,
            };
        }
        
        private UpdateResult FormResponse(UpdateOneResponse clientResponse)
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
                IsModifiedCountAvailable = clientResponse.Result.IsModifiedCountAvailable,
                MatchedCount = clientResponse.Result.MatchedCount,
                ModifiedCount = clientResponse.Result.ModifiedCount,
                UpsertedId = clientResponse.Result.UpsertedId
            };
        }
    }
}