using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeMash.Net.DataContracts;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Globalization;
using ServiceStack;

namespace CodeMash.Net
{
    public class Db : CodeMashBase
    {
        private static List<TProjection> Find<T, TProjection>(FilterDefinition<T> filter, SortDefinition<T> sort = null,
            ProjectionDefinition<T, TProjection> projection = null, int? pageNumber = 0, int? pageSize = 100,
            FindOptions findOptions = null, bool includeSchema = false) where TProjection : new()
        {

            var serializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(serializer, BsonSerializer.SerializerRegistry);

            var projectionAsJson = string.Empty;
            if (projectionInfo.Document != null)
            {
                projectionAsJson = BsonExtensionMethods.ToJson(projectionInfo.Document);
            }

            var request = new Find
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                Projection = projectionAsJson,
                Sort = sort.ToJson(),
                FindOptions = findOptions,
                PageSize = pageSize ?? 100,
                PageNumber = pageNumber ?? 0,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            var response = Client.Post<FindResponse>(request);
            
            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TProjection>>(response.Result);
                return list;
            }
            return new List<TProjection>();
        }
        
        public static List<T> Find<T>(FilterDefinition<T> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false)
        {
            var request = new Find
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                Projection = projection.ToJson(),
                Sort = sort.ToJson(),
                FindOptions = findOptions,
                PageSize = pageSize ?? 100,
                PageNumber = pageNumber ?? 0,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name

            };
            
