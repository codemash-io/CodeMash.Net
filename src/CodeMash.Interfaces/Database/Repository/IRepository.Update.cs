using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Interfaces.Database.Repository
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
        Task<DatabaseReplaceOneResponse> ReplaceOneAsync(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        Task<DatabaseReplaceOneResponse> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        
        
        /* Replace */ 
        DatabaseReplaceOneResponse ReplaceOne(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        DatabaseReplaceOneResponse ReplaceOne(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
    }
}