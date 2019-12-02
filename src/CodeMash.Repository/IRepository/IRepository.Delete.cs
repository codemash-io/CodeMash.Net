using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;

namespace CodeMash.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Delete Async */
        Task<DeleteResult> DeleteOneAsync(string id);
        
        Task<DeleteResult> DeleteOneAsync(ObjectId id);
        
        Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter);
        
        Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter);
        
        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter);
        
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter);
        
        
        
        /* Delete */
        DeleteResult DeleteOne(string id);
        
        DeleteResult DeleteOne(ObjectId id);
        
        DeleteResult DeleteMany(FilterDefinition<T> filter);
        
        DeleteResult DeleteMany(Expression<Func<T, bool>> filter);

        
        
        /* Find One and Delete Async */
        Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        
        Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        
        Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions);
        
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter);
        
        
        
        /* Find One and Delete */
        T FindOneAndDelete(string id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null);
        
        T FindOneAndDelete(ObjectId id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null);
        
        T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null);
        
        T FindOneAndDelete(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions);
        
        T FindOneAndDelete(Expression<Func<T, bool>> filter);
    }
}