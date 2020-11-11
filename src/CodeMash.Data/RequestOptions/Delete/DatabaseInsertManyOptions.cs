using System;

namespace CodeMash.Repository
{
    /// <summary>Options for deleting documents using DeleteMany and DeleteManyAsync methods.</summary>
    public class DatabaseDeleteManyOptions
    {
        public bool IgnoreTriggers { get; set; }
    }
}