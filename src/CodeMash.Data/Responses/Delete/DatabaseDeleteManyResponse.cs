namespace CodeMash.Repository
{
    public class DatabaseDeleteManyResponse
    {
        public bool IsAcknowledged { get; set; }

        public long DeletedCount { get; set; }
    }
}