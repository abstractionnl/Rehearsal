using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using NFluent;
using Xunit;

namespace Rehearsal.Common.Test
{
    public class RandomizerTests
    {
        private readonly Randomizer _randomizer;

        public RandomizerTests()
        {
            _randomizer = new Randomizer();
        }
        
        [Fact]
        public void RandomizeThrowsWhenSourceIsNull()
        {
            IEnumerable<string> empty = null;
            Check.ThatCode(() => _randomizer.Randomize(empty)).Throws<ArgumentNullException>();
        }

        [Fact]
        public void EmptyArrayReturnsEmptyEnumerable()
        {
            Check.That(_randomizer.Randomize(new string[] { }).ToList()).IsEmpty();
        }

        [Fact]
        public void ArrayWithOneElementContainsThatOneElement()
        {
            var faker = new Faker();

            var list = new string[] {faker.Lorem.Word()};
            
            Check.That(_randomizer.Randomize(list).ToList()).ContainsExactly(list);
        }

        [Fact]
        public void WorksWithManyElements()
        {
            var list = Enumerable.Range(0, 100).ToArray();
            Check.That(_randomizer.Randomize(list).ToList()).Contains(list);
        }

        [Fact]
        public void PickRandomThrowsWhenSourceIsNull()
        {
            IEnumerable<string> empty = null;
            Check.ThatCode(() => _randomizer.PickRandom(empty)).Throws<ArgumentNullException>();
        }
        
        [Fact]
        public void PickRandomThrowsWhenSourceIsEmpty()
        {
            IEnumerable<string> empty = new string[0];
            Check.ThatCode(() => _randomizer.PickRandom(empty)).Throws<InvalidOperationException>();
        }
        
        [Fact]
        public void PickRandomWithOneElementReturnsThatOneElement()
        {
            var faker = new Faker();

            var list = new string[] {faker.Lorem.Word()};
            
            Check.That(_randomizer.PickRandom(list).ToList()).Equals(list[0]);
        }
        
        [Fact]
        public void PickRandomWorksWithManyElements()
        {
            var list = Enumerable.Range(0, 100).ToArray();
            Check.That(list).Contains(_randomizer.PickRandom(list));
        }
    }
}