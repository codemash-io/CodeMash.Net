namespace CodeMash.Repository
{
    /// <summary>Options for finding documents using Find and FindAsync methods.</summary>
    public class DatabaseFindOneOptions
    {
        /// <summary>Specified if schema should be returned together with documents.</summary>
        public bool IncludeSchema { get; set; }
        
        /// <summary>
        /// Used to get documents in specific translation.
        /// If not culture code is set, default language from project settings is used.
        /// </summary>
        public string CultureCode { get; set; }
        
        public bool ExcludeCulture { get; set; }
    }
}