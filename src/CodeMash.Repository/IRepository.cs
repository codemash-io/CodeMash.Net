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
    public interface IRepository<T> where T : IEntity
    {
        IRepository<T> WithCollection(string collectionName);

        // Insert Async
        Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions);
        Task<T> InsertOneAsync(T entity);
        Task<bool> InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions);
        Task<bool> InsertManyAsync(IEnumerable<T> entities);

        // Insert
        T InsertOne<T>(T entity, InsertOneOptions insertOneOptions) where T : IEntity;
        T InsertOne<T>(T entity) where T : IEntity;
        bool InsertMany<T>(IEnumerable<T> entities, InsertManyOptions insertManyOptions = null) where T : IEntity;
        bool InsertMany<T>(IEnumerable<T> entities) where T : IEntity;

        // Upadte Async
        Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);
        Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions);

        // Update
        UpdateResult UpdateOne<T>(string id, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;
        UpdateResult UpdateOne<T>(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;
        UpdateResult UpdateOne<T>(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;
        UpdateResult UpdateOne<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;
        UpdateResult UpdateMany<T>(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;
        UpdateResult UpdateMany<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity;

        // Replace 
        ReplaceOneResult ReplaceOne<T>(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null) where T : IEntity;
        ReplaceOneResult ReplaceOne<T>(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null) where T : IEntity;

        // Replace Async 
        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null);
        Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null);

        // Find
        List<T> Find<T>(FilterDefinition<T> filter) where T : IEntity;
        List<T> Find<T>(Expression<Func<T, bool>> filter) where T : IEntity;
        List<TP> Find<T, TP>(FilterDefinition<T> filter, ProjectionDefinition<T,TP> projection, SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null) where TP : IEntity;
        
        List<T> Find<T>(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null) where T : IEntity;
        List<T> Find<T>(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null) where T : IEntity;

        // Find Async
        Task<List<T>> FindAsync<T>(FilterDefinition<T> filter, FindOptions<T, T> findOptions) where T : IEntity;
        Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> filter, FindOptions<T, T> findOptions) where T : IEntity;
        Task<List<T>> FindAsync(FilterDefinition<T> filter);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter);

        // Find One
        T FindOneById<T>(string id) where T : IEntity;
        T FindOneById<T>(ObjectId id) where T : IEntity;
        TP FindOne<T, TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, FindOptions findOptions = null) where T : IEntity;
        TP FindOne<T, TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null, FindOptions findOptions = null) where T : IEntity;
        T1 FindOne<T1>(FilterDefinition<T1> filter, FindOptions findOptions = null) where T1 : IEntity;
        T1 FindOne<T1>(Expression<Func<T1, bool>> filter, FindOptions findOptions = null) where T1 : IEntity;

        // Find One Async
        Task<T> FindOneByIdAsync(string id);
        Task<T> FindOneByIdAsync(ObjectId id);
        Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null);

        // Find One and Replace
        T1 FindOneAndReplace<T1>(string id, T1 entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity;
        T1 FindOneAndReplace<T1>(ObjectId id, T1 entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity;
        T1 FindOneAndReplace<T1>(FilterDefinition<T1> filter, T1 entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity;
        T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions) where T1 : IEntity;
        T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity) where T1 : IEntity;

        // Find One and Replace Async
        Task<T> FindOneAndReplaceAsync(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null);
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions);
        Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity);

        // Find One and Delete
        T1 FindOneAndDelete<T1>(string id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null) where T1 : IEntity;
        T1 FindOneAndDelete<T1>(ObjectId id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null) where T1 : IEntity;
        T1 FindOneAndDelete<T1>(FilterDefinition<T1> filter, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null) where T1 : IEntity;
        T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions) where T1 : IEntity;
        T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity;

        // Find One and Delete Async
        Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null);
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions);
        Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter);

        // Find One and Update
        T1 FindOneAndUpdate<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null) where T1 : IEntity;
        T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions) where T1 : IEntity;
        T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity) where T1 : IEntity;

        // Find One and Update Async
        Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null);
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions);
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity);


        // Delete
        DeleteResult DeleteOne<T>(string id) where T : IEntity;
        DeleteResult DeleteOne<T>(ObjectId id) where T : IEntity;
        //DeleteResult DeleteOne<T>(FilterDefinition<T> filter) where T : IEntity;
        //DeleteResult DeleteOne<T>(Expression<Func<T, bool>> filter) where T : IEntity;
        DeleteResult DeleteMany<T>(FilterDefinition<T> filter) where T : IEntity;
        DeleteResult DeleteMany<T>(Expression<Func<T, bool>> filter) where T : IEntity;

        // Delete Async
        Task<DeleteResult> DeleteOneAsync(string id);
        Task<DeleteResult> DeleteOneAsync(ObjectId id);
        Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter);
        Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter);
        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter);
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter);

        // Aggregate
        List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions);
        Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions);
        
        // Count
        long Count(FilterDefinition<T> filter, CountOptions countOptions = null);
        long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null);

        // Count Async
        Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null);
        Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null);

        // Distinct
        List<string> Distinct(string field, FilterDefinition<T> filter, DistinctOptions options = null);

        List<string> Distinct(string field, Expression<Func<T, bool>> filter, DistinctOptions options);
    }
}