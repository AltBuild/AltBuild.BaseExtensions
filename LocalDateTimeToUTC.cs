using System;
using System.Collections.Generic;
using System.Text;

namespace AltBuild.BaseExtensions
{
    public static class LocalDateTimeToUTCExtension
    {
        /// <summary>
        ///  結果: 例："2020-10-12T11:54:47+09:00"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToLocalUTC1(this DateTime dateTime)
        {
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            DateTimeOffset dto = dateTime;

            return dto.ToString("yyyy-MM-ddTHH:mm:sszzz");
        }
    }
}
