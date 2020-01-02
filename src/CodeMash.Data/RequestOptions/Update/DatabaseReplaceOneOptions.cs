namespace CodeMash.Repository
{
    /// <summary>Options for inserting documents using InsertOne and InsertOneAsync methods.</summary>
    public class DatabaseReplaceOneOptions
    {
        public bool BypassDocumentValidation { get; set; }
        
        public bool IsUpsert { get; set; }
    }
}