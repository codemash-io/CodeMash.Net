using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Update Async */
        Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        
        
        /* Update */
        UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        
        
        /* Replace Async */ 
        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null);
        
        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null);
        
        
        
        /* Replace */ 
        ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null);
        
        ReplaceOneResult ReplaceOne(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null);
        
        
        
        /* Find One and Update Async */
        Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null);
        
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions);
        
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity);
        
        
        
        /* Find One and Update */
        T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null);
        
        T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions);
        
        T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity);

        
        
        /* Find One and Replace Async */
        Task<T> FindOneAndReplaceAsync(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        
        Task<T> FindOneAndReplaceAsync(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        
        Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions);
        
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity);
        
        
        
        /* Find One and Replace */
        T FindOneAndReplace(string id, T entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null);
        
        T FindOneAndReplace(ObjectId id, T entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null);
        
        T FindOneAndReplace(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null);
        
        T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions);
        
        T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity);
    }
}