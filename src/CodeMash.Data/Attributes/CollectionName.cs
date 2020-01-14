using System;

namespace CodeMash.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Collection : Attribute
    {
        public Collection(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("CollectionName is not specified", nameof(value));
            }
            Name = value;
        }

        public virtual string Name { get; private set; }
    }
}