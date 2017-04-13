using System;
using NFluent;
using Xunit;

namespace Infrastructure.Tests
{
    public abstract class IdentityTests<T> : EqualityTests
    {
        protected abstract T GetId();
        protected abstract T GetAnotherId();

        protected override object GetObject() => new TestIdentity(GetId());
        protected override object GetDifferentObject() => new TestIdentity(GetAnotherId());

        [Fact]
        public void IdIsSet()
        {
            var id = GetId();
            var i = new TestIdentity(id);
            Check.That(i.Value).IsEqualTo(id);
        }

        [Fact]
        public void CanCompareWithT()
        {
            var id = GetId();
            var i = new TestIdentity(id);

            Check.That(i).IsEqualTo(id);
        }

        public void CanCastToIdType()
        {
            var id = GetId();
            var i = new TestIdentity(id);

            var x = (T) i;
            Check.That(x).IsEqualTo(id);
        }

        protected class TestIdentity : Identity<T>
        {
            public TestIdentity(T value) : base(value)
            {
            }
        }
    }

    public class IntIdentityTests : IdentityTests<int>
    {
        protected override int GetId() => 17;

        protected override int GetAnotherId() => 19;
    }

    public class GuidIdentityTests : IdentityTests<Guid>
    {
        protected static Guid Id = Guid.NewGuid();
        protected static Guid AnotherId = Guid.NewGuid();

        protected override Guid GetId() => Id;

        protected override Guid GetAnotherId() => AnotherId;
    }

    public class StringIdentityTests : IdentityTests<string>
    {
        protected override string GetId() => "one";

        protected override string GetAnotherId() => "two";

        [Fact]
        public void ShouldThrowOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TestIdentity(null));
        }
    }
}