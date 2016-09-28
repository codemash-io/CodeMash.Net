using MongoDB.Driver;

namespace CodeMash.Net.DataContracts
{
    public interface IPaginationRequest
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}