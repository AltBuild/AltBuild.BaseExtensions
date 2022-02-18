using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltBuild.BaseExtensions
{
    /// <summary>
    /// Log base.
    /// </summary>
    public class LogBase<T> : ILogBase
        where T : LogItem
    {
        protected object _lockObject = new();

        protected List<T> _items = new ();

        public IReadOnlyCollection<T> Items => _items;

        public int Count => _items.Count;

        public LogLevel MaxLevel => _items.Count > 0 ? _items.Max(x => x.Level) : LogLevel.None;

        public T[] ToArray()
        {
            lock (_lockObject)
            {
                return _items.ToArray();
            }
        }

        ILogItemBase[] ILogBase.ToArray()
        {
            lock (_lockObject)
            {
                return (from x in _items select (ILogItemBase)x).ToArray();
            }
        }

        public string Message
        {
            get
            {
                StringBuilder bild = new StringBuilder();
                foreach (var item in ToArray())
                    bild.Chain("\r\n").Append(item.Message);

                return bild.ToString();
            }
        }

        public void Add(T item)
        {
            lock (_lockObject)
            {
                _items.Add(item);
            }
        }

        public void AddRange(IEnumerable<T> items)
        {
            lock (_lockObject)
            {
                _items.AddRange(items);
            }
        }

        void ILogBase.Add(ILogItemBase item) => Add((T)item);

        void ILogBase.AddRange(IEnumerable<ILogItemBase> items)
        {
            lock (_lockObject)
            {
                items.ForEach(o => _items.Add((T)o));
            }
        }

        void ILogBase.AddRange(ILogBase log)
        {
            lock (_lockObject)
            {
                log.AddRange(log.ToArray());
            }
        }
    }
}
