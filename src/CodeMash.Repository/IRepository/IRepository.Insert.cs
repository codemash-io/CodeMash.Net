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
        /* Insert Async */
        Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions);
        
        Task<T> InsertOneAsync(T entity);
        
        Task<bool> InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions);
        
        Task<bool> InsertManyAsync(IEnumerable<T> entities);
        
        
        
        /* Insert */
        T InsertOne(T entity, InsertOneOptions insertOneOptions);
        
        T InsertOne(T entity);
        
        bool InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions = null);
    }
}