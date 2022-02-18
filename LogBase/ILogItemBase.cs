using System;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Log base.
    /// </summary>
    public interface ILogItemBase
    {
        DateTime TimeStamp { get; }

        string Name { get; }

        LogLevel Level { get; }

        string Message { get; }

        string StackTrace { get; }
    }
}
