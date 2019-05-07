namespace CodeMash.ServiceModel
{
    public interface ICultureBasedRequest : IRequestBase
    {
        string CultureCode { get; set; }
    }
}