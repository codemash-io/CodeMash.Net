using System.Collections.Generic;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Repository
{
    public class TermsFindResponse<T>
    {
        public List<TermEntity<T>> Items { get; set; }
        
        public long TotalCount { get; set; }
    }
    
    public class TermsFindResponse : TermsFindResponse<string> {}
}