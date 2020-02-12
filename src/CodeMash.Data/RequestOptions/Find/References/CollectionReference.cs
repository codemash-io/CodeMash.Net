using System.Collections.Generic;

namespace CodeMash.Repository
{
    public class CollectionReference
    {
        public string CollectionName { get; set; }
        
        public List<CollectionReferenceField<T>> Fields { get; set; }
    }
}