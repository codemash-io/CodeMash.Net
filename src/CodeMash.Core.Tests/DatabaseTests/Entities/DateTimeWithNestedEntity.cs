using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [CollectionName("sdk-datetime-nested")]
    public class DateTimeWithNestedEntity : Entity
    {
        [FieldName("nonNested")]
        public DateTime NonNested { get; set; }
        
        [FieldName("nestedDateTime")]
        public List<DateTimeEntity> NestedDateTime { get; set; }
    }
}