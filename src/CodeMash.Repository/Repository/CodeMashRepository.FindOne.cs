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
        /* Find One Async */
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
        
        
        
        /* Find One */
        public T FindOneById(string id)
        {
            throw new NotImplementedException();
        }

        public T FindOneById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public TP FindOne<TP>(FilterDefinition<T> filter, ProjectionDefinition<T, TP> projection = null, FindOptions findOptions = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("Filter cannot be empty");
            }

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

        public TP FindOne<TP>(Expression<Func<T, bool>> filter, ProjectionDefinition<T, TP> projection = null,
            FindOptions findOptions = null)
        {
            return FindOne(new ExpressionFilterDefinition<T>(filter), projection, findOptions);
        }

        public T FindOne(FilterDefinition<T> filter, FindOptions findOptions = null)
        {
            return FindOne<T>(filter, null, findOptions);
        }

        public T FindOne(Expression<Func<T, bool>> filter, FindOptions findOptions = null)
        {
            return FindOne<T>(filter, null, findOptions);
        }
    }
}