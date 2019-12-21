using System.Collections.Generic;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Repository
{
    public class DatabaseFindResponse<T>
    {
        public List<T> Result { get; set; }
        
        public Schema Schema { get; set; }
    }
}