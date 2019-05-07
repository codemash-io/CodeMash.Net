using System.Runtime.Serialization;
using MongoDB.Driver;

namespace CodeMash.ServiceModel 
{
    [DataContract]
    public class ListRequestBase : RequestBase, IRequestWithPaging, IRequestWithSorting
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortPropertyName { get; set; }
        public SortDirection SortDirection { get; set; }
        public int Skip => PageNumber * PageSize;
    }
}