using System.Collections.Generic;

namespace CodeMash.Repository
{
    /// <summary>Options for finding documents using Find and FindAsync methods.</summary>
    public class DatabaseFindOptions
    {
        /// <summary>
        /// Used to get documents in specific translation.
        /// If not culture code is set, default language from project settings is used.
        /// </summary>
        public string CultureCode { get; set; }
        
        /// <summary>Page of documents to return.</summary>
        public int? PageNumber { get; set; }
        
        /// <summary>Amount of documents to return.</summary>
        public int? PageSize { get; set; }
        
        public bool AddReferencesFirst { get; set; }
        
        public List<CollectionReferenceField> ReferencedFields { get; set; }
        
        public bool IncludeUserNames { get; set; }
        
        public bool IncludeRoleNames { get; set; }
        
        public bool IncludeCollectionNames { get; set; }
        
        public bool IncludeTermNames { get; set; }
        
        public bool ExcludeCulture { get; set; }
    }
}