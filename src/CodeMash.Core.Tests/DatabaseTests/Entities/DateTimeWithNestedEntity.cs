using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-datetime-nested")]
    public class DateTimeWithNestedEntity : Entity
    {
        public DateTime NonNested { get; set; }
        
        public List<DateTimeEntity> NestedDateTime { get; set; }
    }
}