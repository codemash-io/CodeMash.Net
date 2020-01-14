﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Bson;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Insert Async */
        public async Task<T> InsertOneAsync(T entity, DatabaseInsertOneOptions insertOneOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity is not defined");
            }

            if (string.IsNullOrEmpty(entity.Id) || entity.Id == ObjectId.Empty.ToString())
            {
                entity.Id = new ObjectId().ToString();
            }
            
            var request = new InsertOneRequest
            {
                CollectionName = GetCollectionName(),
                Document = JsonConverterHelper.SerializeEntity(entity),
                BypassDocumentValidation = insertOneOptions?.BypassDocumentValidation ?? false,
            };

            var response = await Client.PostAsync<InsertOneResponse>(request);
            if (response?.Result == null)
            {
                return default(T);
            }

            return JsonConverterHelper.DeserializeEntity<T>(response.Result, null);
        }
        
        public async Task<bool> InsertManyAsync(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities), "Entities are not defined");
            }
            
            var request = new InsertManyRequest
            {
                CollectionName = GetCollectionName(),
                Documents = entities.ConvertAll(x =>
                {
                    if (x.Id == ObjectId.Empty.ToString()) x.Id = ObjectId.GenerateNewId().ToString();
                    return JsonConverterHelper.SerializeEntity(x);
                })
            };

            var response = await Client.PostAsync<InsertManyResponse>(request);
            return response.Result;
        }

        
        /* Insert */
        public T InsertOne(T entity, DatabaseInsertOneOptions insertOneOptions = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity is not defined");
            }

            if (string.IsNullOrEmpty(entity.Id) || entity.Id == ObjectId.Empty.ToString())
            {
                entity.Id = new ObjectId().ToString();
            }
            
            var request = new InsertOneRequest
            {
                CollectionName = GetCollectionName(),
                Document = JsonConverterHelper.SerializeEntity(entity),
                BypassDocumentValidation = insertOneOptions?.BypassDocumentValidation ?? false,
            };

            var response = Client.Post<InsertOneResponse>(request);
            if (response?.Result == null)
            {
                return default(T);
            }

            return JsonConverterHelper.DeserializeEntity<T>(response.Result, null);
        }
        
        public bool InsertMany(List<T> entities, DatabaseInsertManyOptions insertManyOptions = null)
        {
            if (entities == null || !entities.Any())
            {
                throw new ArgumentNullException(nameof(entities), "Entities are not defined");
            }
            
            var request = new InsertManyRequest
            {
                CollectionName = GetCollectionName(),
                Documents = entities.ConvertAll(x =>
                {
                    if (x.Id == ObjectId.Empty.ToString()) x.Id = ObjectId.GenerateNewId().ToString();
                    return JsonConverterHelper.SerializeEntity(x);
                })
            };

            var response = Client.Post<InsertManyResponse>(request);
            return response.Result;
        }
    }
}