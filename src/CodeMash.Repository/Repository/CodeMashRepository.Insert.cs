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
        /* Insert Async */
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

        
        
        /* Insert */
        public T InsertOne(T entity, InsertOneOptions insertOneOptions)
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

            var documentAsEntity = BsonSerializer.Deserialize<T>(response.Result);
            return documentAsEntity;
        }

        public T InsertOne(T entity)
        {
            return InsertOne(entity, null);
        }

        public bool InsertMany(IEnumerable<T> entities, InsertManyOptions insertManyOptions = null)
        {
            throw new NotImplementedException();
        }

        public bool InsertMany(IEnumerable<T> entities)
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
    }
}