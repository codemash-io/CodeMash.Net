namespace CodeMash.Repository
{
    public static class StringExtensions
    {
        public static string ToLowerCaseFirstLetter(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            if (value.Length == 1) return value.ToLower();
            
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }
    }
}