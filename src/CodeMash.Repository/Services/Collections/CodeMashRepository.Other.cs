using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        public async Task<long> CountAsync(FilterDefinition<T> filter, DatabaseCountOptions countOptions = null)
        {
            var request = FormCountRequest(filter, countOptions);
            var clientResponse = await Client.GetAsync<CountResponse>(request);
            return clientResponse.Result;
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> filter, DatabaseCountOptions countOptions = null)
        {
            if (filter == null) filter = _ => true;
            return await CountAsync(new ExpressionFilterDefinition<T>(filter), countOptions);
        }

        public async Task<long> CountAsync(DatabaseCountOptions countOptions = null)
        {
            return await CountAsync(null, countOptions);
        }

        public long Count(FilterDefinition<T> filter, DatabaseCountOptions countOptions = null)
        {
            var request = FormCountRequest(filter, countOptions);
            var clientResponse = Client.Get<CountResponse>(request);
            return clientResponse.Result;
        }

        public long Count(Expression<Func<T, bool>> filter, DatabaseCountOptions countOptions = null)
        {
            if (filter == null) filter = _ => true;
            return Count(new ExpressionFilterDefinition<T>(filter), countOptions);
        }

        public long Count(DatabaseCountOptions countOptions = null)
        {
            return Count(null, countOptions);
        }
        
        private CountRequest FormCountRequest(FilterDefinition<T> filter, DatabaseCountOptions countOptions = null)
        {
            var request = new CountRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Limit = countOptions?.Limit,
                Skip = countOptions?.Skip
            };

            return request;
        }
        
        public async Task<List<object>> DistinctAsync(string field, FilterDefinition<T> filter)
        {
            var request = FormDistinctRequest(field, filter);
            var clientResponse =  await Client.GetAsync<DistinctResponse>(request);
            return clientResponse.Result;
        }

        public async Task<List<object>> DistinctAsync(string field, Expression<Func<T, bool>> filter)
        {
            if (filter == null) filter = _ => true;
            return await DistinctAsync(field, new ExpressionFilterDefinition<T>(filter));
        }

        public async Task<List<object>> DistinctAsync(string field)
        {
            return await DistinctAsync(field, null);
        }

        public List<object> Distinct(string field, FilterDefinition<T> filter)
        {
            var request = FormDistinctRequest(field, filter);
            var clientResponse = Client.Get<DistinctResponse>(request);
            return clientResponse.Result;
        }

        public List<object> Distinct(string field, Expression<Func<T, bool>> filter)
        {
            if (filter == null) filter = _ => true;
            return Distinct(field, new ExpressionFilterDefinition<T>(filter));
        }

        public List<object> Distinct(string field)
        {
            return Distinct(field, null);
        }
        
        private DistinctRequest FormDistinctRequest(string field, FilterDefinition<T> filter)
        {
            var request = new DistinctRequest
            {
                CollectionName = GetCollectionName(),
                Filter = filter?.FilterToJson(),
                Field = field
            };

            return request;
        }
    }
}