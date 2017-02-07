using System;

namespace CodeMash
{
    [Serializable]
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