using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace AltBuild.BaseExtensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// 同じアイテムが複数入らない様にリストを再生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> ToUnique<T>(this List<T> list)
        {
            var results = new List<T>();
            foreach (var item in list)
                if (results.Contains(item) == false)
                    results.Add(item);

            return results;
        }

        /// <summary>
        /// リストから該当するアイテムを削除する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        public static void Exclude<T>(this List<T> list, Predicate<T> predicate)
        {
            for (int i = list.Count - 1; i >= 0; --i)
                if (predicate(list[i]))
                    list.RemoveAt(i);
        }

        /// <summary>
        /// リストから該当するアイテムを削除する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        public static bool Exclude<T>(this List<T> list, T value) =>
            list.Remove(value);

        /// <summary>
        /// リストから該当するリストを削除する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ExcludeRange<T>(this List<T> list, IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
                list.Remove(item);
        }

        /// <summary>
        /// 指定の条件のアイテムを含んでいるか？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool Contains<T>(this List<T> list, Predicate<T> predicate)
        {
            foreach (var item in list)
                if (predicate(item))
                    return true;

            return false;
        }

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this List<T> list, int startIndex, int requestCount)
        {
            var count = Math.Min(requestCount, list.Count - startIndex);

            T[] results = new T[count];
            int at = 0;

            int endIndex = startIndex + count;

            for (int i = startIndex; i < endIndex; i++)
                results[at++] = list[i];

            return results;
        }

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this List<T> list, int startIndex) =>
            list.ToArray(startIndex, list.Count - startIndex);

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static T[] ToArray<T>(this List<T> list) =>
            list.ToArray(0, list.Count - 0);


        /// <summary>
        /// 条件に合致するアイテムを１つ探す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetValue<T>(this List<T> list, Predicate<T> match, out T value)
        {
            return (value = list.Find(match)) != null;
        }

        /// <summary>
        /// 指定の条件に合致するオブジェクトを入替
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <param name="newItem"></param>
        public static void Replace<T>(this List<T> list, Predicate<T> match, T newItem)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (match(list[i]))
                {
                    list[i] = newItem;
                    break;
                }
            }
        }

        /// <summary>
        /// 条件に合致するリストを削除する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        public static void Remove<T>(this List<T> list, Predicate<T> match)
        {
            for (int i = list.Count -1; i >= 0; --i)
                if (match(list[i]))
                    list.RemoveAt(i);
        }

        /// <summary>
        /// 指定位置のアイテムを１段あげる
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>成功=true, 失敗=false</returns>
        public static bool Sortup<T>(this List<T> list, int index, Func<T, T, bool> where = null)
        {
            if (list.Count > index && index > 0)
            {
                // 条件
                if (where == null || where(list[index], list[index - 1]))
                {
                    var dammy = list[index];
                    list[index] = list[index - 1];
                    list[index - 1] = dammy;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 指定位置のアイテムを１段下げる
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <returns>成功=true, 失敗=false</returns>
        public static bool Sortdown<T>(this List<T> list, int index, Func<T, T, bool> where = null)
        {
            if (list.Count -1 > index && index >= 0)
            {
                // 条件
                if (where == null || where(list[index], list[index + 1]))
                {
                    var dammy = list[index];
                    list[index] = list[index + 1];
                    list[index + 1] = dammy;
                    return true;
                }
            }
            return false;
        }

        public static void MoveAt<T>(this List<T> list, int sourceIndex, int destineIndex)
        {
            if (sourceIndex != destineIndex)
            {
                T tmp = list[sourceIndex];
                list.RemoveAt(sourceIndex);
                list.Insert(destineIndex - (sourceIndex < destineIndex ? 1 : 0), tmp);
            }
        }

        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            if (index1 != index2)
            {
                T tmp = list[index1];
                list[index1] = list[index2];
                list[index2] = tmp;
            }
        }

        /// <summary>
        /// リストに値が含まれていなければ追加して true を返す。
        /// 既に含まれている場合は、何もせず false を返す。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncludeWithoutNull<T>(this List<T> list, T value)
        {
            if (value != null && list.Contains(value) == false)
            {
                list.Add(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// リストに値が含まれていなければ追加して true を返す。
        /// 既に含まれている場合は、何もせず false を返す。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns>true: 組み込んだ。 false: 既に組み込み済み</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Include<T>(this List<T> list, T value)
        {
            if (list.Contains(value) == false)
            {
                list.Add(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// リストに値が含まれていなければ追加する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="enumerable"></param>
        public static void IncludeRange<T>(this List<T> list, IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
                if (list.Contains(item) == false)
                    list.Add(item);
        }

        /// <summary>
        /// リストに値が含まれていなければ追加して true を返す。
        /// 既に含まれている場合は、何もせず false を返す。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Include<T>(this List<T> list, T value, Predicate<T> match)
        {
            foreach (var item in list)
                if (match(item))
                    return false;

            list.Add(value);
            return true;
        }

        /// <summary>
        /// マッチしなければ 加える（true）。マッチしたら差し替える（false）
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IncludeOrReplace<T>(this List<T> list, T value, Predicate<T> match)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (match(list[i]))
                {
                    list[i] = value;
                    return false;
                }
            }

            list.Add(value);
            return true;
        }


        /// <summary>
        /// リストに値が含まれていなければ追加して true を返す。
        /// 既に含まれている場合は、何もせず false を返す。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns></returns>
        public static List<T> Include<T>(this List<T> list, IEnumerable<T> values)
        {
            foreach (var value in values)
                if (list.Contains(value) == false)
                    list.Add(value);

            return list;
        }

        /// <summary>
        /// リストに値が含まれていなければ追加して true を返す。
        /// 既に含まれている場合は、何もせず false を返す。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="list">リストオブジェクト</param>
        /// <param name="value">含める値</param>
        /// <returns></returns>
        public static bool IncludeWithoutNull<T>(this List<T> list, IEnumerable<T> values)
        {
            bool isModify = false;

            foreach (var value in values)
            {
                if (value != null && list.Contains(value) == false)
                {
                    list.Add(value);
                    isModify = true;
                }
            }

            return isModify;
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
                action(item);
        }

        /// <summary>
        /// リストを連結する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="splitString"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string Concat<T>(this List<T> list, string splitString = null, string prefix = null, string suffix = null)
        {
            StringBuilder bild = new StringBuilder();

            foreach (var value in list)
            {
                if (bild.Length > 0)
                    bild.Append(splitString);

                if (prefix != null)
                    bild.Append(prefix);

                if (value != null)
                    bild.Append(value);

                if (suffix != null)
                    bild.Append(suffix);
            }

            return bild.ToString();
        }

        /// <summary>
        /// 全ての要素の指定条件の値が全て同一か？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool Equals<T, T2>(this List<T> list, Func<T, T2> func)
        {
            if (list.Count <= 1)
                return true;

            T2 first = func(list[0]);
            for (int i = 1; i < list.Count; i++)
            {
                var at = func(list[i]);
                if (first == null)
                {
                    if (at != null)
                        return false;
                }
                else
                {
                    if (!first.Equals(at))
                        return false;
                }
            }

            return true;
        }
    }
}
