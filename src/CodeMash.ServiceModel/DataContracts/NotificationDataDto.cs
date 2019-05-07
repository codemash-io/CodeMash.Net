using System.Collections.Generic;

namespace CodeMash.ServiceModel
{
    public class NotificationDataDto
    {
        public string Id { get; set; }
        
        public long CreatedOn { get; set; }
        
        public string Title { get; set; }
        
        public string Message { get; set; }
        
        public Dictionary<string, string> Meta { get; set; }
        
        public int TotalSuccess { get; set; }
        
        public int TotalError { get; set; }
    }
}