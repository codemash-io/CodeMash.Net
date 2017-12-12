using System;

namespace CodeMash.Data.MongoDB
{
    public interface IEntityWithCreatedOn
    {
        DateTime CreatedOn { get; set; }
    }
}