namespace CodeMash.Repository
{
    /// <summary>Options for finding documents using Find and FindAsync methods.</summary>
    public class DatabaseFindOptions
    {
        /// <summary>Specified if schema should be returned together with documents.</summary>
        public bool IncludeSchemaInResponse { get; set; }
        
        /// <summary>
        /// Used to get documents in specific translation.
        /// If not culture code is set, default language from project settings is used.
        /// </summary>
        public string CultureCode { get; set; }
        
        /// <summary>Page of documents to return.</summary>
        public int? PageNumber { get; set; }
        
        /// <summary>Amount of documents to return.</summary>
        public int? PageSize { get; set; }
    }
}