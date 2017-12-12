using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    class FilterByCompanyStrategy<T> : IFilterStrategy<T> where T : Entity 
    {
        public FilterDefinition<T> Filter(IRequestBase request, IIdentityProvider identityProvider)
        {
            if (identityProvider.IsAuthenticated)
            {
                return Builders<T>.Filter.Eq("TenantId", identityProvider.TenantId);
            }
            
            return null;
        }
    }
}