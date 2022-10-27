using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = CodeMash.ServiceContracts.Api.UpdateResult;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Update Async */
        Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);

        
        
        /* Update */
        UpdateResult UpdateOne(string id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
        
        UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateOneOptions updateOptions = null);
    }
}