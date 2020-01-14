using System.Collections.Generic;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Repository
{
    public class DatabaseFindResponse<T>
    {
        public List<T> List { get; set; }
        
        public long TotalCount { get; set; }
    }
}