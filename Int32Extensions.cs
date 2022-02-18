using System;
using System.Collections.Generic;

namespace AltBuild.BaseExtensions
{
    public static class Int32Extensions
    {
        public static bool In(this int source, params int[] targets)
        {
            foreach (int at in targets)
                if (source == at)
                    return true;

            return false;
        }

        public static TimeSpan ToDaysSpan(this int source)
            => TimeSpan.FromDays(source);

        public static TimeSpan ToHoursSpan(this int source)
            => TimeSpan.FromHours(source);

        public static TimeSpan ToMinutesSpan(this int source)
            => TimeSpan.FromMinutes(source);

        public static TimeSpan ToSecondsSpan(this int source)
            => TimeSpan.FromSeconds(source);

        public static TimeSpan ToMillisecondsSpan(this int source)
            => TimeSpan.FromMilliseconds(source);

        public static int Refine(this int source, int min, int max)
        {
            if (source < min)
                return min;
            else if (source > max)
                return max;
            else
                return source;
        }

        /// <summary>
        /// int値の合計を求める
        /// </summary>
        /// <param name="array">元配列</param>
        /// <param name="indexOfBegin">開始位置</param>
        /// <param name="indexOfEnd">終了位置</param>
        /// <returns></returns>
        public static int Sum(this int[] array, int indexOfBegin, int indexOfEnd)
        {
            int result = default;

            for (int i = indexOfBegin; i < indexOfEnd; i++)
                result += array[i];

            return result;
        }

        /// <summary>
        /// int値の合計を求める
        /// </summary>
        /// <param name="array">元配列</param>
        /// <returns></returns>
        public static int Sum(this int[] array) => array.Sum(0, array.Length);
    }
}
