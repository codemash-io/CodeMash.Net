using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    class FilterByNameStrategy<T> : IFilterStrategy<T> where T : Entity 
    {
        public FilterDefinition<T> Filter(IRequestBase request, IIdentityProvider identityProvider)
        {
            if (request is IRequestWithFilterByName requestWithName)
            {
                return typeof(IEntityWithName).IsAssignableFrom(typeof(T)) 
                    ? Builders<T>.Filter.Regex("Name", new BsonRegularExpression(requestWithName.Name, "i")) 
                    : null;
                
            }
            
            return null;
        }
    }
}