using System;
using System.Globalization;
using System.Collections.Generic;

namespace AltBuild.BaseExtensions
{
    public static class DateTimeExtensions
    {
        static Dictionary<string, IFormatProvider> FormatProvider { get; } = CreateFormatProvider();

        /// <summary>
        /// 国・言語により、DateTime.ToString を処理する
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, IFormatProvider> CreateFormatProvider()
        {
            var results = new Dictionary<string, IFormatProvider>();

            // 日本／日本語
            var culture = new CultureInfo("ja-JP", true);
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            results.Add("JPN", culture);

            return results;
        }

        public static void Swap(ref DateTime dateTime1, ref DateTime dateTime2)
        {
            var dateTime0 = dateTime1;
            dateTime1 = dateTime2;
            dateTime2 = dateTime0;
        }


        /// <summary>
        /// 指定のインターバルで切り下げする
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static DateTime RoundDown(this DateTime dateTime, TimeSpan interval)
            => new DateTime((((dateTime.Ticks + interval.Ticks) / interval.Ticks) - 1) * interval.Ticks, dateTime.Kind);

        /// <summary>
        /// 指定のインターバルで切り下げする
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static DateTime RoundDown(this DateTime dateTime, int seconds)
            => RoundDown(dateTime, TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// 指定のインターバルで切り上げする
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static DateTime RoundUp(this DateTime dateTime, TimeSpan interval)
            => new DateTime(((dateTime.Ticks + interval.Ticks - 1) / interval.Ticks) * interval.Ticks, dateTime.Kind);

        /// <summary>
        /// 指定のインターバルで切り上げする
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static DateTime RoundUp(this DateTime dateTime, int seconds)
            => RoundUp(dateTime, TimeSpan.FromSeconds(seconds));

        /// <summary>
        /// 大きい日時の方を返す
        /// </summary>
        /// <param name="dt1">日時１</param>
        /// <param name="dt2">日時２</param>
        /// <returns></returns>
        public static DateTime Max(DateTime dateTime1, DateTime dateTime2) => (dateTime1 > dateTime2 ? dateTime1 : dateTime2);

        /// <summary>
        /// 小さい日時の方を返す
        /// </summary>
        /// <param name="dt1">日時１</param>
        /// <param name="dt2">日時２</param>
        /// <returns></returns>
        public static DateTime Min(DateTime dateTime1, DateTime dateTime2) => (dateTime1 > dateTime2 ? dateTime2 : dateTime1);

        /// <summary>
        /// date が begin ～ end の範囲内に収まる様に調整する
        /// </summary>
        /// <param name="date">元日付</param>
        /// <param name="beginDate">開始日付</param>
        /// <param name="endDate">終了日付</param>
        /// <returns></returns>
        public static DateTime FitInRange(this DateTime date, DateTime beginDate, DateTime endDate)
        {
            if (beginDate > date)
                date = beginDate;
            else if (endDate < date)
                date = endDate;

            return date;
        }

        /// <summary>
        /// 月数計算
        /// </summary>
        /// <param name="date"></param>
        /// <param name="baseDate"></param>
        /// <returns></returns>
        public static int ElapsedMonths(this DateTime date, DateTime baseDate) => (date.Year - baseDate.Year) * 12 + date.Month - baseDate.Month;

        /// <summary>
        /// 当月初めを返す
        /// </summary>
        public static DateTime BeginningOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);

        /// <summary>
        /// 月末を返す
        /// </summary>
        public static DateTime EndOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1).AddMonths(1).AddTicks(-1);

        /// <summary>
        /// 月の日数を返す
        /// </summary>
        /// <param name="date">年月</param>
        /// <returns>日数</returns>
        public static int DaysInMonth(this DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);

        /// <summary>
        /// その日は当月の何週目か？
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeeksOfMonth(this DateTime date) => 1 + (date.Day - 1) / 7;

        /// <summary>
        /// 指定の時分秒が一致するか？
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool EqualsTime(this DateTime date, int hour, int minute, int second) =>
            (date.Hour == hour && date.Minute == minute && date.Second == second);

        public static DateTime SetTime(this DateTime date, int hours, int minutes, int seconds) =>
            date.Date.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);

        public static DateTime SetTime(this DateTime date, DateTime time) =>
            date.Date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second);

        public static DateTime SetDate(this DateTime time, DateTime date) =>
            date.Date.AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second);

        public static DateTime SetDate(this DateTime time, int year, int month, int day) =>
            new DateTime(year, month, day).AddHours(time.Hour).AddMinutes(time.Minute).AddSeconds(time.Second);

        public static string ToString(this DateTime dateTime, string format, string code3)
        {
            try
            {
                if (dateTime == default)
                    return "";

                else if (FormatProvider.TryGetValue(code3, out IFormatProvider formatProvider))
                    return dateTime.ToString(format, formatProvider);

                else
                    return dateTime.ToString(format);
            }

            catch (Exception)
            {
                return "";
            }
        }

        public static bool TryStringToFormatProvider(string code3, out IFormatProvider formatProvider)
        {
            return FormatProvider.TryGetValue(code3, out formatProvider);
        }
    }
}
