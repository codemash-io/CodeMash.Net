using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public interface IFilterStrategy<T> where T : Entity
    {
        FilterDefinition<T> Filter(IRequestBase request, IIdentityProvider identity);
    }
}