using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using NFluent;
using Xunit;

namespace Rehearsal.Common.Test
{
    public class RandomizeTests
    {
        [Fact]
        public void SourceNullThrows()
        {
            IEnumerable<string> empty = null;
            Check.ThatCode(() => empty.Randomize()).Throws<ArgumentNullException>();
        }

        [Fact]
        public void EmptyArrayReturnsEmptyEnumerable()
        {
            Check.That(new string[] { }.Randomize().ToList()).IsEmpty();
        }

        [Fact]
        public void ArrayWithOneElementContainsThatOneElement()
        {
            var faker = new Faker();

            var list = new string[] {faker.Lorem.Word()};
            
            Check.That(list.Randomize().ToList()).ContainsExactly(list);
        }

        [Fact]
        public void WorksWithManyElements()
        {
            var list = Enumerable.Range(0, 100).ToArray();
            Check.That(list.Randomize().ToList()).Contains(list);
        }
    }
}