using System;
using System.Collections.Generic;

namespace Assignment_01.Comparers
{
    // Custom comparer used by the case-insensitive ordering examples.
    public sealed class CaseInsensitiveComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
            => string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    }
}
