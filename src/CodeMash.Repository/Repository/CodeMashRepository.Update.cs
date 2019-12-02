using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServiceStack;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Update Async */
        public Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        
        
        /* Update */
        public UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update) 
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        
        
        /* Replace Async */
        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Replace */
        public ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Find One and Update Async */
        public Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Find One and Update */
        public T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            throw new NotImplementedException();
        }

        
        
        /* Find One and Replace Async */
        public Task<T> FindOneAndReplaceAsync(string id, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(ObjectId id, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Find One and Replace */
        public T FindOneAndReplace(string id, T entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(ObjectId id, T entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(FilterDefinition<T> filter, T entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity)
        {
            throw new NotImplementedException();
        }
    }
}