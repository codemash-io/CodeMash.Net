using CodeMash.Interfaces.Client;

namespace CodeMash.Client
{
    public class CodeMashRequestOptions : ICodeMashRequestOptions
    {
        public string BaseUrl { get; set; }
        
        public string BearerToken { get; set; }
        
        public bool UnauthenticatedRequest { get; set; }
        
        public string FileName { get; set; }
    }
}