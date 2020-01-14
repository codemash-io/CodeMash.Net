using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Update Async */
        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        
        
        /* Update */
        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }
    }
}