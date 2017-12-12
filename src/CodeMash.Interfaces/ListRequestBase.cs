using MongoDB.Driver;
using ServiceStack;

namespace CodeMash.Interfaces
{
    public class ListRequestBase : RequestBase, IRequestWithPaging, IRequestWithSorting
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortPropertyName { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}