using System.Collections.Generic;
using CodeMash.Models;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Repository
{
    public class TermsFindResponse<T>
    {
        public List<TermEntity<T>> Result { get; set; }
        
        public long TotalCount { get; set; }
        
        public Taxonomy Taxonomy { get; set; }
    }
}