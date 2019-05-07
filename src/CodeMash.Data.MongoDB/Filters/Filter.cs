using CodeMash.Interfaces;
using CodeMash.ServiceModel;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public class Filter<T> where T : EntityBase
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