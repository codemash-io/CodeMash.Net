using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.FluentValidation.Results;

namespace CodeMash
{

    [DataContract]
    public class BusinessException : Exception
    {
        public string Key { get; set; }

        [DataMember]
        public IList<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public BusinessException()
        {
        }

        public BusinessException(string message, IList<ValidationFailure> errors)
            : base(message)
        {
            Errors = errors;
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }
    }
    
    
    public class CodeMashException : Exception
    {
        public CodeMashException(Exception originalException, string messageFormatString, params object[] args)
            : base(string.Format(messageFormatString, args), originalException.GetBaseException())
        {
        }

        public CodeMashException()
        {
        }

        public CodeMashException(string message)
            : base(message)
        {
        }

        public CodeMashException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public CodeMashException(string message, string key)
            : base(message)
        {
            Key = key;
        }

        public CodeMashException(string message, Exception innerException, string key)
            : base(message, innerException)
        {
            Key = key;
        }

        public string Key { get; set; }
    }
}