using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NFluent;
using Rehearsal.Infrastructure;
using Xunit;

namespace Rehearsal.Tests.Infrastructure
{
    public class ValueObjectTests
    {
        [Fact]
        public void EqualsIsReflexive()
        {
            var obj = new TestValueObject("Test", 17);

            AssertEquals(obj, obj);
        }

        [Fact]
        public void PropertiesAreEqual()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new TestValueObject("Test", 17);

            AssertEquals(obj, obj2);
        }

        [Fact]
        public void EqualsIsSymmetric()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new TestValueObject("Test", 17);

            AssertEquals(obj, obj2);
            AssertEquals(obj2, obj);
        }

        [Fact]
        public void EqualsIsTransitive()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new TestValueObject("Test", 17);
            var obj3 = new TestValueObject("Test", 17);

            AssertEquals(obj, obj2);
            AssertEquals(obj2, obj3);
            AssertEquals(obj, obj3);
        }

        [Fact]
        [SuppressMessage("ReSharper", "EqualExpressionComparison")]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void NullIsNotEqual()
        {
            var obj = new TestValueObject("Test", 17);

            Check.That(obj == null).IsFalse();
            Check.That(obj != null).IsTrue();

            Check.That(null == obj).IsFalse();
            Check.That(null != obj).IsTrue();

            Check.That((TestValueObject)null == (TestValueObject)null).IsTrue();
            Check.That((TestValueObject)null != (TestValueObject)null).IsFalse();
        }

        [Fact]
        public void DifferentPropertiesAreNotEqual()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new TestValueObject("Different", 17);
            var obj3 = new TestValueObject("Test", 19);

            AssertNotEqual(obj, obj2);
            AssertNotEqual(obj, obj3);
        }

        [Fact]
        public void HandlesNullProperties()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new TestValueObject(null, 17);

            AssertNotEqual(obj, obj2);
            AssertNotEqual(obj2, obj);
        }

        [Fact]
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public void DifferentTypeNotEqual()
        {
            var obj = new TestValueObject("Test", 17);
            var obj2 = new DifferentValueObject();

            Check.That(obj.Equals(obj2)).IsFalse();
        }

        private static void AssertEquals(TestValueObject obj, TestValueObject obj2)
        {
            Check.That(obj.Equals(obj2)).IsTrue();
            Check.That(obj == obj2).IsTrue();
            Check.That(obj != obj2).IsFalse();
            Check.That(obj.GetHashCode()).IsEqualTo(obj2.GetHashCode());
        }

        private static void AssertNotEqual(TestValueObject obj, TestValueObject obj2)
        {
            Check.That(obj.Equals(obj2)).IsFalse();
            Check.That(obj == obj2).IsFalse();
            Check.That(obj != obj2).IsTrue();
            Check.That(obj.GetHashCode()).IsNotEqualTo(obj2.GetHashCode());
        }

        private class TestValueObject : ValueObject<TestValueObject>
        {
            public string A { get; private set; }
            public int B { get; private set; }

            public TestValueObject(String a, int b)
            {
                A = a;
                B = b;
            }

            protected override IEnumerable<Func<TestValueObject, object>> GetCompareProperties()
            {
                yield return x => x.A;
                yield return x => x.B;
            }
        }

        private class DifferentValueObject : ValueObject<DifferentValueObject>
        {
            protected override IEnumerable<Func<DifferentValueObject, object>> GetCompareProperties()
            {
                yield break;
            }
        }
    }
}
