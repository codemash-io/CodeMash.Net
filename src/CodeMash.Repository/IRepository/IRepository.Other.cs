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
        /* Count Async */
        Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null);
        
        Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null);
        
        
        
        /* Count */
        long Count(FilterDefinition<T> filter, CountOptions countOptions = null);
        
        long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null);
        
        long Count(CountOptions countOptions = null);


        
        /* Distinct */
        List<string> Distinct(string field, FilterDefinition<T> filter, DistinctOptions options = null);

        List<string> Distinct(string field, Expression<Func<T, bool>> filter, DistinctOptions options);
    }
}