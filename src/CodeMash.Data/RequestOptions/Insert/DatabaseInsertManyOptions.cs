namespace CodeMash.Repository
{
    /// <summary>Options for inserting documents using InsertOne and InsertOneAsync methods.</summary>
    public class DatabaseInsertManyOptions
    {
        public bool BypassDocumentValidation { get; set; }
    }
}