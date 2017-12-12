using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public class Filter<T> where T : Entity
    {
        private readonly IFilterStrategy<T> _strategy;
        
        public Filter(IFilterStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public FilterDefinition<T> GetFilter(IRequestBase request, IIdentityProvider identity)
        {
            return _strategy.Filter(request, identity);
        }
    }
}