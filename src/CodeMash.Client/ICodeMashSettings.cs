using System;
using ServiceStack;

namespace CodeMash.Client
{
    public interface ICodeMashSettings
    {
        IServiceClient Client { get; }
        
        string ApiKey { get; }

        Guid ProjectId { get; }
    }
}