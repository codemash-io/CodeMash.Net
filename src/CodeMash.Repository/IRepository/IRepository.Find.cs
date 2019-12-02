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
        /* Find Async */
        Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T> findOptions);
        
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> findOptions);
        
        Task<List<T>> FindAsync(FilterDefinition<T> filter);
        
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);
        
        
        
        /* Find */
        List<T> Find(FilterDefinition<T> filter);
        
        List<T> Find(Expression<Func<T, bool>> filter);
        
        List<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null) where TP : IEntity;
        
        List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null);
        
        List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null);
    }
}