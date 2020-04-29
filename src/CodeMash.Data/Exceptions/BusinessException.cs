using System;
using System.Collections.Generic;

namespace CodeMash.Exceptions
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; set; }

        public int StatusCode { get; set; }

        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        public BusinessException()
        {
        }

        public BusinessException(string message, List<ValidationError> errors)
            : base(message)
        {
            this.Errors = errors;
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}