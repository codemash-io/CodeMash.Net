using System;

namespace CodeMash
{
    public class Log 
    {
        public string CustomerId { get; set; }
        public string ProjectId { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public bool IsDebug { get; set; }
    }
}
