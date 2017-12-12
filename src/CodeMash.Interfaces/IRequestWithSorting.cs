using MongoDB.Driver;

namespace CodeMash.Interfaces
{
    public interface IRequestWithSorting : IRequestBase
    {
        string SortPropertyName { get; set; }
        SortDirection SortDirection { get; set; }
    }
}