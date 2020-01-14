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
        /* Replace Async */ 
        Task<DatabaseReplaceOneResponse> ReplaceOneAsync(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        Task<DatabaseReplaceOneResponse> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        
        
        /* Replace */ 
        DatabaseReplaceOneResponse ReplaceOne(FilterDefinition<T> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
        
        DatabaseReplaceOneResponse ReplaceOne(Expression<Func<T, bool>> filter, T entity, DatabaseReplaceOneOptions updateOptions = null);
    }
}