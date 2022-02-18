using System;

namespace AltBuild.BaseExtensions
{
    public static class ArrayTypeConverter
    {
        /// <summary>
        /// 配列型の要素型を変換します
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="elementType"></param>
        /// <returns></returns>
        public static object From<TI>(TI[] sourceArray, Type elementType)
        {
            // 結果
            var result = Array.CreateInstance(elementType, sourceArray.Length);

            // キャスト
            for (int i = 0; i < sourceArray.Length; i++)
                result.SetValue(sourceArray[i], i);

            return result;
        }

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this TI[] list) =>
            ToConvert<TO, TI>(list, 0, list.Length);

        /// <summary>
        /// 指定型で配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this TI[] list, int startIndex) =>
            ToConvert<TO, TI>(list, startIndex, list.Length - startIndex);

        /// <summary>
        /// 配列化する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static TO[] ToConvert<TO, TI>(this TI[] list, int startIndex, int requestCount)
        {
            var count = Math.Min(requestCount, list.Length - startIndex);

            TO[] results = new TO[count];
            int at = 0;

            int endIndex = startIndex + count;

            for (int i = startIndex; i < endIndex; i++)
                if (list[i] is TO atValue)
                    results[at++] = atValue;

            return results;
        }
    }
}
