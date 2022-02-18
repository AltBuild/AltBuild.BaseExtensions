using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// StringBuilderの拡張メソッド群
    /// </summary>
    public static partial class StringBuilderExtensions
    {
        /// <summary>
        /// 指定の位置から最後まで、文字列を連結する
        /// </summary>
        /// <param name="src">元の文字列</param>
        /// <param name="str">連結する文字列を含む文字列</param>
        /// <param name="startIndex">連結する最初の位置</param>
        /// <returns>元のオブジェクトを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Append(this StringBuilder src, string str, int startIndex)
        {
            src.Append(str, startIndex, str.Length - startIndex);
            return src;
        }

        /// <summary>
        /// 連結区切文字(chain)を末尾に付加（但し元がEmptyの場合は付加しない）する
        /// </summary>
        /// <param name="src">元のStringBuilder</param>
        /// <param name="chainChar">連結区切文字</param>
        /// <returns>元のStringBuilderを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Chain(this StringBuilder src, char chainChar)
        {
            if (src.Length > 0)
                src.Append(chainChar);

            return src;
        }

        /// <summary>
        /// 連結区切文字列(chain)を末尾に付加（但し元がEmptyの場合は付加しない）する
        /// </summary>
        /// <param name="src">元のStringBuilder</param>
        /// <param name="chainChar">連結区切文字列</param>
        /// <returns>元のStringBuilderを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Chain(this StringBuilder src, string chainString)
        {
            if (src.Length > 0)
                src.Append(chainString);

            return src;
        }
    }
}
