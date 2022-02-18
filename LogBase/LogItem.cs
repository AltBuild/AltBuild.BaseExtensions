using System;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Log item.
    /// </summary>
    public class LogItem : ILogItemBase
    {
        public DateTime TimeStamp { get; init; } = DateTime.Now;

        public string Name { get; init; }

        public LogLevel Level { get; init; } = LogLevel.Error;

        public string Message { get; init; }

        public string StackTrace { get; init; } = Environment.StackTrace;

        public override string ToString()
        {
            return $"{TimeStamp} {Level} {Name} {Message}";
        }
    }
}