            var response = Client.Post<FindResponse>(request);

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<T>>(response.Result);
                return list;
            }
            return new List<T>();
        }
        

        public static List<T> Find<T>(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, ProjectionDefinition<T> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false)
        {
            return filter == null 
                ? Find(new BsonDocument(), sort, projection, pageNumber, pageSize, findOptions, includeSchema) 
                : Find(new ExpressionFilterDefinition<T>(filter), sort, projection, pageNumber, pageSize, findOptions, includeSchema);
        }

        public static List<TProjection> Find<T, TProjection>(Expression<Func<T, bool>> filter, SortDefinition<T> sort = null, Expression<Func<T, TProjection>> projection = null, int? pageNumber = 0, int? pageSize = 100, FindOptions findOptions = null, bool includeSchema = false) where TProjection : new()
        {
            return filter == null 
                ? Find(new BsonDocument(), sort, Builders<T>.Projection.Expression(projection), pageNumber, pageSize, findOptions, includeSchema) 
                : Find(new ExpressionFilterDefinition<T>(filter), sort, Builders<T>.Projection.Expression(projection), pageNumber, pageSize, findOptions, includeSchema);
        }

        public static T FindOne<T>(FilterDefinition<T> filter, ProjectionDefinition<BsonDocument> projection, FindOptions findOptions = null, bool includeSchema = false)
        {
            var request = new FindOne
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                Projection = projection.ToJson(),
                FindOptions = findOptions,
                IncludeSchema = includeSchema,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            
            var response = Client.Post<FindOneResponse>(request);

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        public static T FindOne<T>(Expression<Func<T, bool>> filter, ProjectionDefinition<BsonDocument> projection, FindOptions findOptions = null, bool includeSchema = false)
        {
            return filter == null 
                ? FindOne<T>(new BsonDocument(), projection, findOptions, includeSchema) 
                : FindOne(new ExpressionFilterDefinition<T>(filter), projection, findOptions, includeSchema);
        }

        public static T FindOne<T>(Expression<Func<T, bool>> filter)
        {
            return filter == null 
                ? FindOne<T>(new BsonDocument(), null) 
                : FindOne(new ExpressionFilterDefinition<T>(filter), null);
        }

        public static T FindOne<T>(FilterDefinition<T> filter)
        {
            return filter == null 
                ? FindOne<T>(new BsonDocument(), null) 
                : FindOne(filter, null);
        }


        public static T FindOneById<T>(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return FindOne(filter, null);
        }


        public static T FindOneAndReplace<T>(FilterDefinition<T> filter, T entity, FindOneAndReplaceOptions<T> findOneAndReplaceOptions = null, Notification notification = null)
        {
            if (findOneAndReplaceOptions == null)
            {
                findOneAndReplaceOptions = new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                };
            }

            var request = new FindOneAndReplace
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                FindOneAndReplaceOptions = findOneAndReplaceOptions.ToBson(),
                Document = BsonExtensionMethods.ToJson(entity),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            var response = Client.Post<FindOneAndReplaceResponse>(request);
            
            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        public static T FindOneAndDelete<T>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T> findOneAndReplaceOptions = null, Notification notification = null)
        {
            var request = new FindOneAndDelete
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                FindOneAndDeleteOptions = findOneAndReplaceOptions.ToBson(),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            var response = Client.Post<FindOneAndDeleteResponse>(request);

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }



        public static T FindOneAndUpdate<T>(FilterDefinition<BsonDocument> filter, UpdateDefinition<T> entity, FindOneAndUpdateOptions<T> findOneAndUpdateOptions = null, Notification notification = null)
        {
            if (findOneAndUpdateOptions == null)
            {
                findOneAndUpdateOptions = new FindOneAndUpdateOptions<T>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                };
            }

            var request = new FindOneAndUpdate
            {
                CollectionName = GetCollectionName<T>(),
                Filter = filter.ToJson(),
                FindOneAndUpdateOptions = findOneAndUpdateOptions.ToBson(),
                Document = entity.RenderToBsonDocument(),
                Notification = notification,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            
            var response = Client.Post<FindOneAndUpdateResponse>(request);

            if (response != null && response.Result.Any())
            {
                var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
                return documentAsEntity;
            }
            return default(T);
        }

        /*public static  <SaveFileResponse> UploadFile(SaveFile request)
        {
            var response =  Send<SaveFileResponse>("files", request, "POST");
            return response;
        }*/

        public static List<TOutput> Aggregate<TInput, TOutput>(PipelineDefinition<TInput, TOutput> aggregation, AggregateOptions options)
        {
            var request = new Aggregate
            {
                CollectionName = GetCollectionName<TInput>(),
                AggregateOptions = options,
                Aggregation = aggregation.ToJson(),
                OutputMode = JsonOutputMode.Strict,
            };

            
            var response = Client.Post<AggregateResponse>(request);
            
            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TOutput>>(response.Result);
                return list;
            }
            return new List<TOutput>();
        }

        public static List<TOutput> Aggregate<TInput, TOutput>(List<BsonDocument> stages, AggregateOptions options)
        {
            var request = new Aggregate
            {
                CollectionName = GetCollectionName<TInput>(),
                AggregateOptions = options,
                Aggregation = stages.ToJson(new JsonWriterSettings() { OutputMode = JsonOutputMode.Strict }),
                OutputMode = JsonOutputMode.Strict,
            };

            
            var response = Client.Post<AggregateResponse>(request);

            if (response != null && response.Result.Any())
            {
                var list = BsonSerializer.Deserialize<List<TOutput>>(response.Result);
                return list;
            }
            return new List<TOutput>();
        }

        public static long Count<T>(FilterDefinition<T> filter, CountOptions options = null)
        {
            if (filter == null)
            {
                filter = new BsonDocumentFilterDefinition<T>(new BsonDocument());
            }

            var request = new Count
            {
                CollectionName = GetCollectionName<T>(),
                CountOptions = options,
                Filter = filter.ToJson(),
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            
            var response = Client.Post<CountResponse>(request);

            if (response != null)
            {
                return response.Result;
            }
            return 0;
        }

        public static long Count<T>(Expression<Func<T, bool>> filter, CountOptions options = null)
        {
            return filter == null
                ? Count<T>(new BsonDocument() , options)
                : Count(new ExpressionFilterDefinition<T>(filter), options);
        }

        public static List<string> Distinct<T>(string field, FilterDefinition<BsonDocument> filter, DistinctOptions options = null)
        {
            var request = new Distinct
            {
                CollectionName = GetCollectionName<T>(),
                DistinctOptions = options,
                Filter = filter.ToJson(),
                Field = field,
                OutputMode = JsonOutputMode.Strict,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            
            var response = Client.Post<DistinctResponse>(request);

            if (response != null)
            {
                return response.Result;
            }
            return new List<string>();
        }

        public static void InsertOne<T>(T document, /* InsertOneOptions options, */ Notification notification = null) where T : EntityBase
        {
            var request = new InsertOne
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Notification = notification,
                Document = document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }),
                //InsertOneOptions = options,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            
            var response = Client.Post<InsertOneResponse>(request);
            
            if (response?.Result == null)
            {
                return;
            }
            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            document.Id = documentAsEntity.Id;
        }


        public static bool InsertMany<T>(IEnumerable<T> documents, InsertManyOptions insertManyOptions = null, Notification notification = null)
        {
            var request = new InsertMany
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Notification = notification,
                Documents = documents.Select(x => x.ToBsonDocument().ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }))
            };
            
            var response = Client.Post<InsertManyResponse>(request);
            
            return response?.Result != null && response.Result;
        }

        public static void ReplaceOne<T>(FilterDefinition<T> filter, T document, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new ReplaceOne
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Document = BsonExtensionMethods.ToJson(document.ToBsonDocument()),
                UpdateOptions = updateOptions,
                SchemaId = schemaId
            };

            
            var response = Client.Post<ReplaceOneResponse>(request);
            if (response?.Result == null)
            {
                return; // default(T);
            }
            document = BsonSerializer.Deserialize<T>(response.Result);
            //return documentAsEntity;
        }

        public static void ReplaceOne<T>(Expression<Func<T, bool>> filter, T document, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            ReplaceOne(new ExpressionFilterDefinition<T>(filter), document, updateOptions, schemaId, notification);
        }

        public static void FindOneAndReplace<T>(FilterDefinition<T> filter, T document, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new FindOneAndReplace
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Document = BsonExtensionMethods.ToJson(document.ToBsonDocument()),
                FindOneAndReplaceOptions = findOneAndReplaceOptions
            };
            
            var response = Client.Post<FindOneAndReplaceResponse>(request);

            if (response?.Result == null)
            {
                return; // default(T);
            }
            document = BsonSerializer.Deserialize<T>(response.Result);
            // return documentAsEntity;
        }

        public static void FindOneAndReplace<T>(Expression<Func<T, bool>> filter, T document, FindOneAndReplaceOptions<BsonDocument> findOneAndReplaceOptions = null, string schemaId = "", Notification notification = null)
        {
            FindOneAndReplace(new ExpressionFilterDefinition<T>(filter), document, findOneAndReplaceOptions, schemaId, notification);
        }


        public static DataContracts.UpdateResult UpdateOne<T>(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            var request = new UpdateOne
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Update = update.ToJson(),
                UpdateOptions = updateOptions,
                SchemaId = schemaId
            };
            
            var response = Client.Put<UpdateOneResponse>(request);
            return response.Result;
        }

        public static DataContracts.UpdateResult UpdateOne<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            return UpdateOne(new ExpressionFilterDefinition<T>(filter), update, updateOptions, schemaId, notification);
        }

        public static DataContracts.UpdateResult UpdateMany<T>(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions updateOptions, string schemaId = "", Notification notification = null)
        {
            var request = new UpdateMany
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                Update = BsonExtensionMethods.ToJson(update.ToBsonDocument()),
                UpdateOptions = updateOptions,
                SchemaId = schemaId,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            var response = Client.Put<UpdateOneResponse>(request);
            return response.Result;
        }

        public static DataContracts.UpdateResult UpdateMany<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, UpdateOptions updateOptions = null, string schemaId = "", Notification notification = null)
        {
            return UpdateMany(new ExpressionFilterDefinition<T>(filter), update, updateOptions, schemaId, notification);
        }

        public static DataContracts.DeleteResult DeleteOne<T>(FilterDefinition<T> filter, Notification notification = null)
        {
            var request = new DeleteOne
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                CultureCode = CultureInfo.CurrentCulture.Name,

            };
            
            var response = Client.Delete<DeleteOneResponse>(request);
            return response.Result;
        }

        public static DataContracts.DeleteResult DeleteOne<T>(Expression<Func<T, bool>> filter, Notification notification = null)
        {
            return DeleteOne(new ExpressionFilterDefinition<T>(filter), notification);
        }

        public static DataContracts.DeleteResult DeleteOne<T>(string id, Notification notification = null)
        {
            FilterDefinition<BsonDocument> filter = new BsonDocument("_id", ObjectId.Parse(id));
            return DeleteOne(filter, notification);
        }

        public static DataContracts.DeleteResult DeleteMany<T>(FilterDefinition<T> filter, Notification notification = null)
        {
            var request = new DeleteMany
            {
                CollectionName = GetCollectionName<T>(),
                OutputMode = JsonOutputMode.Strict,
                Filter = filter.ToJson(),
                Notification = notification,
                CultureCode = CultureInfo.CurrentCulture.Name
            };
            
            var response = Client.Delete<DeleteManyResponse>(request);
            return response.Result;
        }

        public static DataContracts.DeleteResult DeleteMany<T>(Expression<Func<T, bool>> filter, Notification notification = null)
        {
            return DeleteMany(new ExpressionFilterDefinition<T>(filter), notification);
        }
        
        private static string GetCollectionName<T>()
        {
            var collectionName = typeof(T).BaseType == typeof(object)
                ? GetCollectionNameFromInterface<T>()
                : GetCollectionNameFromType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        private static string GetCollectionNameFromInterface<T>()
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

    }
}