using MongoDB.Driver;

namespace CodeMash.Interfaces
{
    public class SortingRequest : IRequestWithSorting
    {
        public string SortPropertyName { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}