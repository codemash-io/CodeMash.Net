using System;
using ServiceStack;

namespace CodeMash.Interfaces
{
    public interface ICodeMashSettings
    {
        string ApiKey { get; }
        string ApiUrl { get; }
        Guid ProjectId { get; }
        IServiceClient Client { get; }
    }
}