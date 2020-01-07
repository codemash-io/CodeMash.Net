using System;
using System.Globalization;

namespace Isidos.CodeMash.Utils
{
    public static class DateTimeHelpers
    {
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (long)(TimeZoneInfo.ConvertTimeToUtc(dateTime) - 
                    new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        
        public static long? DateTimeToUnixTimestamp(DateTime? dateTime)
        {
            if (dateTime == null) return null;
            return DateTimeToUnixTimestamp(dateTime.Value);
        }
        
        public static DateTime DateTimeFromUnixTimestamp(long timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timeStamp);
        }
        
        public static DateTime? DateTimeFromUnixTimestamp(long? timeStamp)
        {
            if (timeStamp == null) return null;
            return DateTimeFromUnixTimestamp(timeStamp.Value);
        }
    }
}