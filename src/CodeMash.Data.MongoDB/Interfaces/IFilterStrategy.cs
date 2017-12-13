using CodeMash.Interfaces;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public interface IFilterStrategy<T> where T : EntityBase
    {
        FilterDefinition<T> Filter(IRequestBase request, IIdentityProvider identity);
    }
}