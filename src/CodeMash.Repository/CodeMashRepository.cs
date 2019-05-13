using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using CodeMash.Repository;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServiceStack;
using DeleteResult = MongoDB.Driver.DeleteResult;
using ReplaceOneResult = MongoDB.Driver.ReplaceOneResult;
using UpdateResult = MongoDB.Driver.UpdateResult;

namespace CodeMash.Repository
{
    public class CodeMashRepository<T> : IRepository<T> where T : new()
    {
        private static string GetCollectionNameFromInterface()
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            var collectionName = att != null ? ((CollectionName)att).Name : typeof(T).Name;

            return collectionName;
        }

        private static string GetCollectionNameFromType(Type entityType)
        {
            string collectionName = string.Empty;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var customAttribute = Attribute.GetCustomAttribute(entityType, typeof(CollectionName));
            if (customAttribute != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionName = ((CollectionName)customAttribute).Name;
            }
            else
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType))
                {
                    while (entityType != null && entityType.BaseType != typeof(EntityBase))
                    {
                        entityType = entityType.BaseType;
                    }
                }
                if (entityType != null) collectionName = entityType.Name;
            }

            return collectionName;
        }
        
        private static string GetCollectionName()
        {
            var collectionName = typeof(T).BaseType == typeof(object)
                ? GetCollectionNameFromInterface()
                : GetCollectionNameFromType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }
        
        public ICodeMashSettings Settings { get; set; }

        public IJsonServiceClient Client { get; }

        public CodeMashRepository(ICodeMashSettings settings)
        {
            
            Settings = settings ?? throw new ArgumentNullException(nameof(Settings), "CodeMash settings undefined.");
            
            Debug.WriteLine($"Repository Initialization with {Settings.ApiKey}");

            if (string.IsNullOrWhiteSpace(Settings.ApiKey))
            {
                throw new ArgumentNullException(nameof(Settings.ApiKey), "apiKey is not defined");
            }

            string apiAddress = string.IsNullOrEmpty(Settings.ApiUrl) ? "https://api.codemash.io/" : Settings.ApiUrl;

            
            Client = new JsonServiceClient(apiAddress)
            {
                Credentials = new NetworkCredential(Settings.ApiKey, "")
            };
            
        }
        
        public IRepository<T> WithCollection(string collectionName)
        {
            throw new NotImplementedException();
        }

        public T InsertOne(T entity, InsertOneOptions insertOneOptions)
        {
            throw new NotImplementedException();
        }

        public T InsertOne(T entity)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(FilterDefinition<T> filter)
        {
            return Find(filter, null, 0, 1000);
        }

        public List<T> Find(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return Find(new ExpressionFilterDefinition<T>(filter), null, 0, 1000);

        }

        public List<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, int? skip = null,
            int? limit = null, FindOptions findOptions = null)
        {
            
            var projectionAsJson = string.Empty;

            if (projection != null)
            {
                var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
                var projectionInfo = projection.Render(serializer, BsonSerializer.SerializerRegistry);

            
                if (projectionInfo.Document != null)
                {
                    projectionAsJson = BsonExtensionMethods.ToJson(projectionInfo.Document);
                }    
            }
            
            var request = new Find
            {
                CollectionName = GetCollectionName(),
                Filter = filter.ToJson(),
                Projection = projectionAsJson,
                Sort = sort.ToJson(),
                PageSize = limit ?? 100,
                PageNumber = skip ?? 0,
                IncludeSchemaInResponse = false,
                CultureCode = CultureInfo.CurrentCulture.Name,
                ProjectId = Guid.Parse("68ef0ba1-8911-4cb3-ac00-3b1aafa26124")
            };
            
            var response = Client.Post(request);

            if (response == null || !response.Result.Any())
            {
                return new List<TP>();
            }
            
            var list = BsonSerializer.Deserialize<List<TP>>(response.Result);
            return list;
        }

        public List<TP> Find<TP>(Expression<Func<T, bool>> filter, Expression<Func<T, TP>> projection, SortDefinition<T> sort = null, int? skip = null, int? limit = null,
            FindOptions findOptions = null)
        {
            return filter == null 
                ? Find(new BsonDocument(), Builders<T>.Projection.Expression(projection), sort, skip, limit, findOptions) 
                : Find(new ExpressionFilterDefinition<T>(filter), Builders<T>.Projection.Expression(projection), sort, skip, limit, findOptions);
        }

        public List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null,
            FindOptions findOptions = null)
        {
            return filter == null 
                ? Find<T>(new BsonDocument(), null, sort, skip, limit, findOptions) 
                : Find<T>(filter, null, sort, skip, limit, findOptions);
        }

        public List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null)
        {
            return filter == null 
                ? Find<T>(new BsonDocument(), null, sort, skip, limit, findOptions) 
                : Find<T>(new ExpressionFilterDefinition<T>(filter), null, sort, skip, limit, findOptions);
        }

        public T FindOneById(string id)
        {
            throw new NotImplementedException();
        }

        public T FindOneById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public T FindOne(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOne(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndDelete(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            throw new NotImplementedException();
        }

        public T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(string id)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }

        public long Count(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public List<string> Distinct(string field, FilterDefinition<T> filter, DistinctOptions options = null)
        {
            throw new NotImplementedException();
        }

        public List<string> Distinct(string field, Expression<Func<T, bool>> filter, DistinctOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertOneAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity, UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TP>> FindAsync<TP>(FilterDefinition<T> filter, FindOptions<T, TP> findOptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<TP>> FindAsync<TP>(Expression<Func<T, bool>> filter, FindOptions<T, TP> findOptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> DistinctAsync(string field, FilterDefinition<T> filter, DistinctOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> DistinctAsync(string field, Expression<Func<T, bool>> filter, DistinctOptions options)
        {
            throw new NotImplementedException();
        }
    }
}