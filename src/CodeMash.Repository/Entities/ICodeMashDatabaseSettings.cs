using System;
using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Common;
using ServiceStack;

namespace CodeMash.Repository
{
    public interface ICodeMashDatabaseSettings
    {
        IServiceClient Client { get; }
        
        string ApiKey { get; }

        Guid ProjectId { get; }

        string Region { get; }
    }
}