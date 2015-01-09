using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jukebox.Jobs.Extensions
{
    static class EnumerableExtensions
    {
        public static T AddOrUpdate<T>(this IList<T> list, T item) where T : class
        {
            var found = list.FirstOrDefault(x => x.Equals(item));
            return found ?? Add(list, item);
        }

        private static T Add<T>(ICollection<T> collection, T item)
        {
            collection.Add(item);
            return item;
        }

        public static string Sanitize(this string source)
        {
            return new Regex("\\W+").Replace(source, "_");
        }
    }
}