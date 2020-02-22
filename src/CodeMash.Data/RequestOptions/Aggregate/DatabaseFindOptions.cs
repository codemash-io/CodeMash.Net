using System.Collections.Generic;

namespace CodeMash.Repository
{
    public class AggregateOptions
    {
        /// <summary>
        /// Tokens that will be injected into aggregate document.
        /// </summary>
        public Dictionary<string, string> Tokens { get; set; }
    }
}