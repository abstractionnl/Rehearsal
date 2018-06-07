using System;
using System.Collections.Generic;
using System.Linq;

namespace Rehearsal.Common
{
    public class Randomizer
    {
        private Random _random;

        public Randomizer()
        {
            _random = new Random();
        }
        
        public Randomizer(Random random)
        {
            _random = random;
        }

        public IEnumerable<T> Randomize<T>(IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return InternalRandomize(source.ToList(), _random);
        }
        
        public T PickRandom<T>(IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return InternalRandom(source, _random);
        }
        
        private static IEnumerable<T> InternalRandomize<T>(IList<T> sourceArray, Random random)
        {
            var max = sourceArray.Count;

            for (var i = max-1; i >= 0; i--)
            {
                var nextIndexToReturn = random.Next(i);
                var item = sourceArray[nextIndexToReturn];

                if (i != max)
                    sourceArray[nextIndexToReturn] = sourceArray[i];    // Move last item to where the returned item was

                yield return item;
            }
        }
        
        private static T InternalRandom<T>(IEnumerable<T> source, Random random)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (random == null) throw new ArgumentNullException(nameof(random));

            var asCollection = source as ICollection<T> ?? source.ToList();

            if (asCollection.Count < 2)
                return asCollection.First();
            
            var index = random.Next(asCollection.Count);
            
            if (source is IList<T> list)
                return list[index];

            using (var e = asCollection.GetEnumerator())
            {
                var i = 0;
                while (i++ <= index)
                    e.MoveNext();

                return e.Current;
            } 
        }
    }
}