using System.Collections.Generic;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;

namespace CodeMash.Interfaces
{
    public interface IRequestContext<T>
    {
        IRequestWithPaging Pagining { get; set; }
        IRequestWithSorting Sorting { get; set; }
        IIdentityProvider IdentityProvider { get; set; }
        List<FilterDefinition<T>> Filters { get; set; }
    }
}