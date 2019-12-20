using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Insert Async */
        Task<DatabaseInsertOneResponse<T>> InsertOneAsync(T entity, DatabaseInsertOneOptions insertOneOptions = null);
        
        Task<DatabaseInsertManyResponse> InsertManyAsync(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null);
        
        
        /* Insert */
        DatabaseInsertOneResponse<T> InsertOne(T entity, DatabaseInsertOneOptions insertOneOptions = null);
        
        DatabaseInsertManyResponse InsertMany(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null);
    }
}