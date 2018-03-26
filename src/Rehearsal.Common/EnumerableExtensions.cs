using System;
using System.Collections.Generic;
using System.Linq;

namespace Rehearsal.Common
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return InternalRandomize(source.ToArray());
        }
        
        private static IEnumerable<T> InternalRandomize<T>(T[] sourceArray)
        {
            var random = new Random();
            var max = sourceArray.Length;

            for (var i = max-1; i >= 0; i--)
            {
                var nextIndexToReturn = random.Next(i);
                var item = sourceArray[nextIndexToReturn];

                if (i != max)
                    sourceArray[nextIndexToReturn] = sourceArray[i];    // Move last item to where the returned item was

                yield return item;
            }
        }
    }
}