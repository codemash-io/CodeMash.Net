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
        Task<long> CountAsync(FilterDefinition<T> filter, DatabaseCountOptions countOptions = null);
        
        Task<long> CountAsync(Expression<Func<T, bool>> filter, DatabaseCountOptions countOptions = null);
        
        Task<long> CountAsync(DatabaseCountOptions countOptions = null);
        
        
        
        /* Count */
        long Count(FilterDefinition<T> filter, DatabaseCountOptions countOptions = null);
        
        long Count(Expression<Func<T, bool>> filter, DatabaseCountOptions countOptions = null);
        
        long Count(DatabaseCountOptions countOptions = null);

        
        
        /* Distinct Async */
        Task<List<object>> DistinctAsync(string field, FilterDefinition<T> filter);

        Task<List<object>> DistinctAsync(string field, Expression<Func<T, bool>> filter);

        Task<List<object>> DistinctAsync(string field);

        
        
        /* Distinct */
        List<object> Distinct(string field, FilterDefinition<T> filter);

        List<object> Distinct(string field, Expression<Func<T, bool>> filter);

        List<object> Distinct(string field);
    }
}