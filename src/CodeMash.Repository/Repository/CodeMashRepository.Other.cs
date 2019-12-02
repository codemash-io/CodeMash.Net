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
            var request = new Count{
                Filter = filter.ToJson(),
                CountOptions = countOptions,
                CollectionName = GetCollectionName(),
                ProjectId = Settings.ProjectId,
                CultureCode = CultureInfo.CurrentCulture.Name
            };

            var response = Client.Post(request);

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