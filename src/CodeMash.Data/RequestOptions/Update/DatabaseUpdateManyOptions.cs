namespace CodeMash.Repository
{
    /// <summary>Options for inserting documents using InsertOne and InsertOneAsync methods.</summary>
    public class DatabaseUpdateManyOptions
    {
        public bool BypassDocumentValidation { get; set; }
        
        public bool IgnoreTriggers { get; set; }
    }
}