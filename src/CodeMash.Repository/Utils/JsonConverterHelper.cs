using System;
using System.Text.RegularExpressions;
using Isidos.CodeMash.Utils;
using Newtonsoft.Json;

namespace CodeMash.Repository
{
    public static class JsonConverterHelper
    {
        public static string SerializeEntity<T>(T serializableObject)
        {
            return serializableObject == null ? null : JsonConvert.SerializeObject(serializableObject, new EntityConverter<T>(null));
        }
        
        public static T DeserializeEntity<T>(string json, string cultureCode)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(json, new EntityConverter<T>(cultureCode));
        }
        
        public static T DeserializeAggregate<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(json, new AggregateConverter<T>());
        }
        
        public static string ReplaceIsoDateToLong(string input, bool addNumberLong = false)
        {
            var isoDatePattern = "(new )?(ISODate\\((\"|\'))(\\d{4}-[01]\\d-[0-3]\\dT[0-2]\\d:[0-5]\\d:[0-5]\\d(\\.\\d+)?([+-][0-2]\\d:[0-5]\\d|Z))((\"|\')\\))";
            var matches = Regex.Matches(input, isoDatePattern);

            var newString = input;
            foreach (Match match in matches)
            {
                var dateValueString = match.Groups[4].Value;
                var dateTime = DateTime.Parse(dateValueString).ToUniversalTime();

                var dateLong = addNumberLong
                    ? $"NumberLong({DateTimeHelpers.DateTimeToUnixTimestamp(dateTime)})"
                    : $"{DateTimeHelpers.DateTimeToUnixTimestamp(dateTime)}";
                newString = newString.Replace(match.Value, dateLong);
            }

            return newString;
        }
    }
}