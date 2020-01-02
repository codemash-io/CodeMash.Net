using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;

namespace CodeMash.Interfaces.Database.Repository
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