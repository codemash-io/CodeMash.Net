using System.Collections.Generic;

namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashResponse
    {
        Dictionary<string, string> Headers { get; set; }
        
        string Method { get; set; }
        
        string ResponseUri { get; set; }
    }
}