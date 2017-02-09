using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Extensions;
using CodeMash.Interfaces.Data;
using CodeMash.ServiceModel;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Data
{
    public class Repository<T> : IRepository<T> where T : /*IEntity<string>,*/ new()
    {
        private MongoClient client { get; set; }
        private MongoUrl url { get; set; }

        private IMongoDatabase database { get; set; }
        protected IMongoDatabase Database => GetDatabase();

        private IMongoCollection<T> collection { get; set; }

        protected IMongoCollection<T> Collection
        {
            get
            {
                return GetCollection();
            }
            set { collection = value; }
        }


        public Repository()
        {
            try
            {
                var settings = CodeMashBase.Client.Get(new GetAccount());
                if (settings.HasData() && settings.Result.DataBase != null)
                {
                    url = new MongoUrl(settings.Result.DataBase.ConnectionString);
                }
            }
            catch (Exception e)
            {
                url = new MongoUrlBuilder().ToMongoUrl();
            }
            
            client = MongoClientFactory.Create(url);
            if (url.DatabaseName != null)
            {
                database = client.GetDatabase(url.DatabaseName);
            }
        }

        public Repository(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), "apiKey is not defined");
            }

            string apiAddress;

            try
            {
                apiAddress = Configuration.Address;
            }
            catch (Exception e)
            {
                apiAddress = "http://api.codemash.io/1.0/";
            }

            var jsonClient = new JsonServiceClient(apiAddress)
            {
                Credentials = new NetworkCredential(apiKey, "")
            };
            
            var accountResponse = jsonClient.Get(new GetAccount());
            if (accountResponse.HasData() && accountResponse.Result.DataBase != null)
            {
                url = new MongoUrl(accountResponse.Result.DataBase.ConnectionString);
            }

            url = new MongoUrlBuilder().ToMongoUrl();
            client = MongoClientFactory.Create(url);
            if (url.DatabaseName != null)
            {
                database = client.GetDatabase(url.DatabaseName);
            }

        }

        public Repository(MongoUrl mongoUrl)
        {
            if (mongoUrl == null)
            {
                throw new ArgumentNullException(nameof(mongoUrl), "connection string is not provided");
            }
            url = mongoUrl;
            client = MongoClientFactory.Create(url);
            database = client.GetDatabase(mongoUrl.DatabaseName ?? "test");

        }

        public Repository(MongoUrl mongoUrl, string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName), "collectionName is not provided");
            }
            if (mongoUrl == null)
            {
                throw new ArgumentNullException(nameof(mongoUrl), "connection string is not provided");
            }
            url = mongoUrl;
            client = MongoClientFactory.Create(url);
            database = client.GetDatabase(url.DatabaseName ?? "test");
            collection = database.GetCollection<T>(collectionName);
        }

        private IMongoCollection<T> GetCollection()
        {
            return collection = collection ?? GetDefaultCollection();
        }

        private IMongoCollection<T> GetDefaultCollection()
        {
            var collectionName = GetCollectionName();
            return Database.GetCollection<T>(collectionName);
        }

        private IMongoDatabase GetDatabase()
        {
            return database = database ?? GetDefaultDatabase();
        }

        private IMongoDatabase GetDefaultDatabase()
        {
            return client.GetDatabase("test");
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
                if (typeof(Entity).IsAssignableFrom(entityType))
                {
                    while (entityType != null && entityType.BaseType != typeof(Entity))
                    {
                        entityType = entityType.BaseType;
                    }
                }
                if (entityType != null) collectionName = entityType.Name;
            }

            return collectionName;
        }

        public virtual T FindOneById(string id)
        {
            return FindOneById(ObjectId.Parse(id));
        }

        public virtual T FindOneById(ObjectId id)
        {
            return FindOne(MongoDB.Driver.Builders<T>.Filter.Eq("_id", id));
        }

        public virtual async Task<T> FindOneByIdAsync(string id)
        {
            return await FindOneByIdAsync(ObjectId.Parse(id));
        }

        public virtual async Task<T> FindOneByIdAsync(ObjectId id)
        {
            return await FindOneAsync(MongoDB.Driver.Builders<T>.Filter.Eq("_id", id));
        }

        public virtual T FindOne(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOne(new ExpressionFilterDefinition<T>(filter), projection, findOptions);
        }

        public virtual async Task<T> FindOneAsync(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {

            if (filter == null)
            {
                filter = new BsonDocument();
            }
            var mCursor = await Collection.Find(filter, findOptions).ToListAsync();

            /*if (projection != null)
            {
                mCursor = mCursor.Project(projection);
            }*/

            return mCursor.FirstOrDefault();
        }

        public virtual async Task<T> FindOneAsync(Expression<Func<T, bool>> filter, ProjectionDefinition<T> projection = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
                // return await FindOneAsync(new BsonDocument(), projection, findOptions);
            }
            return await FindOneAsync(new ExpressionFilterDefinition<T>(filter), projection, findOptions);
        }

        public IRepository<T> WithCollection(string collectionName)
        {
            Collection = Database.GetCollection<T>(collectionName);
            return this;
        }

        public virtual T InsertOne(T entity, InsertOneOptions insertOneOptions)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "entity is not set");
            }

            Collection.InsertOne(entity, insertOneOptions);
            return entity;
        }

        public virtual T InsertOne(T entity)
        {
            return InsertOne(entity, null);
        }


        public virtual async Task<T> InsertOneAsync(T entity, InsertOneOptions insertOneOptions)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "entity is not set");
            }

            await Collection.InsertOneAsync(entity, insertOneOptions);
            return entity;
        }

        public virtual async Task<T> InsertOneAsync(T entity)
        {
            return await InsertOneAsync(entity, null);
        }

        public virtual void InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), "entities are not set");
            }
            Collection.InsertMany(entities, insertManyOptions);
        }

        public virtual void InsertMany(IEnumerable<T> entities)
        {
            InsertMany(entities, null);
        }

        public virtual async Task InsertManyAsync(IEnumerable<T> entities, InsertManyOptions insertManyOptions)
        {

            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), "entities are not set");
            }

            await Collection.InsertManyAsync(entities, insertManyOptions);
        }

        public virtual async Task InsertManyAsync(IEnumerable<T> entities)
        {
            await InsertManyAsync(entities, null);
        }

        public virtual UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "update is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var updateResult = Collection.UpdateOne(filter, update, updateOptions);
            return updateResult;
        }

        public virtual UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return UpdateOne(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "update is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var updateResult = await Collection.UpdateOneAsync(filter, update, updateOptions);
            return updateResult;
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await UpdateOneAsync(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }

        public virtual UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "update is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var updateResult = Collection.UpdateMany(filter, update, updateOptions);
            return updateResult;
        }

        public virtual UpdateResult UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return UpdateMany(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }


        public virtual async Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update), "update is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var updateResult = await Collection.UpdateManyAsync(filter, update, updateOptions);
            return updateResult;
        }

        public virtual async Task<UpdateResult> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update,
            UpdateOptions updateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await UpdateManyAsync(new ExpressionFilterDefinition<T>(filter), update, updateOptions);
        }

        public virtual ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "entity is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var replaceOnResult = Collection.ReplaceOne(filter, entity, updateOptions);
            return replaceOnResult;
        }

        public virtual ReplaceOneResult ReplaceOne(Expression<Func<T, bool>> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return ReplaceOne(new ExpressionFilterDefinition<T>(filter), entity, updateOptions);
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T entity, UpdateOptions updateOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "entity is not set");
            }

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var replaceOnResult = await Collection.ReplaceOneAsync(filter, entity, updateOptions);
            return replaceOnResult;
        }

        public virtual async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T entity,
            UpdateOptions updateOptions = null)
        {
            if (filter == null)
            {

                filter = _ => true;
            }
            return await ReplaceOneAsync(new ExpressionFilterDefinition<T>(filter), entity, updateOptions);
        }

        public virtual T FindOneAndReplace(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return Collection.FindOneAndReplace(filter, entity, findOneAndReplaceOptions);
        }

        public virtual T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndReplace(new ExpressionFilterDefinition<T>(filter), entity, findOneAndReplaceOptions);
        }

        public virtual T FindOneAndReplace(Expression<Func<T, bool>> filter, T entity)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndReplace(new ExpressionFilterDefinition<T>(filter), entity);
        }


        public virtual async Task<T> FindOneAndReplaceAsync(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return await Collection.FindOneAndReplaceAsync(filter, entity, findOneAndReplaceOptions);
        }

        public virtual async Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndReplaceAsync(new ExpressionFilterDefinition<T>(filter), entity, findOneAndReplaceOptions);
        }

        public virtual async Task<T> FindOneAndReplaceAsync(Expression<Func<T, bool>> filter, T entity)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndReplaceAsync(new ExpressionFilterDefinition<T>(filter), entity);
        }

        public virtual T FindOneAndDelete(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return Collection.FindOneAndDelete(filter, findOneAndDeleteOptions);
        }

        public virtual T FindOneAndDelete(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndDelete(new ExpressionFilterDefinition<T>(filter), findOneAndDeleteOptions);
        }

        public virtual T FindOneAndDelete(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndDelete(new ExpressionFilterDefinition<T>(filter));
        }

        public virtual async Task<T> FindOneAndDeleteAsync(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return await Collection.FindOneAndDeleteAsync(filter, findOneAndDeleteOptions);
        }

        public virtual async Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter, FindOneAndDeleteOptions<T> findOneAndDeleteOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndDeleteAsync(new ExpressionFilterDefinition<T>(filter), findOneAndDeleteOptions);
        }

        public virtual async Task<T> FindOneAndDeleteAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndDeleteAsync(new ExpressionFilterDefinition<T>(filter));
        }

        public virtual T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return Collection.FindOneAndUpdate(filter, entity, findOneAndUpdateOptions);
        }

        public virtual T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity,
            FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndUpdate(new ExpressionFilterDefinition<T>(filter), entity, findOneAndUpdateOptions);
        }

        public virtual T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return FindOneAndUpdate(new ExpressionFilterDefinition<T>(filter), entity);
        }

        public virtual async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            return await Collection.FindOneAndUpdateAsync(filter, entity, findOneAndUpdateOptions);
        }

        public virtual async Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndUpdateAsync(new ExpressionFilterDefinition<T>(filter), entity, findOneAndUpdateOptions);
        }

        public virtual async Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> entity)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await FindOneAndUpdateAsync(new ExpressionFilterDefinition<T>(filter), entity);
        }
        public virtual DeleteResult DeleteOne(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            var deleteResult = Collection.DeleteOne(filter);
            return deleteResult;
        }

        public virtual DeleteResult DeleteOne(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return DeleteOne(new ExpressionFilterDefinition<T>(filter));
        }


        public virtual async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            var deleteResult = await Collection.DeleteOneAsync(filter);
            return deleteResult;
        }

        public virtual async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await DeleteOneAsync(new ExpressionFilterDefinition<T>(filter));
        }

        public virtual DeleteResult DeleteMany(FilterDefinition<T> filter)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var deleteResult = Collection.DeleteMany(filter);

            return deleteResult;
        }

        public virtual DeleteResult DeleteMany(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return DeleteMany(new ExpressionFilterDefinition<T>(filter));
        }

        public virtual async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var deleteResult = await Collection.DeleteManyAsync(filter);

            return deleteResult;
        }

        public virtual async Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            return await DeleteManyAsync(new ExpressionFilterDefinition<T>(filter));
        }

        public virtual List<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection, SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            
            var mCursor = Collection
                .Find(filter, findOptions)
                .Sort(sort)
                .Project(projection);
            
            mCursor = skip.HasValue ? mCursor.Skip(skip.Value) : mCursor.Skip(0);
            mCursor = limit.HasValue ? mCursor.Limit(limit.Value) : mCursor.Limit(1000/*Extensions.ToInt(AppSettings.DefaultPageSize)*/);
            
            var result = mCursor.ToList();
            return result;
        }

        public virtual List<TP> Find<TP>(Expression<Func<T, bool>> filter, Expression<Func<T, TP>> projectionExpression, SortDefinition<T> sort = null, int? skip = null, int? limit = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            var projectionDefinition = new ProjectionDefinitionBuilder<T>().Expression(projectionExpression);
            return Find(new ExpressionFilterDefinition<T>(filter), projectionDefinition, sort, skip, limit, findOptions);
        }

        public virtual List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            
            var mCursor = Collection.Find(filter, findOptions);
            
            if (sort != null)
            {
                mCursor = mCursor.Sort(sort);
            }

            // generic constraint to not allow get all database records once
            // TODO : this should be configurable for each client. Lets say fs has limit of 2000 record per call, but for client let's say wise to have 50 per page as default
            mCursor = skip.HasValue ? mCursor.Skip(skip.Value) : mCursor.Skip(0);
            mCursor = limit.HasValue ? mCursor.Limit(limit.Value) : mCursor.Limit(1000/*Extensions.ToInt(AppSettings.DefaultPageSize)*/);
            
            var result = mCursor.ToList();
            return result;
        }

        public virtual List<T> Find(FilterDefinition<T> filter)
        {
            return Find(filter, null, 0, 1000); // Extensions.ToInt(ConfigurationManager.AppSettings["DefaultPageSize"]));
        }

        public virtual List<T> Find(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;//return await FindAsync(new BsonDocument(), null, null, 0, Extensions.ToInt(ConfigurationManager.AppSettings["DefaultPageSize"]));
            }
            return Find(new ExpressionFilterDefinition<T>(filter), null, 0, 1000); // Extensions.ToInt(ConfigurationManager.AppSettings["DefaultPageSize"]));
        }

        public virtual List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null, int? limit = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;// return await FindAsync(new BsonDocument(), projection, sort, skip, limit, findOptions);
            }
            return Find(new ExpressionFilterDefinition<T>(filter), sort, skip, limit, findOptions);
        }


        public virtual async Task<List<TP>> FindAsync<TP>(FilterDefinition<T> filter, FindOptions<T, TP> findOptions)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var mCursor = await Collection.FindAsync(filter, findOptions);
            
            var result = await mCursor.ToListAsync();
            return result;
        }

        public virtual async Task<List<TP>> FindAsync<TP>(Expression<Func<T, bool>> filter, FindOptions<T, TP> findOptions)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            var mCursor = await Collection.FindAsync(new ExpressionFilterDefinition<T>(filter), findOptions);
            var result = await mCursor.ToListAsync();
            return result;
        }

        public virtual async Task<List<T>> FindAsync(FilterDefinition<T> filter)
        {
            var findOptions = new FindOptions<T, T>
            {
                Skip = 0,
                Limit = 1000, //  Extensions.ToInt(ConfigurationManager.AppSettings["DefaultPageSize"])
                Sort = null
            };
            return await FindAsync(filter, findOptions); 
        }

        public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }
            var findOptions = new FindOptions<T, T>
            {
                Skip = 0,
                Limit = 1000, //  Extensions.ToInt(ConfigurationManager.AppSettings["DefaultPageSize"])
                Sort = null
            };
            return await FindAsync(new ExpressionFilterDefinition<T>(filter), findOptions);
        }
        
        public virtual List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions options)
        {
            if (aggregation == null)
            {
                throw new ArgumentNullException(nameof(aggregation), "aggregation is not set");
            }

            var mCursor = Collection.Aggregate(aggregation, options);
            var result = mCursor.ToList();
            return result;
        }

        public virtual async Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions options)
        {
            if (aggregation == null)
            {
                throw new ArgumentNullException(nameof(aggregation), "aggregation is not set");
            }

            var mCursor = await Collection.AggregateAsync(aggregation, options);
            var result = await mCursor.ToListAsync();
            return result;
        }

        public virtual long Count(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            return Collection.Count(filter, countOptions);
        }

        public virtual long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {

            if (filter == null)
            {
                filter = _ => true;
            }
            return Count(new ExpressionFilterDefinition<T>(filter), countOptions);
        }

        public virtual async Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            return await Collection.CountAsync(filter, countOptions);
        }

        public virtual async Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {

            if (filter == null)
            {
                filter = _ => true;
            }
            return await CountAsync(new ExpressionFilterDefinition<T>(filter), countOptions);
        }

        public virtual List<string> Distinct(string field, FilterDefinition<T> filter, DistinctOptions options = null)
        {

            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }

            var cursor = Collection.Distinct<string>(field, filter, options);
            var result = cursor.ToList();
            return result;
        }

        public virtual List<string> Distinct(string field, Expression<Func<T, bool>> filter, DistinctOptions options = null)
        {
            if (filter == null)
            {
                return Distinct(field, _ => true, options);
            }
            return Distinct(field, new ExpressionFilterDefinition<T>(filter), options);
        }

        public virtual async Task<List<string>> DistinctAsync(string field, FilterDefinition<T> filter, DistinctOptions options = null)
        {

            if (filter == null)
            {
                filter = new BsonDocument();
            }

            var cursor = await Collection.DistinctAsync<string>(field, filter, options);
            var result = await cursor.ToListAsync();
            return result;
        }

        public virtual async Task<List<string>> DistinctAsync(string field, Expression<Func<T, bool>> filter, DistinctOptions options = null)
        {
            if (filter == null)
            {
                return await DistinctAsync(field, _ => true, options);
            }
            return await DistinctAsync(field, new ExpressionFilterDefinition<T>(filter), options);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Collection.AsQueryable().GetEnumerator();
        }

        public virtual UpdateResult UpdateOne(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            return UpdateOne(ObjectId.Parse(id), update, updateOptions);
        }

        public virtual UpdateResult UpdateOne(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return UpdateOne(filter, update, updateOptions);
        }

        public virtual T FindOne(FilterDefinition<T> filter, ProjectionDefinition<T> projection = null,
            FindOptions findOptions = null) 
        {
            if (filter == null)
            {
                filter = Builders<T>.Filter.Empty;
            }
            var mCursor = Collection.Find(filter, findOptions);

            /*if (projection != null)
            {
                mCursor = mCursor.Project()
            }*/

            return mCursor.FirstOrDefault();
        }

        public virtual T FindOneAndReplace(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            return FindOneAndReplace(ObjectId.Parse(id), entity, findOneAndReplaceOptions);
        }

        public virtual T FindOneAndReplace(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return FindOneAndReplace(filter, entity, findOneAndReplaceOptions);
        }

        public virtual T FindOneAndDelete(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            return FindOneAndDelete(ObjectId.Parse(id), findOneAndDeleteOptions);
        }
        public virtual T FindOneAndDelete(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return FindOneAndDelete(filter, findOneAndDeleteOptions);
        }

        public virtual DeleteResult DeleteOne(string id)
        {
            return DeleteOne(ObjectId.Parse(id));
        }

        public virtual DeleteResult DeleteOne(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);

            return DeleteOne(filter);
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(string id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {
            return await UpdateOneAsync(ObjectId.Parse(id), update, updateOptions);
        }

        public virtual async Task<UpdateResult> UpdateOneAsync(ObjectId id, UpdateDefinition<T> update, UpdateOptions updateOptions)
        {

            var filter = Builders<T>.Filter.Eq("_id", id);
            return await UpdateOneAsync(filter, update, updateOptions);
        }

        public virtual async Task<T> FindOneAndReplaceAsync(string id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {
            return await FindOneAndReplaceAsync(ObjectId.Parse(id), entity, findOneAndReplaceOptions);
        }

        public virtual async Task<T> FindOneAndReplaceAsync(ObjectId id, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null)
        {

            var filter = Builders<T>.Filter.Eq("_id", id);

            return await FindOneAndReplaceAsync(filter, entity, findOneAndReplaceOptions);
        }

        public virtual async Task<T> FindOneAndDeleteAsync(string id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {
            return await FindOneAndDeleteAsync(ObjectId.Parse(id), findOneAndDeleteOptions);
        }

        public virtual async Task<T> FindOneAndDeleteAsync(ObjectId id, FindOneAndDeleteOptions<T> findOneAndDeleteOptions = null)
        {

            var filter = Builders<T>.Filter.Eq("_id", id);
            return await FindOneAndDeleteAsync(filter, findOneAndDeleteOptions);
        }

        public async Task<DeleteResult> DeleteOneAsync(string id)
        {
            return await DeleteOneAsync(ObjectId.Parse(id));
        }

        public async Task<DeleteResult> DeleteOneAsync(ObjectId id)
        {

            var filter = Builders<T>.Filter.Eq("_id", id);
            return await DeleteOneAsync(filter);
        }

        public Expression Expression => Collection.AsQueryable().Expression;

        public Type ElementType => Collection.AsQueryable().ElementType;

        public virtual IQueryProvider Provider => Collection.AsQueryable().Provider;
    }
}