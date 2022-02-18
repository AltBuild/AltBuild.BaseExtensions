using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Type の拡張メソッド
    /// </summary>
    public static partial class AppDomainExtensions
    {
        static object _lockObject = new();

        static ConcurrentDictionary<Type, TypesOfTypeItem> appDomainClassOf = new();
        static ConcurrentDictionary<Type, TypesOfTypeItem> appDomainSubClassOf = new();

        /// <summary>
        /// Cache data clear
        /// </summary>
        public static void CacheClear()
        {
            appDomainClassOf.Clear();
            appDomainSubClassOf.Clear();
        }

        /// <summary>
        /// Get type.
        /// </summary>
        /// <param name="appDomain"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type GetType(this AppDomain appDomain, string typeName)
        {
            if (typeName != null)
            {
                bool bFullName = typeName.Contains(".");

                foreach (Assembly asm in appDomain.GetAssemblies())
                {
                    var type = bFullName ?
                        asm.GetType(typeName) :
                        asm.GetTypes().FirstOrDefault(o => o.Name == typeName);

                    if (type != null)
                        return type;
                }
            }

            return null;
        }

        public static bool TryGetType(this AppDomain appDomain, string typeName, out Type type)
        {
            type = GetType(appDomain, typeName);
            return (type != null);
        }

        /// <summary>
        /// 指定のインターフェースを保持するタイプリストを取得する
        /// </summary>
        /// <param name="appDomain">Target AppDomain</param>
        /// <param name="interfaceType">Target interface type</param>
        /// <returns></returns>
        public static Type[] ClassesOf(this AppDomain appDomain, Type interfaceType)
        {
            if (appDomainClassOf.TryGetValue(interfaceType, out TypesOfTypeItem item) == false)
            {
                lock (_lockObject)
                {
                    if (appDomainClassOf.TryGetValue(interfaceType, out item) == false)
                    {
                        appDomainClassOf.TryAdd(interfaceType, item = new TypesOfTypeItem { SourceType = interfaceType, Types = _classOf(appDomain, interfaceType) });
                    }
                }
            }
            return item.Types;
        }

        /// <summary>
        /// 指定のインターフェースを保持するタイプリストを取得する
        /// </summary>
        /// <param name="appDomain">Target AppDomain</param>
        /// <param name="interfaceType">Target interface type</param>
        /// <returns></returns>
        public static Type[] SubClassOf(this AppDomain appDomain, Type subClass)
        {
            if (appDomainSubClassOf.TryGetValue(subClass, out TypesOfTypeItem item) == false)
            {
                lock (_lockObject)
                {
                    if (appDomainSubClassOf.TryGetValue(subClass, out item) == false)
                    {
                        appDomainSubClassOf.TryAdd(subClass, item = new TypesOfTypeItem { SourceType = subClass, Types = _subClassOf(appDomain, subClass) });
                    }
                }
            }
            return item.Types;
        }

        static Type[] _classOf(this AppDomain appDomain, Type interfaceType)
        {
            Debug.Assert(interfaceType.IsInterface);

            List<Type> results = new();

            foreach (Assembly asm in appDomain.GetAssemblies())
                foreach (var atType in asm.DefinedTypes)
                    if (atType.GetInterfaces().Contains(interfaceType))
                        results.Add(atType);

            return results.ToArray();
        }

        static Type[] _subClassOf(this AppDomain appDomain, Type subClass)
        {
            List<Type> results = new();

            foreach (Assembly asm in appDomain.GetAssemblies())
                foreach (var atType in asm.DefinedTypes)
                    if (atType.IsSubclassOf(subClass))
                        results.Add(atType);

            return results.ToArray();
        }


        /// <summary>
        /// Get all [subclass] inherit types.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetSubClassOf(this AppDomain appDomain, Type subclass, string findString = null)
        {
            string strFind = findString?.ToLower();

            foreach (var type in appDomain.SubClassOf(subclass))
                if (strFind == null || IsHit(type.FullName.ToLower()))
                    yield return type;

            bool IsHit(string typeName)
            {
                int startIndex = 0;

                foreach (var chr in strFind)
                {
                    startIndex = typeName.IndexOf(chr, startIndex);
                    if (startIndex < 0)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Get all [interface] inherit types.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetClassOf(this AppDomain appDomain, Type[] interfaces, string findString = null)
        {
            string strFind = findString?.ToLower();

            foreach (var interfaceType in interfaces)
                foreach (Type type in appDomain.ClassesOf(interfaceType))
                    if (strFind == null || IsHit(type.FullName.ToLower()))
                        yield return type;

            bool IsHit(string typeName)
            {
                int startIndex = 0;

                foreach (var chr in strFind)
                {
                    startIndex = typeName.IndexOf(chr, startIndex);
                    if (startIndex < 0)
                        return false;
                }

                return true;
            }
        }
    }
}
