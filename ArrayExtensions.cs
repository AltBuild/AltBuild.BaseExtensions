using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace AltBuild.BaseExtensions
{
    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
                if (predicate(array[i]))
                    return true;

            return false;
        }

        public static string ToChaining<T>(this T[] array, string prefix, string suffix, string chain)
        {
            bool bPrefix = !string.IsNullOrWhiteSpace(prefix);
            bool bSuffix = !string.IsNullOrWhiteSpace(suffix);
            bool bChain = !string.IsNullOrWhiteSpace(chain);

            StringBuilder bild = new();

            foreach (T at in array)
            {
                if (bChain && bild.Length > 0)
                    bild.Append(chain);

                if (bPrefix)
                    bild.Append(prefix);

                bild.Append(at);

                if (bSuffix)
                    bild.Append(suffix);
            }

            return bild.ToString();
        }
    }
}
