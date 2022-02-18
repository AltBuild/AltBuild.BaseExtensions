using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AltBuild.BaseExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 複数のフラグのいずれか、含まれているか？
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static bool In<T>(this Enum source, params T[] targets)
            where T: Enum
        {
            if (source == null)
                throw new InvalidProgramException();

            if (source.GetType().GetCustomAttribute<FlagsAttribute>() != null)
            {
                foreach (var target in targets)
                    if (source.HasFlag(target))
                        return true;
            }
            else
            {
                foreach (var target in targets)
                    if (source.Equals(target))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// 複数のフラグのいずれも、含まれていないか？
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        public static bool Out(this Enum source, params Enum[] targets) => !In(source, targets);
    }
}
