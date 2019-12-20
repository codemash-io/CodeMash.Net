using System.Collections.Generic;
using CodeMash.Interfaces.Client;

namespace CodeMash.Client
{
    public class CodeMashRequest : ICodeMashRequest
    {
        public Dictionary<string, string> Headers { get; set; }
        
        public string Method { get; set; }
        
        public string RequestUri { get; set; }
        
        public string Host { get; set; }
    }
}