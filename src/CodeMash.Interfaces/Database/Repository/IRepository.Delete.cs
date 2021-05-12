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
        /* Delete Async */
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(string id, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(ObjectId id, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(FilterDefinition<T> filter, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(Expression<Func<T, bool>> filter, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        Task<DatabaseDeleteManyResponse> DeleteManyAsync(FilterDefinition<T> filter, DatabaseDeleteManyOptions deleteManyOptions = null);
        
        Task<DatabaseDeleteManyResponse> DeleteManyAsync(Expression<Func<T, bool>> filter, DatabaseDeleteManyOptions deleteManyOptions = null);
        
        
        
        /* Delete */
        DatabaseDeleteOneResponse DeleteOne(string id, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        DatabaseDeleteOneResponse DeleteOne(ObjectId id, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        DatabaseDeleteOneResponse DeleteOne(FilterDefinition<T> filter, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        DatabaseDeleteOneResponse DeleteOne(Expression<Func<T, bool>> filter, DatabaseDeleteOneOptions deleteOneOptions = null);
        
        DatabaseDeleteManyResponse DeleteMany(FilterDefinition<T> filter, DatabaseDeleteManyOptions deleteManyOptions = null);
        
        DatabaseDeleteManyResponse DeleteMany(Expression<Func<T, bool>> filter, DatabaseDeleteManyOptions deleteManyOptions = null);
    }
}