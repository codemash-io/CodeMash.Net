namespace CodeMash.Repository
{
    /// <summary>Options for counting documents using Count and CountAsync methods.</summary>
    public class DatabaseCountOptions
    {
        public int? Limit { get; set; }
        
        public int? Skip { get; set; }
    }
}