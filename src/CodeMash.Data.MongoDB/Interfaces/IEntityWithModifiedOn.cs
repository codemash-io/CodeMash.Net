using System;

namespace CodeMash.Data.MongoDB
{
    public interface IEntityWithModifiedOn
    {
        DateTime ModifiedOn { get; set; }
    }
}