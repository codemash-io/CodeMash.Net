using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation.Results;

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
}