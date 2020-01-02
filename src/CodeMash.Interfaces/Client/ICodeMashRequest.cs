using System.Collections.Generic;

namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashRequest
    {
        Dictionary<string, string> Headers { get; set; }
        
        string Method { get; set; }
        
        string RequestUri { get; set; }
        
        string Host { get; set; }
    }
}