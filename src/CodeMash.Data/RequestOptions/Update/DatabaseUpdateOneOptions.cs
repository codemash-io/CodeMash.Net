namespace CodeMash.Repository
{
    /// <summary>Options for inserting documents using InsertOne and InsertOneAsync methods.</summary>
    public class DatabaseUpdateOneOptions
    {
        public bool BypassDocumentValidation { get; set; }
        
        public bool IgnoreTriggers { get; set; }
        
        public bool WaitForFileUpload { get; set; }
        
        public bool ResolveProviderFiles { get; set; }
    }
}