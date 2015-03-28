using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        //NOTE: I hate and understand why this is not aviable out of the box. This should be used sparingly.
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> processor)
        {
            foreach(T value in enumerable) { processor(value); }
        }
    }
}
