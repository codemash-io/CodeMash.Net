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
    public partial class CodeMashRepository<T> : IRepository<T> where T : IEntity
    {
        /* Count Async */
        public Task<long> CountAsync(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {
            throw new NotImplementedException();
        }
        
        
        
        /* Count */
        public long Count(FilterDefinition<T> filter, CountOptions countOptions = null)
        {
            var request = new CountRequest{
                Filter = filter.ToJson(),
                CountOptions = countOptions,
                CollectionName = GetCollectionName(),
            };

            var response = Client.Post<CountResponse>(request);

            return response.Result;
        }

        public long Count(Expression<Func<T, bool>> filter, CountOptions countOptions = null)
        {
            return filter == null ? Count(countOptions) : Count(new ExpressionFilterDefinition<T>(filter), countOptions);
        }

        public long Count(CountOptions countOptions = null){
            return Count(FilterDefinition<T>.Empty, countOptions);
        }

        
        
        /* Distinct */
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