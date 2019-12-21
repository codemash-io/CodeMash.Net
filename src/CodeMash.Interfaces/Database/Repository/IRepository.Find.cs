using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using MongoDB.Driver;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Find Async */
        Task<DatabaseFindResponse<T>> FindAsync(Expression<Func<T, bool>> filter, DatabaseFindOptions findOptions = null);
        
        Task<DatabaseFindResponse<T>> FindAsync(FilterDefinition<T> filter, DatabaseFindOptions findOptions = null);
        
        Task<DatabaseFindResponse<T>> FindAsync(Expression<Func<T, bool>> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null);
        
        Task<DatabaseFindResponse<T>> FindAsync(FilterDefinition<T> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null);
        
        Task<DatabaseFindResponse<TP>> FindAsync<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null);
        
        Task<DatabaseFindResponse<TP>> FindAsync<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null);
        
        
        
        /* Find */
        DatabaseFindResponse<T> Find(Expression<Func<T, bool>> filter, DatabaseFindOptions findOptions = null);
        
        DatabaseFindResponse<T> Find(FilterDefinition<T> filter, DatabaseFindOptions findOptions = null);
        
        DatabaseFindResponse<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null);
        
        DatabaseFindResponse<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, DatabaseFindOptions findOptions = null);
        
        DatabaseFindResponse<TP> Find<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null);
        
        DatabaseFindResponse<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, DatabaseFindOptions findOptions = null);
    }
}