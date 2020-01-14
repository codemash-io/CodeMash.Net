using System;
using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.Core.Tests
{
    [Collection("sdk-datetime")]
    public class DateTimeEntity : Entity
    {
        public DateTime DateTimeField { get; set; }
    }
}