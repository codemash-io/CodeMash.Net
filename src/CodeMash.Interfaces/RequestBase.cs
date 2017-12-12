namespace CodeMash.Interfaces
{
    public class RequestBase : IRequestBase, ICultureBasedRequest
    {
        public string CultureCode { get; set; }

    }
}