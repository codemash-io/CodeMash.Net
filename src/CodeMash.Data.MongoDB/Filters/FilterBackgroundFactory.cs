using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Interfaces.IAM;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public static class FilterBackgroundFactory
    {
        public static List<FilterDefinition<T>> CreateFilter<T>(IRequestBase request, IIdentityProvider identityProvider) where T : Entity
        {
            var filterList = new List<FilterDefinition<T>>();

            void ApplyFilter(Filter<T> filterInstance, IRequestBase requestInstance, IIdentityProvider identity)
            {
                var filter = filterInstance.GetFilter(requestInstance, identity);
                if (filter != null)
                {
                    filterList.Add(filter);
                }
            }

            ApplyFilter(new Filter<T>(new FilterByDeletableStrategy<T>()), request, identityProvider);
            ApplyFilter(new Filter<T>(new FilterByNameStrategy<T>()), request, identityProvider);
            ApplyFilter(new Filter<T>(new FilterByCompanyStrategy<T>()), request, identityProvider);

            return filterList;
        }
    }
}