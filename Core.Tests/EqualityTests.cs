using System;
using NFluent;
using Xunit;

namespace Core.Tests
{
    public abstract class EqualityTests
    {
        protected abstract object GetObject();
        protected abstract object GetDifferentObject();

        [Fact]
        public void IsEqualToItself()
        {
            var obj = GetObject();
            var obj2 = obj;

            Check.That(obj.Equals(obj2)).IsTrue();
        }

        [Fact]
        public void IsNotEqualToNull()
        {
            var obj = GetObject();

            Check.That(obj.Equals(null)).IsFalse();
        }

        [Fact]
        public void IsEqualToAnotherObject()
        {
            var obj = GetObject();
            var obj2 = GetObject();

            ValidateObjectsAreNotSame(obj, obj2);

            Check.That(obj.Equals(obj2)).IsTrue();
            Check.That(obj2.Equals(obj)).IsTrue();
        }

        [Fact]
        public void IsTransitive()
        {
            var obj = GetObject();
            var obj2 = GetObject();
            var obj3 = GetObject();

            ValidateObjectsAreNotSame(obj, obj2, obj3);

            Check.That(obj.Equals(obj2)).IsTrue();
            Check.That(obj2.Equals(obj3)).IsTrue();
            Check.That(obj.Equals(obj3)).IsTrue();
        }

        [Fact]
        public void HasSameHashcodeAsAnotherObject()
        {
            var obj = GetObject();
            var obj2 = GetObject();

            ValidateObjectsAreNotSame(obj, obj2);

            Check.That(obj.GetHashCode()).IsEqualTo(obj2.GetHashCode());
        }

        [Fact]
        public void IsNotEqualToDifferentObject()
        {
            var obj = GetObject();
            var obj2 = GetDifferentObject();

            Check.That(obj.Equals(obj2)).IsFalse();
        }

        private static void ValidateObjectsAreNotSame(params object[] objects)
        {
            for (var i=0; i<objects.Length; i++)
            for (var j=i+1; j<objects.Length; j++)
                if (ReferenceEquals(objects[i], objects[j]))
                    throw new InvalidOperationException("EqualityTests.GetObject should return new but equal objects");
        }
    }
}