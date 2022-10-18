using System;

namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashSettings
    {
        string ApiBaseUrl { get; }
        string SecretKey { get; }

        Guid ProjectId { get; }
        
        string CultureCode { get; set; }
        
        string Version { get; set; }
    }
}