namespace CodeMash.Repository
{
    public class DatabaseReplaceOneResponse
    {
        public bool IsAcknowledged { get; set; }

        public bool IsModifiedCountAvailable { get; set; }

        public long MatchedCount { get; set; }

        public long ModifiedCount { get; set; }

        public string UpsertedId { get; set; }
    }
}