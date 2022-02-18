using System;
using System.Collections.Generic;

namespace AltBuild.BaseExtensions
{
    public class MatchList<T>
    {
        public List<T> Removed { get; } = new List<T>();

        public List<T> Added { get; } = new List<T>();

        public List<(T src, T dst)> Existed { get; } = new List<(T src, T dst)>();
    }
}
