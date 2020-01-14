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
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        
        
        /* Update */
        UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        
        UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
    }
}