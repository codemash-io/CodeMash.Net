using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;
using UpdateResult = CodeMash.ServiceContracts.Api.UpdateResult;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Update Async */
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null);
        
        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null);

        
        
        /* Update */
        UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null);
        
        UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, DatabaseUpdateManyOptions updateOptions = null);
    }
}