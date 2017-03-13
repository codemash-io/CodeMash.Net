namespace CodeMash.Interfaces
{
    public interface IPaginationRequest
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}