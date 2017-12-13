using CodeMash.Interfaces;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    class FilterByDeletableStrategy<T> : IFilterStrategy<T> where T : EntityBase
    {
        public FilterDefinition<T> Filter(IRequestBase request, IIdentityProvider identityProvider)
        {
            return typeof(IEntityWithIsDeleted).IsAssignableFrom(typeof(T)) 
                ? Builders<T>.Filter.Eq("IsDeleted", false) 
                : null;
        }
    }
}