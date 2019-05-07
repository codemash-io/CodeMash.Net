using System.Collections.Generic;
using CodeMash.Interfaces;
using MongoDB.Driver;

namespace CodeMash.ServiceModel
{
    public interface IRequestContext<T>
    {
        IRequestWithPaging Pagining { get; set; }
        IRequestWithSorting Sorting { get; set; }
        IIdentityProvider IdentityProvider { get; set; }
        List<FilterDefinition<T>> Filters { get; set; }
    }
}