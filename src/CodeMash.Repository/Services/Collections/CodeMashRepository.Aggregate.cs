using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;
using MongoDB.Driver;

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