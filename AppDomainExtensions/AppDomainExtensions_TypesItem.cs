using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Linq;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Type の拡張メソッド
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
