using System;

namespace CodeMash.Net
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionName : Attribute
    {
        public CollectionName(string value)
        {

#if NET35
            if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
#else
            if (string.IsNullOrWhiteSpace(value))
#endif
                throw new ArgumentException("Empty CollectionName not allowed", nameof(value));

            Name = value;
        }

        public virtual string Name { get; private set; }
    }
}
