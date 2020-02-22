using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Insert Async */
        Task<T> InsertOneAsync(T entity, DatabaseInsertOneOptions insertOneOptions = null);
        
        Task<List<string>> InsertManyAsync(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null);
        
        
        /* Insert */
        T InsertOne(T entity, DatabaseInsertOneOptions insertOneOptions = null);
        
        List<string> InsertMany(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null);
    }
}