using MongoDB.Driver;

namespace CodeMash.ServiceModel
{
    public interface IRequestWithSorting : IRequestBase
    {
        string SortPropertyName { get; set; }
        SortDirection SortDirection { get; set; }
    }
}