using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Data.MongoDB
{
    public class RequestContext<T> : IRequestContext<T> where T : EntityBase
    {
        public RequestContext() { }

        public RequestContext(RequestContextBuilder builder)
        {
            Pagining = builder.PaginationRequest;
            Sorting = builder.SortingRequest;
            IdentityProvider = builder.IdentityProvider;
            Filters = builder.Filters;
        }
        
        
        public IRequestWithPaging Pagining { get; set; }
        public IRequestWithSorting Sorting { get; set; }
        public IIdentityProvider IdentityProvider { get; set; }
        public List<FilterDefinition<T>> Filters { get; set; }
        
        
        public static RequestContextBuilder Create(IRequestBase request)  
        {
            return new RequestContextBuilder(request);
        }
        
        
        
        public class RequestContextBuilder 
        {
            protected readonly IRequestBase Request;
            public IRequestWithPaging PaginationRequest { get; set; }
            public IRequestWithSorting SortingRequest { get; set; }
            public IIdentityProvider IdentityProvider { get; set; }
            public List<FilterDefinition<T>> Filters { get; set; }
            public bool IsDeletable { get; set; }
            
            public RequestContextBuilder(IRequestBase request)
            {
                Request = request;
            }
            
            public RequestContextBuilder WithSession(AuthUserSession session)
            {
                IdentityProvider = new IdentityProvider();
                
                if (session != null)
                {
                    IdentityProvider.IsAuthenticated = session.IsAuthenticated;
                    IdentityProvider.SessionId = session.Id;
                    IdentityProvider.UserId = session.UserAuthId;
                    IdentityProvider.TenantId = session.Company;
                }
                
                return this;
            }

            public RequestContextBuilder WithPagination(IRequestWithPaging paginationRequest)
            {
                if (paginationRequest != null)
                {
                    PaginationRequest = new PaginationFromSettingsRequest(paginationRequest.PageNumber)
                    {
                        PageSize = paginationRequest.PageSize
                    };
                }
                
                return this;
            }


            public RequestContextBuilder WithSorting(IRequestWithSorting sortingRequest)
            {
                if (sortingRequest != null)
                {
                    SortingRequest = new SortingRequest
                    {
                        SortDirection = sortingRequest.SortDirection,
                        SortPropertyName = sortingRequest.SortPropertyName
                    };
                }
                return this;
            }
            
            
            public RequestContextBuilder WithFilter()
            {
                Filters = FilterBackgroundFactory.CreateFilter<T>(Request, IdentityProvider);
                return this;
            }

            public RequestContext<T> Build()
            {
                return new RequestContext<T>(this);

            }
        }

    }
}