namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashRequestOptions
    {
        string BearerToken { get; set; }
        
        bool UnauthenticatedRequest { get; set; }
        
        string FileName { get; set; }
    }
}