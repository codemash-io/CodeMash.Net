using System;
using System.Collections.Generic;
using CodeMash.Models;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-datetime-nested")]
    public class DateTimeWithNestedEntity : Entity
    {
        public DateTime NonNested { get; set; }
        
        public DateTime NonNested2 { get; set; }
        
        public List<DateTimeNonEntity> NestedDateTime { get; set; }
    }
}