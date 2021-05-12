using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        /* Aggregate Async */
        public async Task<List<TA>> AggregateAsync<TA>(Guid aggregateId, AggregateOptions aggregateOptions = null)
        {
            var request = FormRequest(aggregateId, aggregateOptions);
            var clientResponse = await Client.PostAsync<AggregateResponse>(request);
            return FormResponse<TA>(clientResponse);
        }
        
        
        /* Aggregate */
        public List<TA> Aggregate<TA>(Guid aggregateId, AggregateOptions aggregateOptions = null)
        {
            var request = FormRequest(aggregateId, aggregateOptions);
            var clientResponse = Client.Post<AggregateResponse>(request);
            return FormResponse<TA>(clientResponse);
        }

        private AggregateRequest FormRequest(Guid aggregateId, AggregateOptions aggregateOptions = null)
        {
            return new AggregateRequest()
            {
                CollectionName = GetCollectionName(),
                Id = aggregateId,
                Tokens = aggregateOptions?.Tokens,
                Cluster = Cluster
            };
        }
        
        private List<TA> FormResponse<TA>(AggregateResponse response)
        {
            if (response?.Result == null)
            {
                return new List<TA>();
            }

            return JsonConverterHelper.DeserializeAggregate<List<TA>>(response.Result);
        }
    }
}