using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AltBuild.BaseExtensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this IList<TI> list, int startIndex, int requestCount)
        {
            var count = Math.Min(requestCount, list.Count - startIndex);

            TO[] results = new TO[count];
            int at = 0;

            int endIndex = startIndex + count;

            for (int i = startIndex; i < endIndex; i++)
                if (list[i] is TO atValue)
                    results[at++] = atValue;

            return results;
        }

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this IList<TI> list, int startIndex) =>
            ToConvert<TO, TI>(list, startIndex, list.Count - startIndex);

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this IList<TI> list) =>
            ToConvert<TO, TI>(list, 0, list.Count - 0);

        public static void Sort<T>(this IList<T> list)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort();
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort();
                Copy(copy, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(comparison);
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort(comparison);
                Copy(copy, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, IComparer<T> comparer)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(comparer);
            }
            else
            {
                List<T> copy = new List<T>(list);
                copy.Sort(comparer);
                Copy(copy, list, 0, list.Count);
            }
        }

        public static void Sort<T>(this IList<T> list, int index, int count, IComparer<T> comparer)
        {
            if (list is List<T>)
            {
                ((List<T>)list).Sort(index, count, comparer);
            }
            else
            {
                List<T> range = new List<T>(count);
                for (int i = 0; i < count; i++)
                {
                    range.Add(list[index + i]);
                }
                range.Sort(comparer);
                Copy(range, list, index, count);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Copy<T>(IList<T> sourceList, IList<T> destineList, int destineIndex, int count)
        {
            for (int i = 0; i < count; i++)
                destineList[destineIndex + i] = sourceList[i];
        }

        public static void Swap<T>(this IList<T> list, int index1, int index2)
        {
            if (index1 >= 0 && index1 < list.Count &&
                index2 >= 0 && index2 < list.Count)
            {
                T at = list[index1];
                list[index1] = list[index2];
                list[index2] = at;
            }
        }

        public static void Swap<T>(this IList<T> list, T o1, T o2)
        {
            var index1 = list.IndexOf(o1);
            var index2 = list.IndexOf(o2);
            if (index1 >= 0 && index2 >= 0 && index1 < list.Count && index2 < list.Count)
                Swap(list, index1, index2);
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> func)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (func(item))
                    return i;

                i++;
            }

            return -1;
        }

        /// <summary>
        /// 最小値と最大値を取得する
        /// </summary>
        /// <typeparam name="TI">元のオブジェクト</typeparam>
        /// <typeparam name="TO">抽出する型</typeparam>
        /// <param name="list">元のリスト</param>
        /// <param name="func">オブジェクトから取得する型</param>
        /// <returns></returns>
        public static (TO Min, TO Max) GetMinMax<TI, TO>(this IList<TI> list, Func<TI, TO> func)
            where TO : IComparable
        {
            TO Min = default;
            TO Max = default;

            bool isFirst = true;
            foreach (var item in list)
            {
                TO at = func(item);

                if (isFirst)
                {
                    Min = Max = at;
                }
                else
                {
                    if (Min.CompareTo(at) == 1)
                        Min = at;
                    else if (Max.CompareTo(at) == -1)
                        Max = at;
                }
            }

            return (Min, Max);
        }

        public static StringBuilder ToStringBuilder<T>(this IList<T> list, string chain = null, string prefix = null, string suffix = null, StringBuilder builder = null)
        {
            if (builder == null)
                builder = new StringBuilder();

            if (list.Count > 0)
            {
                // Prefix.
                if (prefix != null)
                    builder.Append(prefix);

                // Contents.
                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0 && chain != null)
                        builder.Append(chain);

                    builder.Append(list[i].ToString());
                }

                // Suffix.
                if (suffix != null)
                    builder.Append(suffix);
            }

            return builder;
        }
    }
}
