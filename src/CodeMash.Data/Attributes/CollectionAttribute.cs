using System;

namespace CodeMash.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionAttribute : Attribute
    {
        public CollectionAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Collection name is not specified", nameof(value));
            }
            Name = value;
        }

        public virtual string Name { get; private set; }
    }
}