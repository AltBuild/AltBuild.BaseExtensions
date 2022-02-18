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
        /// 日時の古い（小さい）方を返す
        /// 但し DateTime.MinValue (default) は除く
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static bool MinWithoutDefault(DateTime dateTime1, DateTime dateTime2, out DateTime result)
        {
            if (dateTime1 != default)
            {
                if (dateTime2 != default)
                {
                    if (dateTime1 < dateTime2)
                    {
                        result = dateTime1;
                    }
                    else
                    {
                        result = dateTime2;
                    }
                }
                else
                {
                    result = dateTime1;
                }

                return true;
            }
            else if (dateTime2 != default)
            {
                result = dateTime2;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// 日時の新しい（大きい）方を返す
        /// 但し DateTime.MinValue (default) は除く
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static bool MaxWithoutDefault(DateTime dateTime1, DateTime dateTime2, out DateTime result)
        {
            if (dateTime1 != default)
            {
                if (dateTime2 != default)
                {
                    if (dateTime1 > dateTime2)
                    {
                        result = dateTime1;
                    }
                    else
                    {
                        result = dateTime2;
                    }
                }
                else
                {
                    result = dateTime1;
                }

                return true;
            }
            else if (dateTime2 != default)
            {
                result = dateTime2;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// 年齢や社歴など入力情報が適切なら true を返す
        /// </summary>
        /// <param name="date">評価値</param>
        /// <param name="years">前後の範囲年数</param>
        /// <param name="ifDefault">dateがデフォルト値の場合の戻り値</param>
        /// <returns></returns>
        public static bool IsEnableCustomary(this DateTime date, int years = 500, bool ifDefault = false)
        {
            // デフォルト値の場合
            if (date == default)
                return ifDefault;

            // 計算
            var now = DateTime.Now;
            return (date > now.AddYears(-years) && date < now.AddYears(years));
        }

        /// <summary>
        /// 時分秒を追加する
        /// </summary>
        /// <param name="date">元の日時</param>
        /// <param name="hours">時間</param>
        /// <param name="minutes">分</param>
        /// <param name="seconds">秒</param>
        public static DateTime AddTime(this DateTime date, int hours, int minutes, int seconds)
            => date.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);

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
        /// 年度計算の時の 年度（Year）を取得する
        /// </summary>
        /// <param name="date">元の年月日</param>
        /// <param name="month">年度の開始月</param>
        /// <returns></returns>
        public static int FiscalYear(this DateTime date, int month)
        {
            if (month < 1 || month > 12)
                throw new InvalidOperationException($"年度計算の際の 次年度開始月（Month = {month}）の値が不正です。");

            if (date.Month < month)
                return date.Year - 1;
            else
                return date.Year;
        }

        /// <summary>
        /// 年度初めのDateTimeを返す
        /// </summary>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime FiscalDate(this DateTime date, int month)
        {
            if (month < 1 || month > 12)
                throw new InvalidOperationException($"年度計算の際の 次年度開始月（Month = {month}）の値が不正です。");

            int fYear = (date.Month < month) ? date.Year - 1 : date.Year;

            return new DateTime(fYear, month, 1);
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
        /// 時刻部（Hours, Minutes, Seconds）のみ秒単位で返す
        /// （秒未満と日付部は参照しない）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeInSeconds(this DateTime time) => time.Hour * 60 * 60 + time.Minute * 60 + time.Second;

        /// <summary>
        /// 時刻部（Hours, Minutes）のみ分単位で返す
        /// （秒以下と日付部は参照しない）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int TimeInMinutes(this DateTime time) => time.Hour * 60 + time.Minute;

        /// <summary>
        /// 分未満をクリアする
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime RoundMinites(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);

        /// <summary>
        /// 分未満をクリアする
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime RoundSeconds(this DateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

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

        /// <summary>
        /// 年 が一致するか？
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合は true </returns>
        public static bool EqualsYear(this DateTime date, DateTime other) =>
            (date.Year == other.Year);

        /// <summary>
        /// 年 と 月 が一致するか？
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合は true </returns>

        public static bool EqualsYearMonth(this DateTime date, DateTime other) =>
            (date.Month == other.Month && date.Year == other.Year);

        /// <summary>
        /// 年月日が一致するか？
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合は true </returns>

        public static bool EqualsYearMonthDay(this DateTime date, DateTime other) =>
            (date.Day == other.Day && date.Month == other.Month && date.Year == other.Year);

        /// <summary>
        /// 月 と 日 が一致するか？
        /// </summary>
        /// <param name="date">日付</param>
        /// <param name="other">比較対象</param>
        /// <returns>一致する場合は true </returns>

        public static bool EqualsMonthDay(this DateTime date, DateTime other) =>
            (date.Day == other.Day && date.Month == other.Month);

        /// <summary>
        /// 生年月日から年齢を計算する
        /// </summary>
        /// <param name="birthDate">生年月日</param>
        /// <param name="today">現在の日付</param>
        /// <returns>年齢</returns>
        public static int GetAge(this DateTime birthDate, DateTime today)
        {
            // 年齢
            int age = today.Year - birthDate.Year;

            // 現在の日付から年齢を引いた日付が誕生日より前ならば、1引く
            if (today.AddYears(-age) < birthDate)
                age--;

            // 結果
            return age;
        }

        /// <summary>
        /// 生年月日から現在の年齢を計算する
        /// </summary>
        /// <param name="birthDate">生年月日</param>
        /// <param name="today">現在の日付</param>
        /// <returns>年齢</returns>
        public static int GetAge(this DateTime birthDate) => birthDate.GetAge(DateTime.Now);

        public static int ToYearMonth(this DateTime dateTime)
        {
            return dateTime.Year * 100 + dateTime.Month;
        }

        public static int ToYearMonthDay(this DateTime dateTime)
        {
            return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
        }

        public static int ToHourMinute(this DateTime time)
        {
            return time.Hour * 100 + time.Minute;
        }

        public static int ToHourMinuteSecond(this DateTime time)
        {
            return time.Hour * 10000 + time.Minute * 100 + time.Second;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int ToMonthDay(this DateTime dateTime)
        {
            return dateTime.Month * 100 + dateTime.Day;
        }

        /// <summary>
        /// 論理月数を取得する（Year * 12 + Month)
        /// 月数の加減算等で利用
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int Months(this DateTime dateTime) => dateTime.Year * 12 + dateTime.Month;

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

            catch (Exception ex)
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
