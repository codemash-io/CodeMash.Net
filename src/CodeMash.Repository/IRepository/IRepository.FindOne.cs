using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Find One Async */
        Task<T> FindOneByIdAsync(string id);
        
        Task<T> FindOneByIdAsync(ObjectId id);
        
        Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);
        
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);
        
        
        
        /* Find One */
        T FindOneById(string id);

        T FindOneById(ObjectId id);
        
        TP FindOne<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, FindOptions findOptions = null);

        TP FindOne<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, FindOptions findOptions = null);

        T FindOne(FilterDefinition<T> filter, FindOptions findOptions = null);
        
        T FindOne(Expression<Func<T, bool>> filter, FindOptions findOptions = null);
    }
}