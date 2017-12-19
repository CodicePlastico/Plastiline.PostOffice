using System.Collections.Generic;
using System.Linq;

namespace Plastiline.PostOffice.Extensions {
    public static class EnumerableExtension {
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> self) {
            return self ?? Enumerable.Empty<T>();
        }
    }
}