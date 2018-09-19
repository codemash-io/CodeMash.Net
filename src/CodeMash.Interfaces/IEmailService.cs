using System;
using System.Collections.Generic;

namespace CodeMash.Interfaces
{
    public interface IEmailService
    {
        bool SendMail(string[] recipients, string templateName, Dictionary<string, object> tokens, Guid? accountId);
    }
}