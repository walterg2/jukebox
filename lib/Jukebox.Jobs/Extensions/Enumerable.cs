using System;
using System.Collections.Generic;
using System.Linq;

namespace Jukebox.Jobs.Extensions
{
    static class Enumerable
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items) action(item);
        }
    }
}