namespace CodeMash.ServiceModel
{
    public interface IRequestWithPaging : IRequestBase
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}