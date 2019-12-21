namespace CodeMash.Repository
{
    public class DatabaseDeleteOneResponse
    {
        public bool IsAcknowledged { get; set; }

        public long DeletedCount { get; set; }
    }
}