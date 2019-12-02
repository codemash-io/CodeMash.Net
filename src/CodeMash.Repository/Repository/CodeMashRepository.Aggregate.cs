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
        /* Aggregate Async */
        public Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation,
            AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }
        
        
        /* Aggregate */
        public List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions)
        {
            throw new NotImplementedException();
        }
    }
}