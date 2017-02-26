using System;

namespace CodeMash.Data.MongoDB
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionName : Attribute
    {
        public CollectionName(string value)
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