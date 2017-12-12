namespace CodeMash.Interfaces
{
    public interface ICultureBasedRequest : IRequestBase
    {
        string CultureCode { get; set; }
    }
}