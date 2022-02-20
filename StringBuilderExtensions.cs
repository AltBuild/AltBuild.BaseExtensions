using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// StringBuilder extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// 指定の位置から最後まで、文字列を連結する
        /// </summary>
        /// <param name="builder">元の文字列</param>
        /// <param name="value">連結する文字列を含む文字列</param>
        /// <param name="startIndex">連結する最初の位置</param>
        /// <returns>元のオブジェクトを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Append(this StringBuilder builder, string value, int startIndex)
        {
            builder.Append(value, startIndex, value.Length - startIndex);
            return builder;
        }

        /// <summary>
        /// 連結区切文字(chain)を末尾に付加（但し元がEmptyの場合は付加しない）する
        /// </summary>
        /// <param name="builder">元のStringBuilder</param>
        /// <param name="chainChar">連結区切文字</param>
        /// <returns>元のStringBuilderを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Chain(this StringBuilder builder, char chainChar)
        {
            if (builder.Length > 0)
                builder.Append(chainChar);

            return builder;
        }

        /// <summary>
        /// 連結区切文字列(chain)を末尾に付加（但し元がEmptyの場合は付加しない）する
        /// </summary>
        /// <param name="builder">元のStringBuilder</param>
        /// <param name="chainString">連結区切文字列</param>
        /// <returns>元のStringBuilderを返す</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Chain(this StringBuilder builder, string chainString)
        {
            if (builder.Length > 0)
                builder.Append(chainString);

            return builder;
        }
    }
}
