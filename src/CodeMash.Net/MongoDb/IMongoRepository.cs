using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CodeMash.Net
{
    
    public interface IMongoRepository<T> : IQueryable<T>
    {

        IMongoRepository<T> WithCollection(string collectionName);
            
        T InsertOne(T entity, InsertOneOptions insertOneOptions);
        T InsertOne(T entity);

        void InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions);
        void InsertMany(IEnumerable<T> entities);

        // update
        UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        //replace 
        ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null);
        ReplaceOneResult ReplaceOne(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null);


        // Find
        List<T> Find(FilterDefinition<T> filter);
        List<T> Find(Expression<Func<T, bool>> filter);
        List<T> Find(FilterDefinition<T> filter, /*ProjectionDefinition<T> projection = null,*/ SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null);
        List<T> Find(Expression<Func<T, bool>> filter, /*ProjectionDefinition<T> projection = null, */SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null);

        //Find One
        T FindOneById(string id);
        T FindOneById(ObjectId id);
        T FindOne(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);
        T FindOne(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);

        // Find One and Replace
        T FindOneAndReplace(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        T FindOneAndReplace(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        T FindOneAndReplace(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions);
        T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity);


        // Find One and Delete
        T FindOneAndDelete(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        T FindOneAndDelete(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        T FindOneAndDelete(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions);
        T FindOneAndDelete(Expression<Func<T, bool>> filter);

        // Find One and Update
        T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null);
        T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions);
        T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity);


        // Delete
        DeleteResult DeleteOne(string id);
        DeleteResult DeleteOne(ObjectId id);
        DeleteResult DeleteOne(FilterDefinition<T> filter);
        DeleteResult DeleteOne(Expression<Func<T, bool>> filter);
        DeleteResult DeleteMany(FilterDefinition<T> filter);
        DeleteResult DeleteMany(Expression<Func<T, bool>> filter);

        // Aggregate
        List<T> Aggregate(PipelineDefinition<T, T> aggregation, AggregateOptions aggregateOptions);


        // Count
        long Count(FilterDefinition<T> filter, CountOptions countOptions = null);
        long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null);


        // Distinct
        List<string> Distinct(string field, FilterDefinition<T> filter, DistinctOptions options = null);

        List<string> Distinct(string field, Expression<Func<T, bool>> filter, DistinctOptions options);



        // Insert
        Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions);
        Task<T> InsertOneAsync(T entity);

        Task InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions);
        Task InsertManyAsync(IEnumerable<T> entities);

        // update
        Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        //replace 
        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null);
        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null);


        // Find
        Task<List<T>> FindAsync(FilterDefinition<T> filter);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> FindAsync(FilterDefinition<T> filter, /*ProjectionDefinition<T> projection = null,*/ SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, /*ProjectionDefinition<T> projection = null, */SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null);

        //Find One
        Task<T> FindOneByIdAsync(string id);
        Task<T> FindOneByIdAsync(ObjectId id);
        Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);

        // Find One and Replace
        Task<T> FindOneAndReplaceAsync(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions);
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity);


        // Find One and Delete
        Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions);
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter);

        // Find One and Update
        Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null);
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions);
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity);


        // Delete
        Task<DeleteResult> DeleteOneAsync(string id);
        Task<DeleteResult> DeleteOneAsync(ObjectId id);
        Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter);
        Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter);
        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter);

        // Aggregate
        Task<List<T>> AggregateAsync(PipelineDefinition<T, T> aggregation, AggregateOptions aggregateOptions);


        // Count
        Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null);
        Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null);


        // Distinct
        Task<List<string>> DistinctAsync(string field, FilterDefinition<T> filter, DistinctOptions options = null);

        Task<List<string>> DistinctAsync(string field, Expression<Func<T, bool>> filter, DistinctOptions options);



    }
}
