using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Stringの拡張メソッド群
    /// </summary>
    public static class StringExtensions
    {
        public static string Trim(this string value, char[] startTrimChars, char[] endTrimChars) =>
            value.TrimStart(startTrimChars).TrimEnd(endTrimChars);

        public static string[] Split(string source, params char[] separator)
        {
            if (string.IsNullOrWhiteSpace(source))
                return Array.Empty<string>();
            else
                return source.Split(separator);
        }

        /// <summary>
        /// 文字列の先頭から、指定の文字数を取得する
        /// 指定の文字数に満たない時は、全てを返す
        /// </summary>
        /// <param name="source">元の文字列</param>
        /// <param name="count">抽出文字数</param>
        /// <returns></returns>
        public static string Beginning(this string source, int count) =>
            source?.Substring(0, Math.Min(source.Length, count));

        /// <summary>
        /// 文字列の末尾から、指定の文字数を取得する
        /// 指定の文字数に満たない時は、全てを返す
        /// </summary>
        /// <param name="source">元の文字列</param>
        /// <param name="count">抽出文字数</param>
        /// <returns></returns>
        public static string Ending(this string source, int count) =>
            source?.Substring(Math.Max(0, source.Length - count));

        /// <summary>
        /// 文字列の中身がnull or Empty or WhiteSpace 以外の意味のある文字列の場合に、
        /// 文字列の前後（preFix と sufFix）の文字列を埋め込み 返す。
        /// </summary>
        /// <param name="source">元の文字列</param>
        /// <param name="preFix">前につける文字列</param>
        /// <param name="sufFix">後に付けつ文字列</param>
        /// <returns></returns>
        public static string ToFix(this string source, string preFix, string sufFix)
        {
            if (string.IsNullOrWhiteSpace(source))
                return "";
            else
                return $"{preFix}{source}{sufFix}";
        }

        /// <summary>
        /// 複数の特定の文字列が含まれているか？
        /// </summary>
        /// <param name="source">検証元文字列</param>
        /// <param name="strings">特定の文字列（複数）</param>
        /// <returns>含む場合は true、含まない場合は false</returns>
        public static bool Contains(this string source, StringComparison comparison = StringComparison.Ordinal, params string[] strings)
        {
            foreach (var targetString in strings)
                if (source.Contains(targetString, comparison))
                    return true;

            return false;
        }

        /// <summary>
        /// 複数の文字列の何れかと一致するか？
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool Equals(this string source, StringComparison comparison = StringComparison.Ordinal, params string[] strings)
        {
            foreach (var targetString in strings)
                if (source.Equals(targetString, comparison))
                    return true;

            return false;
        }

        /// <summary>
        /// 複数の特定の文字列を削除して返す
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string RemoveWords(this string source, params string[] strings)
        {
            string result = source;
            foreach (var targetString in strings)
                result = result.Replace(targetString, "");

            return result;
        }

        /// <summary>
        /// 特定の文字を削除して返す
        /// </summary>
        /// <param name="source">元の文字</param>
        /// <param name="chars">削除する文字</param>
        /// <returns></returns>
        public static string RemoveChars(this string source, params char[] chars)
        {
            StringBuilder bild = new StringBuilder();

            for (int i  = 0; i < source.Length; i++)
                if (!chars.Contains(source[i]))
                    bild.Append(source[i]);

            return bild.ToString();
        }

        /// <summary>
        /// 特定の文字以外を削除して返す
        /// </summary>
        /// <param name="source">元の文字</param>
        /// <param name="withoutChars">残す文字</param>
        /// <returns></returns>
        public static string RemoveWithoutChars(this string source, string withoutChars) =>
            RemoveWithoutChars(source, withoutChars.ToCharArray());

        /// <summary>
        /// 特定の文字以外を削除して返す
        /// </summary>
        /// <param name="source">元の文字</param>
        /// <param name="withoutChars">残す文字</param>
        /// <returns></returns>
        public static string RemoveWithoutChars(this string source, params char[] withoutChars)
        {
            if (source == null)
                return null;

            StringBuilder bild = new StringBuilder();

            for (int i = 0; i < source.Length; i++)
                if (withoutChars.Contains(source[i]))
                    bild.Append(source[i]);

            return bild.ToString();
        }

        /// <summary>
        /// 指定の文字列全てが IsNullOrWhiteSpace であるか？
        /// </summary>
        /// <param name="source"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(params string[] strings)
        {
            foreach (var str in strings)
                if (!string.IsNullOrWhiteSpace(str))
                    return false;

            return true;
        }

        /// <summary>
        /// string1 と string2 が同じ文字列か？
        /// （null, "", " " 等は 同じと解釈する）
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static bool EqualsWithNullOrWhiteSpace(string string1, string string2)
        {
            if (string.IsNullOrWhiteSpace(string1))
                return string.IsNullOrWhiteSpace(string2);
            else
                return string1.Equals(string2, StringComparison.Ordinal);
        }

        /// <summary>
        /// SkippingWords
        /// </summary>
        /// <param name="line"></param>
        /// <param name="skippingWords"></param>
        /// <returns></returns>
        public static int IndexOf(this string line, char endOfChar, int index, PreSufChars[] bulkChars = null)
        {
            for (int i = index; i < line.Length; i++)
            {
                char c = line[i];

                if (c == endOfChar)
                    return i;

                if (bulkChars != null)
                {
                    foreach (var bulkChar in bulkChars)
                    {
                        if (bulkChar.Pre == c)
                        {
                            i = IndexOf(line, bulkChar.Suf, i + 1, bulkChars);
                            break;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 行数を取得する
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int LineCount(this string line)
        {
            int count = 0;
            int atHit = -1;
            char atChar = '\0';

            for (int i = 0; i < line.Length; i++)
            {
                if ((line[i] is '\r' or '\n'))
                {
                    if (atHit + 1 != i || atChar == line[i])
                    {
                        count++;
                        atHit = i;
                        atChar = line[i];
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 先頭から文字列として有効な文字列を抽出する
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ValidCandidate(params string[] values)
        {
            foreach (var value in values)
                if (string.IsNullOrWhiteSpace(value) == false)
                    return value;

            return null;
        }
    }
}
