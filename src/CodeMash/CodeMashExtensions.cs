using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Data.MongoDB;
using CodeMash.ServiceModel;
using ServiceStack;

namespace CodeMash
{
    public static class CodeMashExtensions
    {
        
        public static IRequestContext<T> ToRequestContext<T>(this IRequestBase request, AuthUserSession session = null) where T : EntityBase
        {
            return RequestContext<T>
                .Create(request)
                .WithPagination(request as IRequestWithPaging)
                .WithSorting(request as IRequestWithSorting)
                .WithSession(session)
                .WithFilter()
                .Build();
        }
        
        public static async Task<T> As<T>(this Task<BsonDocument> source)
        {
            var doc = await source;
            if (doc == null)
            {
                return default(T);
            }
            var json = BsonExtensionMethods.ToJson(doc);
            T entity = BsonSerializer.Deserialize<T>(json);
            return entity;
        }

        public static async Task<List<T>> As<T>(this Task<List<BsonDocument>> source)
        {
            var docs = await source;
            if (docs == null)
            {
                return default(List<T>);
            }
            var list = BsonSerializer.Deserialize<List<T>>(docs.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }));
            return list;
        }

        public static BsonDocument RenderToBsonDocument<T>(this FilterDefinition<T> filter)
        {
            if (filter == null)
            {
                return new BsonDocument();
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, serializerRegistry);
        }

        public static BsonDocument RenderToBsonDocument<T>(this UpdateDefinition<T> update)
        {
            if (update == null)
            {
                return new BsonDocument();
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return update.Render(documentSerializer, serializerRegistry);
        }

        public static string ToJson<T>(this FilterDefinition<T> filter, JsonOutputMode outputMode = JsonOutputMode.Strict)
        {
            if (filter == null)
            {
                return "{}";
            }
            var filterToBson = filter.RenderToBsonDocument();
            return filterToBson.ToJson(new JsonWriterSettings { OutputMode = outputMode });
        }

        public static string ToJson<T>(this ProjectionDefinition<T> projection, JsonOutputMode outputMode = JsonOutputMode.Strict)
        {
            if (projection == null)
            {
                return string.Empty;
            }
            var projectionToBson = projection.RenderToBsonDocument();
            return projectionToBson.ToJson(new JsonWriterSettings { OutputMode = outputMode });
        }

        public static FindOneAndReplaceOptions<BsonDocument> ToBson<T>(this FindOneAndReplaceOptions<T> options)
        {
            if (options == null)
            {
                return null;
            }
            return new FindOneAndReplaceOptions<BsonDocument>
            {
                IsUpsert = options.IsUpsert,
                MaxTime = options.MaxTime,
                Projection = options.Projection.ToBsonDocument(),
                ReturnDocument = options.ReturnDocument,
                Sort = options.Sort.ToBsonDocument()
            };
            
        }

        public static FindOneAndDeleteOptions<BsonDocument> ToBson<T>(this FindOneAndDeleteOptions<T> options)
        {
            if (options == null)
            {
                return null;
            }
            return new FindOneAndDeleteOptions<BsonDocument>
            {
                MaxTime = options.MaxTime,
                Sort = options.Sort.ToBsonDocument(),
                Projection = options.Projection.ToBsonDocument()
            };

        }

        public static FindOneAndUpdateOptions<BsonDocument> ToBson<T>(this FindOneAndUpdateOptions<T> options)
        {
            if (options == null)
            {
                return null;
            }
            return new FindOneAndUpdateOptions<BsonDocument>
            {
                IsUpsert = options.IsUpsert,
                MaxTime = options.MaxTime,
                Projection = options.Projection.ToBsonDocument(),
                ReturnDocument = options.ReturnDocument,
                Sort = options.Sort.ToBsonDocument()
                
            };

        }

        public static BsonDocument RenderToBsonDocument<T>(this ProjectionDefinition<T> projectionDefinition)
        {
            if (projectionDefinition == null)
            {
                return new BsonDocument();
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return projectionDefinition.Render(documentSerializer, serializerRegistry);
        }

        public static List<BsonDocument> RenderToBsonDocumentsList<T, TOutput>(this PipelineDefinition<T, TOutput> aggregate)
        {
            if (aggregate == null)
            {
                return new List<BsonDocument>();
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            var pipeline = aggregate.Render(documentSerializer, serializerRegistry);
            return pipeline.Documents.Any() 
                ? pipeline.Documents.ToList() 
                : new List<BsonDocument>();
        }


        public static string ToJson<T, TOutput>(this PipelineDefinition<T, TOutput> aggregate, JsonOutputMode outputMode = JsonOutputMode.Strict)
        {
            var docs = RenderToBsonDocumentsList(aggregate);
            return docs.Count > 0 
                ? docs.ToJson(new JsonWriterSettings { OutputMode = outputMode }) 
                : string.Empty;
        }

        public static BsonDocument RenderToBsonDocument<T>(this SortDefinition<T> sortDefinition)
        {
            if (sortDefinition == null)
            {
                return new BsonDocument();
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return sortDefinition.Render(documentSerializer, serializerRegistry);
        }

        public static string ToJson<T>(this SortDefinition<T> sortDefinition, JsonOutputMode outputMode = JsonOutputMode.Strict)
        {
            if (sortDefinition == null)
            {
                return string.Empty;
            }
            var sortToBson = sortDefinition.RenderToBsonDocument();
            return sortToBson.ToJson(new JsonWriterSettings { OutputMode = outputMode });
        }

        public static string ToJson<T>(this UpdateDefinition<T> updateDefinition, JsonOutputMode outputMode = JsonOutputMode.Strict)
        {
            if (updateDefinition == null)
            {
                return string.Empty;
            }
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            var bson = updateDefinition.Render(documentSerializer, serializerRegistry);
            return bson.ToJson(new JsonWriterSettings { OutputMode = outputMode });
        }
        
        public static byte[] ReadFully(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        /*public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                    ? valueType.GetGenericArguments()[0]
                    : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IDictionary<string, object>;
                    if (enumerable != null)
                    {
                        string dic = enumerable.Aggregate(string.Empty, (current, item) => current + (item.Key + ":" + item.Value + ","));
                        if (dic.Length > 0)
                        {
                            dic = dic.Substring(0, dic.Length - 1);
                        }
                        dic = "{" + dic + "}";
                        properties[key] = dic;
                    }
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }*/
    }
}
