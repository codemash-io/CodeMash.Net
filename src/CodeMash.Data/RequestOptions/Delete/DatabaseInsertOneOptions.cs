namespace CodeMash.Repository
{
    /// <summary>Options for deleting documents using DeleteOne and DeleteOneAsync methods.</summary>
    public class DatabaseDeleteOneOptions
    {
        public bool IgnoreTriggers { get; set; }
    }
}