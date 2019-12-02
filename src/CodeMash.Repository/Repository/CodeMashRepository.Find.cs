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
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Find Async */
        public Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T> findOptions)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> findOptions)
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
        
        
        
        /* Find */
        public List<T> Find(FilterDefinition<T> filter)
        {
            return Find<T>(filter, null, null, 0, 1000);
        }

        public List<T> Find(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<T>(new ExpressionFilterDefinition<T>(filter), null, null, 0, 1000);
        }

        public List<TP> Find<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection,
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

        public List<T> Find(FilterDefinition<T> filter, SortDefinition<T> sort, int? skip = null, int? limit = null,
            FindOptions findOptions = null)
        {
            return filter == null
                ? Find<T>(new BsonDocument(), null, sort, skip, limit, findOptions)
                : Find<T>(filter, null, sort, skip, limit, findOptions);
        }

        public List<T> Find(Expression<Func<T, bool>> filter, SortDefinition<T> sort, int? skip = null,
            int? limit = null, FindOptions findOptions = null)
        {
            return filter == null
                ? Find<T>(new BsonDocument(), null, sort, skip, limit, findOptions)
                : Find<T>(new ExpressionFilterDefinition<T>(filter), null, sort, skip, limit, findOptions);
        }
    }
}