using CodeMash.Interfaces;
using MongoDB.Driver;

namespace CodeMash.ServiceModel
{
    public class SortingRequest : IRequestWithSorting
    {
        public string SortPropertyName { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}