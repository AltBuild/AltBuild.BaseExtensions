using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// String method extensions
    /// </summary>
    public static class StringExtensions
    {
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
        /// NumberOfLines
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static int NumberOfLines(this string line)
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
    }
}
