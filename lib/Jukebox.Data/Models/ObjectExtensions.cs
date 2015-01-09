using System;
using System.Collections.Generic;
using System.Linq;

namespace Jukebox.Data.Models
{
    static class ObjectExtensions
    {
        public static bool CeremoniallyEquals<T>(this T ourObject, object theirObject, Func<T, bool> equals) where T : class
        {
            if (ReferenceEquals(null, theirObject)) return false;
            if (ReferenceEquals(ourObject, theirObject)) return true;
            return theirObject.GetType() == ourObject.GetType() && @equals((T)theirObject);
        }

        public static int CombinedHashCodes(this object us, params object[] others)
        {
            return others.GetSequenceHashCode();
        }

        public static int GetSequenceHashCode<T>(this IEnumerable<T> enumerable) where T : class
        {
            return enumerable.Aggregate(0, (acc, x) => acc ^ (x ?? (object)0).GetHashCode());
        }
    }
}