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
        Task<T> FindOneByIdAsync(string id, DatabaseFindOneOptions findOneOptions = null);
        
        Task<T> FindOneByIdAsync(ObjectId id, DatabaseFindOneOptions findOneOptions = null);
        
        Task<T> FindOneAsync(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null);
        
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null);
        
        Task<TP> FindOneAsync<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);

        Task<TP> FindOneAsync<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);
        
        
        
        /* Find One */
        T FindOneById(string id, DatabaseFindOneOptions findOneOptions = null);

        T FindOneById(ObjectId id, DatabaseFindOneOptions findOneOptions = null);
        
        T FindOne(FilterDefinition<T> filter, DatabaseFindOneOptions findOneOptions = null);
        
        T FindOne(Expression<Func<T, bool>> filter, DatabaseFindOneOptions findOneOptions = null);
        
        TP FindOne<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);

        TP FindOne<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, DatabaseFindOneOptions findOneOptions = null);
    }
}