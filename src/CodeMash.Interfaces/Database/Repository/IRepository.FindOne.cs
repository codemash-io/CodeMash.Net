using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Find One Async */
        Task<DatabaseFindOneResponse<T>> FindOneByIdAsync(string id, DatabaseFindOneOptions findOneOptions = null);
        
        Task<DatabaseFindOneResponse<T>> FindOneByIdAsync(ObjectId id, DatabaseFindOneOptions findOneOptions = null);
        
        Task<DatabaseFindOneResponse<T>> FindOneAsync(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null);
        
        Task<DatabaseFindOneResponse<T>> FindOneAsync(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null);
        
        Task<DatabaseFindOneResponse<TP>> FindOneAsync<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);

        Task<DatabaseFindOneResponse<TP>> FindOneAsync<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);
        
        
        
        /* Find One */
        DatabaseFindOneResponse<T> FindOneById(string id, DatabaseFindOneOptions findOneOptions = null);

        DatabaseFindOneResponse<T> FindOneById(ObjectId id, DatabaseFindOneOptions findOneOptions = null);
        
        DatabaseFindOneResponse<T> FindOne(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null);
        
        DatabaseFindOneResponse<T> FindOne(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null);
        
        DatabaseFindOneResponse<TP> FindOne<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);

        DatabaseFindOneResponse<TP> FindOne<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);
    }
}