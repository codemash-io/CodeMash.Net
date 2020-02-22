using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Driver;

namespace CodeMash.Interfaces.Database.Terms
{
    public partial interface ITermService
    {
        // Meta as T object
        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, ProjectionDefinition<T, T> projection, SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null);

        Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, ProjectionDefinition<T, T> projection, SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null);
        
        // Meta as string
        Task<TermsFindResponse> FindAsync(string taxonomyName, Expression<Func<TermEntity, bool>> filter, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse> FindAsync(string taxonomyName, FilterDefinition<TermEntity> filter, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse> FindAsync(string taxonomyName, Expression<Func<TermEntity, bool>> filter, SortDefinition<TermEntity> sort, TermsFindOptions findOptions = null);
        
        Task<TermsFindResponse> FindAsync(string taxonomyName, FilterDefinition<TermEntity> filter, SortDefinition<TermEntity> sort, TermsFindOptions findOptions = null);
        
        
        
        // Meta as T object
        TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, TermsFindOptions findOptions = null);
        
        TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, TermsFindOptions findOptions = null);
        
        TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null);
        
        TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null);
        
        TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, ProjectionDefinition<T, T> projection, SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null);

        TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, ProjectionDefinition<T, T> projection, SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null);
        
        // Meta as string
        TermsFindResponse Find(string taxonomyName, Expression<Func<TermEntity, bool>> filter, TermsFindOptions findOptions = null);
        
        TermsFindResponse Find(string taxonomyName, FilterDefinition<TermEntity> filter, TermsFindOptions findOptions = null);
        
        TermsFindResponse Find(string taxonomyName, Expression<Func<TermEntity, bool>> filter, SortDefinition<TermEntity> sort, TermsFindOptions findOptions = null);
        
        TermsFindResponse Find(string taxonomyName, FilterDefinition<TermEntity> filter, SortDefinition<TermEntity> sort, TermsFindOptions findOptions = null);
    }
}