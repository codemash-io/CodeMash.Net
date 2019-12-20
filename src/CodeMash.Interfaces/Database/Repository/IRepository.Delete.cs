using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Delete Async */
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(string id);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(ObjectId id);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(FilterDefinition<T> filter);
        
        Task<DatabaseDeleteOneResponse> DeleteOneAsync(Expression<Func<T, bool>> filter);
        
        Task<DatabaseDeleteManyResponse> DeleteManyAsync(FilterDefinition<T> filter);
        
        Task<DatabaseDeleteManyResponse> DeleteManyAsync(Expression<Func<T, bool>> filter);
        
        
        
        /* Delete */
        DatabaseDeleteOneResponse DeleteOne(string id);
        
        DatabaseDeleteOneResponse DeleteOne(ObjectId id);
        
        DatabaseDeleteOneResponse DeleteOne(FilterDefinition<T> filter);
        
        DatabaseDeleteOneResponse DeleteOne(Expression<Func<T, bool>> filter);
        
        DatabaseDeleteManyResponse DeleteMany(FilterDefinition<T> filter);
        
        DatabaseDeleteManyResponse DeleteMany(Expression<Func<T, bool>> filter);
    }
}