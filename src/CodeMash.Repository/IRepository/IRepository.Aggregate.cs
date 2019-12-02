using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Aggregate Async */
        Task<List<TA>> AggregateAsync<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions);
        
        
        
        /* Aggregate */
        List<TA> Aggregate<TA>(PipelineDefinition<T, TA> aggregation, AggregateOptions aggregateOptions);
    }
}