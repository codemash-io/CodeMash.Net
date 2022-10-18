using System;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-datetime")]
    public class DateTimeEntity : Entity
    {
        public DateTime DateTimeField { get; set; }
    }
    
    public class DateTimeNonEntity
    {
        public DateTime DateTimeField { get; set; }
    }
}