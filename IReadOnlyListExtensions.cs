using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace AltBuild.BaseExtensions
{
    public static class IReadOnlyListExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="list">source list</param>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public static bool Contains<T>(this IReadOnlyList<T> list, Predicate<T> predicate)
        {
            foreach (var item in list)
                if (predicate(item))
                    return true;

            return false;
        }
    }
}
