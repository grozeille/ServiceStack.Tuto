using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStack.Tuto.WebHost.Services
{
    public static class DateTimeEpochExtensions
    {
        public static readonly DateTime JAN_01_1970 = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc);

        // Get Unix Timestamp for given DateTime
        public static long MillisecondsFromEpoch(this DateTime date)
        {
            DateTime dt = date.ToUniversalTime();
            TimeSpan ts = dt.Subtract(JAN_01_1970);
            return (long)ts.TotalMilliseconds;
        }

        // Given Unix Timestamp, get DateTime
        public static DateTime EpochToDate(long millisecFromEpoch)
        {
            return JAN_01_1970.AddMilliseconds(millisecFromEpoch);
        }
    }
}