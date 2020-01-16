using System.Collections.Generic;

namespace CodeMash.Repository
{
    public class DatabaseFindResponse<T>
    {
        public List<T> Items { get; set; }
        
        public long TotalCount { get; set; }
    }
}