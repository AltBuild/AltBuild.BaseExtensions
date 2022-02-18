using System;
using System.Collections.Generic;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Log base.
    /// </summary>
    public interface ILogBase
    {
        LogLevel MaxLevel { get; }

        int Count { get; }

        string Message { get; }

        void Add(ILogItemBase item);

        void AddRange(IEnumerable<ILogItemBase> items);

        void AddRange(ILogBase log);

        ILogItemBase[] ToArray();
    }
}
