using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Database.Terms;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;
using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Repository
{
    public partial class CodeMashTermsService : ITermService
    {
        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, TermsFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await FindAsync<T>(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), null, findOptions);
        }

        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, TermsFindOptions findOptions = null)
        {
            return await FindAsync<T>(taxonomyName, filter, null, findOptions);
        }

        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null)
        {
            return filter == null
                ? await FindAsync(taxonomyName, null, null, sort, findOptions)
                : await FindAsync(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), null, sort, findOptions);
        }

        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, SortDefinition<TermEntity<T>> sort,
            TermsFindOptions findOptions = null)
        {
            return filter == null
                ? await FindAsync(taxonomyName, null, null, sort, findOptions)
                : await FindAsync(taxonomyName, filter, null, sort, findOptions);
        }

        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, ProjectionDefinition<T, T> projection, SortDefinition<TermEntity<T>> sort = null,
            TermsFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return await FindAsync<T>(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), projection, sort, findOptions);
        }

        public async Task<TermsFindResponse<T>> FindAsync<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, ProjectionDefinition<T, T> projection,
            SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null)
        {
            var request = FormRequest(taxonomyName, filter, projection, sort, findOptions);

            var clientResponse = await Client.GetAsync<FindTermsResponse>(request);

            return FormResponse<T>(request, clientResponse, findOptions);
        }
        
        
        
        
        

        public TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, TermsFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<T>(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), null, findOptions);
        }

        public TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, TermsFindOptions findOptions = null)
        {
            return Find<T>(taxonomyName, filter, null, findOptions);
        }

        public TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, SortDefinition<TermEntity<T>> sort,
            TermsFindOptions findOptions = null)
        {
            return filter == null
                ? Find(taxonomyName, null, null, sort, findOptions)
                : Find(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), null, sort, findOptions);
        }

        public TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, SortDefinition<TermEntity<T>> sort,
            TermsFindOptions findOptions = null)
        {
            return filter == null
                ? Find(taxonomyName, null, null, sort, findOptions)
                : Find(taxonomyName, filter, null, sort, findOptions);
        }

        public TermsFindResponse<T> Find<T>(string taxonomyName, Expression<Func<TermEntity<T>, bool>> filter, ProjectionDefinition<T, T> projection,
            SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null)
        {
            if (filter == null)
            {
                filter = _ => true;
            }

            return Find<T>(taxonomyName, new ExpressionFilterDefinition<TermEntity<T>>(filter), projection, sort, findOptions);
        }

        public TermsFindResponse<T> Find<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, ProjectionDefinition<T, T> projection,
            SortDefinition<TermEntity<T>> sort = null, TermsFindOptions findOptions = null)
        {
            var request = FormRequest(taxonomyName, filter, projection, sort, findOptions);

            var clientResponse = Client.Get<FindTermsResponse>(request);

            return FormResponse<T>(request, clientResponse, findOptions);
        }
        
        
        

        private FindTermsRequest FormRequest<T>(string taxonomyName, FilterDefinition<TermEntity<T>> filter, ProjectionDefinition<T, T> projection,
            SortDefinition<TermEntity<T>> sort, TermsFindOptions findOptions = null)
        {
            return new FindTermsRequest
            {
                CollectionName = taxonomyName,
                Filter = filter?.FilterToJson(),
                Sort = sort?.SortToJson(),
                Projection = projection?.ProjectionToJson(),
                PageSize = findOptions?.PageSize ?? 1000,
                PageNumber = findOptions?.PageNumber ?? 0,
                IncludeTaxonomy = findOptions?.IncludeTaxonomyInResponse ?? false,
                CultureCode = findOptions?.CultureCode,
                ExcludeCulture = findOptions?.ExcludeCulture ?? false,
            };
        }
        
        private TermsFindResponse<T> FormResponse<T>(FindTermsRequest request, FindTermsResponse clientResponse, TermsFindOptions findOptions = null)
        {
            if (clientResponse?.Result == null)
            {
                return new TermsFindResponse<T>()
                {
                    Result = new List<TermEntity<T>>()
                };
            }

            var parsedTerms = new List<TermEntity<T>>();
            if (clientResponse.Result != null && clientResponse.Result.Any())
            {
                var cultureCode = request.ExcludeCulture
                    ? null
                    : Client.Settings.CultureCode ?? findOptions?.CultureCode;
                
                foreach (var responseTerm in clientResponse.Result)
                {
                    var deserializedMeta = JsonConverterHelper.DeserializeEntity<T>(responseTerm.Meta, cultureCode);
                    var term = new TermEntity<T>().PopulateWith(responseTerm);
                    term.Meta = deserializedMeta;
                    parsedTerms.Add(term);
                }
            }
            
            return new TermsFindResponse<T>()
            {
                Result = parsedTerms,
                Taxonomy = clientResponse.Taxonomy,
                TotalCount = clientResponse.TotalCount
            };
        }
    }
}