using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ServiceStack;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        private static string GetCollectionNameFromInterface()
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionName));
            var collectionName = att != null ? ((CollectionName) att).Name : typeof(T).Name;

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
                collectionName = ((CollectionName) customAttribute).Name;
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

        private ICodeMashSettings Settings { get; set; }

        private IJsonServiceClient Client { get; }

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

        public Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> InsertOneAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertManyAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public T1 InsertOne<T1>(T1 entity, InsertOneOptions insertOneOptions) where T1 : IEntity
        {
            entity.Id = new ObjectId().ToString();

            var request = new InsertOne
            {
                CollectionName = GetCollectionName(),
                Document = entity.ToJson(new JsonWriterSettings {OutputMode = JsonOutputMode.Strict}),
                CultureCode = CultureInfo.CurrentCulture.Name,
                ProjectId = Settings.ProjectId
            };

            var response = Client.Post(request);

            if (response?.Result == null)
            {
                return default;
            }

            var documentAsEntity = BsonSerializer.Deserialize<T1>(response.Result);
            return documentAsEntity;
        }

        public T1 InsertOne<T1>(T1 entity) where T1 : IEntity
        {
            return InsertOne(entity, null);
        }

        public bool InsertMany<T1>(IEnumerable<T1> entities, InsertManyOptions insertManyOptions = null)
            where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public bool InsertMany<T1>(IEnumerable<T1> entities) where T1 : IEntity
        {
            var request = new InsertMany
            {
                CollectionName = GetCollectionName(),
                Documents = entities.Select(x =>
                    x.ToBsonDocument().ToJson(new JsonWriterSettings {OutputMode = JsonOutputMode.Strict}))
            };

            var response = Client.Post<InsertManyResponse>(request);
            return response.Result;
        }

        public Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(string id, UpdateDefinition<T1> update, UpdateOptions updateOptions)
            where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(ObjectId id, UpdateDefinition<T1> update, UpdateOptions updateOptions)
            where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> update,
            UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> update,
            UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> update,
            UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> update,
            UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne<T1>(FilterDefinition<T1> filter, T1 entity,
            UpdateOptions updateOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne<T1>(Expression<Func<T1, bool>> filter, T1 entity,
            UpdateOptions updateOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            throw new NotImplementedException();
        }

        public List<T> Find<T>(FilterDefinition<T> filter) where T : IEntity
        {
            return Find<T, T>(filter, null, null, 0, 1000);
        }

        public List<T> Find<T>(Expression<Func<T, bool>> filter) where T : IEntity
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<T, T>(new ExpressionFilterDefinition<T>(filter), null, null, 0, 1000);

        }

        public List<TP> Find<T, TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection,
            SortDefinition<T> sort = null, int? skip = null,
            int? limit = null, FindOptions findOptions = null) where TP : IEntity
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
                ProjectId = Settings.ProjectId
            };

            var response = Client.Post(request);

            if (response == null || !response.Result.Any())
            {
                return new List<TP>();
            }

            var list = BsonSerializer.Deserialize<List<TP>>(response.Result);
            return list;
        }

        public List<T> Find<T>(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null,
            FindOptions findOptions = null) where T : IEntity
        {

            return filter == null
                ? Find<T, T>(new BsonDocument(), null, sort, skip, limit, findOptions)
                : Find<T, T>(filter, null, sort, skip, limit, findOptions);
        }

        public List<T> Find<T>(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null,
            int? limit = null, FindOptions findOptions = null) where T : IEntity
        {
            return filter == null
                ? Find<T, T>(new BsonDocument(), null, sort, skip, limit, findOptions)
                : Find<T, T>(new ExpressionFilterDefinition<T>(filter), null, sort, skip, limit, findOptions);
        }

        public Task<List<T1>> FindAsync<T1>(FilterDefinition<T1> filter, FindOptions<T1, T1> findOptions)
            where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<List<T1>> FindAsync<T1>(Expression<Func<T1, bool>> filter, FindOptions<T1, T1> findOptions)
            where T1 : IEntity
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

        public T1 FindOneById<T1>(string id) where T1 : IEntity
        {
            return FindOne<T1>(x => x.Id == id);
        }

        public T1 FindOneById<T1>(ObjectId id) where T1 : IEntity
        {
            return FindOne<T1>(x => x.Id == id.ToString());
        }

        public TP FindOne<T1, TP>(FilterDefinition<T1> filter, ProjectionDefinition<T1, TP> projection = null,
            FindOptions findOptions = null) where T1 : IEntity
        {
            if (filter == null)
            {
                throw new ArgumentNullException("Filter cannot be empty");
            }

            var projectionAsJson = string.Empty;

            if (projection != null)
            {
                var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T1>();
                var projectionInfo = projection.Render(serializer, BsonSerializer.SerializerRegistry);

                if (projectionInfo.Document != null)
                {
                    projectionAsJson = BsonExtensionMethods.ToJson(projectionInfo.Document);
                }
            }

            var request = new FindOne
            {
                CollectionName = GetCollectionName(),
                Filter = filter.ToJson(),
                Projection = projectionAsJson,
                CultureCode = CultureInfo.CurrentCulture.Name,
                ProjectId = Settings.ProjectId
            };

            var response = Client.Post(request);

            if (response == null || !response.Result.Any())
            {
                return default(TP);
            }

            var result = BsonSerializer.Deserialize<TP>(response.Result);
            return result;
        }

        public TP FindOne<T1, TP>(Expression<Func<T1, bool>> filter, ProjectionDefinition<T1, TP> projection = null,
            FindOptions findOptions = null) where T1 : IEntity
        {
            return FindOne<T1, TP>(new ExpressionFilterDefinition<T1>(filter), projection, findOptions);
        }

        public T1 FindOne<T1>(FilterDefinition<T1> filter, FindOptions findOptions = null) where T1 : IEntity
        {
            return FindOne<T1, T1>(filter, null, findOptions);
        }

        public T1 FindOne<T1>(Expression<Func<T1, bool>> filter, FindOptions findOptions = null) where T1 : IEntity
        {
            return FindOne<T1, T1>(filter, null, findOptions);
        }

        public Task<T> FindOneByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null,
            FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null,
            FindOptions findOptions = null)
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndReplace<T1>(string id, T1 entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            return FindOneAndReplace(new ExpressionFilterDefinition<T1>(x => x.Id == id), 
                entity, findOneAndReplaceOptions);
        }

        public T1 FindOneAndReplace<T1>(ObjectId id, T1 entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            return FindOneAndReplace(new ExpressionFilterDefinition<T1>(x => x.Id == id.ToString()), 
                entity, findOneAndReplaceOptions);
        }

        public T1 FindOneAndReplace<T1>(FilterDefinition<T1> filter, T1 entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            if (filter == null || entity == null)
            {
                var errorVar = filter == null ? nameof(filter) : nameof(entity);
                throw new ArgumentNullException(errorVar,
                     $"{errorVar} cannot be null");
            }

            var request = new FindOneAndReplace
            {
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name,
                Document = entity.ToJson(),
                Filter = filter.ToJson(),
                FindOneAndReplaceOptions = findOneAndReplaceOptions,
                //TODO: options should be <T1> or <BsonDocument> ??
                //Interface uses T1, serviceContracts uses BsonDocument
                ProjectId = Settings.ProjectId
            };

            var response = Client.Put(request);

            if (response.Result.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Replace failed");
            }

            return BsonSerializer.Deserialize<T1>(response.Result);
        }

        public T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity,
            FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions) where T1 : IEntity
        {
            return FindOneAndReplace(new ExpressionFilterDefinition<T1>(filter), entity, findOneAndReplaceOptions);
        }

        public T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity) where T1 : IEntity
        {
            return FindOneAndReplace(new ExpressionFilterDefinition<T1>(filter), entity, null);
        }

        public Task<T> FindOneAndReplaceAsync(string id, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(ObjectId id, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity,
            FindOneAndReplaceOptions<T> findOneAndReplaceOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity)
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndDelete<T1>(string id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null)
            where T1 : IEntity
        {
            return FindOneAndDelete(new ExpressionFilterDefinition<T1>(x => x.Id == id), findOneAndDeleteOptions);
        }

        public T1 FindOneAndDelete<T1>(ObjectId id, FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null)
            where T1 : IEntity
        {
            return FindOneAndDelete(new ExpressionFilterDefinition<T1>(x => x.Id == id.ToString()), findOneAndDeleteOptions);
        }

        public T1 FindOneAndDelete<T1>(FilterDefinition<T1> filter,
            FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions = null) where T1 : IEntity
        {
            var request = new FindOneAndDelete
            {
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name,
                Filter = filter.ToJson(),
                FindOneAndDeleteOptions = findOneAndDeleteOptions,
                ProjectId = Settings.ProjectId
                //TODO: options should be <T1> or <BsonDocument> ??
                //Interface uses T1, serviceContracts uses BsonDocument
            };

            var response = Client.Delete(request);

            if (response.Result == null)
            {
                throw new InvalidOperationException("delete failed");
            }

            return BsonSerializer.Deserialize<T1>(response.Result);
        }

        public T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter,
            FindOneAndDeleteOptions<BsonDocument> findOneAndDeleteOptions) where T1 : IEntity
        {
            return FindOneAndDelete(new ExpressionFilterDefinition<T1>(filter), findOneAndDeleteOptions);
        }

        public T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
        {
            return FindOneAndDelete(new ExpressionFilterDefinition<T1>(filter), null);
        }

        public Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter,
            FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter,
            FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndUpdate<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> entity,
            FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null) where T1 : IEntity
        {
            var request = new FindOneAndUpdate{
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name,
                Filter = filter.ToJson(),
                ProjectId = Settings.ProjectId,
                FindOneAndUpdateOptions = findOneAndUpdateOptions,
                Document = entity.ToBsonDocument()
                //TODO: options should be <T1> or <BsonDocument> ??
                //Interface uses T1, serviceContracts uses BsonDocument
            };

            var response = Client.Patch(request);

            if (response.Result.IsNullOrEmpty()){
                throw new InvalidOperationException("Entity cannot be updated");
            }

            return BsonSerializer.Deserialize<T1>(response.Result);
        }

        public T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity,
            FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions) where T1 : IEntity
        {
            return FindOneAndUpdate(new ExpressionFilterDefinition<T1>(filter), entity, findOneAndUpdateOptions);
        }

        public T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity)
            where T1 : IEntity
        {
            return FindOneAndUpdate(new ExpressionFilterDefinition<T1>(filter), entity);
        }

        public T1 FindOneAndUpdate<T1>(string id, UpdateDefinition<T1> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null)
            where T1 : IEntity
        {
            return FindOneAndUpdate(new ExpressionFilterDefinition<T1>(x => x.Id == id), entity, findOneAndUpdateOptions);
        }

        public T1 FindOneAndUpdate<T1>(ObjectId id, UpdateDefinition<T1> entity, FindOneAndUpdateOptions<BsonDocument> findOneAndUpdateOptions = null)
            where T1 : IEntity
        {
            return FindOneAndUpdate(new ExpressionFilterDefinition<T1>(x => x.Id == id.ToString()), entity, findOneAndUpdateOptions);
        }

        public Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne<T1>(string id) where T1 : IEntity
        {
            if (id.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(id), "id cannot be empty");
            }
            
            var request = new DeleteOne
            {
                ProjectId = Settings.ProjectId,
                Filter = new ExpressionFilterDefinition<T1>(x => x.Id == id).ToJson(),
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Client.Delete(request);

            return response.Result;
        }

        public DeleteResult DeleteOne<T1>(ObjectId id) where T1 : IEntity
        {
            return DeleteOne<T1>(id.ToString());
        }

        /*public DeleteResult DeleteOne<T1>(FilterDefinition<T1> filter) where T1 : IEntity
        {
            
        }

        public DeleteResult DeleteOne<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
        {
            if(filter == Expression.
            return DeleteOne(new ExpressionFilterDefinition<T1>(filter));
        }*/

        public DeleteResult DeleteMany<T1>(FilterDefinition<T1> filter) where T1 : IEntity
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter), "Filter cannot be null");
            }

            var request = new DeleteMany
            {
                ProjectId = Settings.ProjectId,
                Filter = filter.ToJson(),
                CollectionName = GetCollectionName(),
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Client.Delete(request);

            return response.Result;
        }

        public DeleteResult DeleteMany<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
        {
            return DeleteMany(new ExpressionFilterDefinition<T1>(filter));
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

        public List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation,
            AggregateOptions aggregateOptions)
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

        public Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
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
    }
}