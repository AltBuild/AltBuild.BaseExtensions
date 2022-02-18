using System;
using System.Text;

namespace AltBuild.BaseExtensions
{
    public class StringJoin
    {
        public StringBuilder Source { get; }

        public string Join { get; }

        /// <summary>
        /// Add文字列の先頭に付加
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// Add文字列の末尾に付加
        /// </summary>
        public string Suffix { get; }

        /// <summary>
        /// 生成文字列の先頭に付加する
        /// </summary>
        public string Begin { get; }

        /// <summary>
        /// 生成文字列の最後に付加する
        /// </summary>
        public string End { get; }

        public int Count { get; private set; } = 0;

        public StringJoin(string join, string prefix = null, string suffix = null, string beginString = null, string endString = null)
        {
            Join = join;
            Prefix = prefix;
            Suffix = suffix;
            Begin = beginString;
            End = endString;

            Source = new StringBuilder();
        }

        /// <summary>
        /// 全文字列を生成し出力する
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Begin != null)
            {
                if (End != null)
                    return $"{Begin}{Source}{End}";
                else
                    return $"{Begin}{Source}";
            }
            else if (End != null)
                return $"{Source}{End}";
            else
                return Source.ToString();
        }

        public StringJoin NativeAdd(string text)
        {
            if (text != null)
                Source.Append(text);

            return this;
        }

        public StringJoin Add(string text)
        {
            if (text != null)
            {

                if (Count > 0)
                    Source.Append(Join);

                if (!string.IsNullOrEmpty(Prefix))
                    Source.Append(Prefix);

                if (!string.IsNullOrEmpty(text))
                    Source.Append(text);

                if (!string.IsNullOrEmpty(Suffix))
                    Source.Append(Suffix);

                Count++;
            }

            return this;
        }
    }
}
