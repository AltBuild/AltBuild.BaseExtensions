using System;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Holds the type that owns the specified interface.
    /// </summary>
    public static partial class AppDomainExtensions
    {
        /// <summary>
        /// 指定のインターフェースを所有するタイプを保持
        /// </summary>
        public class TypesOfTypeItem
        {
            public Type[] Types { get; set; }

            public Type SourceType { get; set; }
        }
    }
}
