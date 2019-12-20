using System.Collections.Generic;
using CodeMash.Interfaces.Client;

namespace CodeMash.Client
{
    public class CodeMashResponse : ICodeMashResponse
    {
        public Dictionary<string, string> Headers { get; set; }
        
        public string Method { get; set; }
        
        public string ResponseUri { get; set; }
    }
}