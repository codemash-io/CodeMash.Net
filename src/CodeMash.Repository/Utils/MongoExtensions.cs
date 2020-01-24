using System;
using System.IO;
using System.Text.RegularExpressions;
using CodeMash.Models;
using Isidos.CodeMash.Utils;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CodeMash.Repository
{
    public static class MongoExtensions
    {
        public static string FilterToJson<T>(this FilterDefinition<T> filter)
        {
            var isEntity = typeof(IEntity).IsAssignableFrom(typeof(T));
            if (isEntity)
            {
                var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
                var filterString = filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();

                filterString = JsonConverterHelper.ReplaceIsoDateToLong(filterString, true);

                return filterString;
            }
            else
            {
                var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
                return filter.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            }
        }
        
        public static string ProjectionToJson<T, TP>(this ProjectionDefinition<T, TP> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
            return projectionInfo.Document?.ToString();
        }
        
        public static string ProjectionToJson<T>(this ProjectionDefinition<T> projection)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            var projectionInfo = projection.Render(documentSerializer, BsonSerializer.SerializerRegistry);
            return projectionInfo?.ToString();
        }
        
        public static string SortToJson<T>(this SortDefinition<T> sort)
        {
            var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
            return sort.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
        }
        
        public static string UpdateToJson<T>(this UpdateDefinition<T> update)
        {
            var isEntity = typeof(IEntity).IsAssignableFrom(typeof(T));
            if (isEntity)
            {
                var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
                var updateString = update.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();

                updateString = JsonConverterHelper.ReplaceIsoDateToLong(updateString, true);
                return updateString;
            }
            else
            {
                var documentSerializer = BsonSerializer.SerializerRegistry.GetSerializer<T>();
                return update.Render(documentSerializer, BsonSerializer.SerializerRegistry).ToString();
            }
        }
    }
}