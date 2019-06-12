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
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;

namespace CodeMash.Repository
{
    public class CodeMashRepository<T> : IRepository<T> where T : IEntity
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
        
        /*
        
        
        
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
            var request = new InsertOne
            {
                CollectionName = GetCollectionName(),
                Document = entity.ToJson(new JsonWriterSettings{ OutputMode = JsonOutputMode.Strict }),
                CultureCode = CultureInfo.CurrentCulture.Name,
                
            };

            var response = Client.Post(request);

            if(response?.Result == null){
                return default;
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }

        public void InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            throw new NotImplementedException();
        }

        public void InsertMany(IEnumerable<T> entities)
        {
            var request = new InsertMany{
                CollectionName = GetCollectionName(),
                Documents = entities.Select(x => x.ToBsonDocument().ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }))
            };

            Client.Post<InsertManyResponse>(request);
        }

        public UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions) where T : IEntity
        {
            throw new NotImplementedException();
            //return UpdateOne(new ExpressionFilterDefinition<T>(x => x.Id == id.ToString()), update, updateOptions);
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            var request = new UpdateOne
            {
                CollectionName = GetCollectionName(),
                Filter = filter.ToJson(),
                Update = update.ToJson(),
                UpdateOptions = updateOptions
            };
            
            var response = Client.Put(request);
            return response.Result;

        }

        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            return UpdateOne(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
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
        public async Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions)
        {
            var request = new InsertOne
            {
                CollectionName = GetCollectionName(),
                //Notification = notification,
                Document = entity.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }),
                //InsertOneOptions = options,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = await Client.PostAsync(request);

            if (response?.Result == null)
            {
                return default;
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }
        public async Task<T> InsertOneAsync(T entity)
        {
            return await InsertOneAsync(entity, null);
        }

        public async Task InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            var request = new InsertMany
            {
                CollectionName = GetCollectionName(),
                //Notification = notification,
                Documents = entities.Select(x =>
                    x.ToBsonDocument().ToJson(new JsonWriterSettings {OutputMode = JsonOutputMode.Strict}))
            };

            await Client.PostAsync(request);
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
        
        */
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
            throw new NotImplementedException();
        }

        public T1 InsertOne<T1>(T1 entity) where T1 : IEntity
        {
            var request = new InsertOne
            {
                CollectionName = GetCollectionName(),
                Document = entity.ToJson(new JsonWriterSettings{ OutputMode = JsonOutputMode.Strict }),
                CultureCode = CultureInfo.CurrentCulture.Name,
                
            };

            var response = Client.Post(request);

            if(response?.Result == null){
                return default(T1);
            }
            var documentAsEntity = BsonSerializer.Deserialize<T1>(response.Result);
            return documentAsEntity;
        }

        public bool InsertMany<T1>(IEnumerable<T1> entities, InsertManyOptions insertManyOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public bool InsertMany<T1>(IEnumerable<T1> entities) where T1 : IEntity
        {
            var request = new InsertMany{
                CollectionName = GetCollectionName(),
                Documents = entities.Select(x => x.ToBsonDocument().ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }))
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

        public UpdateResult UpdateOne<T1>(string id, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(ObjectId id, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> update, UpdateOptions updateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne<T1>(FilterDefinition<T1> filter, T1 entity, UpdateOptions updateOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne<T1>(Expression<Func<T1, bool>> filter, T1 entity, UpdateOptions updateOptions = null) where T1 : IEntity
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

        public List<T> Find<T>(FilterDefinition<T> filter) where T : IEntity
        {
            return Find<T,T>(filter, null, null,0, 1000);
        }

        public List<T> Find<T>(Expression<Func<T, bool>> filter) where T : IEntity
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return Find<T,T>(new ExpressionFilterDefinition<T>(filter), null, null, 0, 1000);

        }

        public List<TP> Find<T, TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, int? skip = null,
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

        public List<T> Find<T>(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null) where T : IEntity
        {
            return filter == null 
                ? Find<T, T>(new BsonDocument(), null, sort, skip, limit, findOptions) 
                : Find<T, T>(new ExpressionFilterDefinition<T>(filter), null, sort, skip, limit, findOptions);
        }

        public Task<List<T1>> FindAsync<T1>(FilterDefinition<T1> filter, FindOptions<T1, T1> findOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<List<T1>> FindAsync<T1>(Expression<Func<T1, bool>> filter, FindOptions<T1, T1> findOptions) where T1 : IEntity
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
            throw new NotImplementedException();
        }

        public T1 FindOneById<T1>(ObjectId id) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOne<T1>(FilterDefinition<T1> filter, ProjectionDefinition<T1> projection = null, FindOptions findOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOne<T1>(Expression<Func<T1, bool>> filter, ProjectionDefinition<T1> projection = null, FindOptions findOptions = null) where T1 : IEntity
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

        public T1 FindOneAndReplace<T1>(string id, T1 entity, FindOneAndReplaceOptions<T1> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndReplace<T1>(ObjectId id, T1 entity, FindOneAndReplaceOptions<T1> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndReplace<T1>(FilterDefinition<T1> filter, T1 entity, FindOneAndReplaceOptions<T1> findOneAndReplaceOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity, FindOneAndReplaceOptions<T1> findOneAndReplaceOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndReplace<T1>(Expression<Func<T1, bool>> filter, T1 entity) where T1 : IEntity
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

        public T1 FindOneAndDelete<T1>(string id, FindOneAndDeleteOptions<T1> findOneAndDeleteOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndDelete<T1>(ObjectId id, FindOneAndDeleteOptions<T1> findOneAndDeleteOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndDelete<T1>(FilterDefinition<T1> filter, FindOneAndDeleteOptions<T1> findOneAndDeleteOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter, FindOneAndDeleteOptions<T1> findOneAndDeleteOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndDelete<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
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

        public T1 FindOneAndUpdate<T1>(FilterDefinition<T1> filter, UpdateDefinition<T1> entity,
            FindOneAndUpdateOptions<T1> findOneAndUpdateOptions = null) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity, FindOneAndUpdateOptions<T1> findOneAndUpdateOptions) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public T1 FindOneAndUpdate<T1>(Expression<Func<T1, bool>> filter, UpdateDefinition<T1> entity) where T1 : IEntity
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

        public DeleteResult DeleteOne<T1>(string id) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne<T1>(ObjectId id) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne<T1>(FilterDefinition<T1> filter) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany<T1>(FilterDefinition<T1> filter) where T1 : IEntity
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany<T1>(Expression<Func<T1, bool>> filter) where T1 : IEntity
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

        public List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
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