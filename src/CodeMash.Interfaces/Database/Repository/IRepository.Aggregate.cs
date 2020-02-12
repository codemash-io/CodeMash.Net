using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Models;
using AggregateOptions = CodeMash.Repository.AggregateOptions;

namespace CodeMash.Interfaces.Database.Repository
{
    public partial interface IRepository<T> where T : IEntity
    {
        /* Aggregate Async */
        Task<List<TA>> AggregateAsync<TA>(Guid aggregateId, AggregateOptions aggregateOptions = null);
        
        
        
        /* Aggregate */
        List<TA> Aggregate<TA>(Guid aggregateId, AggregateOptions aggregateOptions = null);
    }
}