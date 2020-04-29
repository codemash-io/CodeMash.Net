namespace CodeMash.Exceptions
{
    public class ValidationError
    {
        public string ErrorCode { get; set; }

        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}