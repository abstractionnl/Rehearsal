using System;
using NFluent;
using Xunit;

namespace Core.Tests
{
    public class EntityTests 
    {
        [Fact]
        public void HasIdentity()
        {
            var e = new TestEntity();
            Check.That(e.Id).IsNotNull();
            Check.That(e.Id.Value).IsNotEqualTo(Guid.NewGuid());
        }

        [Fact]
        public void ObjectUpdatesWithState()
        {
            var newId = Guid.NewGuid();
            var state = new TestEntityState {Id = new TestEntityIdentity()};
            var e = new TestEntity(state);

            state.Id = new TestEntityIdentity(newId);

            Check.That(e.Id.Value).IsEqualTo(newId);
        }

        public class EntityEqualityTests : EqualityTests
        {
            private static readonly TestEntityIdentity Identity = new TestEntityIdentity();
            private static readonly TestEntityIdentity AnotherIdentity = new TestEntityIdentity();

            protected override object GetObject() => new TestEntity(new TestEntityState { Id = Identity });

            protected override object GetDifferentObject() => new TestEntity(new TestEntityState { Id = AnotherIdentity });
        }

        protected class TestEntity : Entity<TestEntityIdentity, TestEntityState>
        {
            public TestEntity() : base(new TestEntityState { Id = new TestEntityIdentity() })
            {
                
            }

            internal TestEntity(TestEntityState state) : base(state)
            {
                
            }
        }

        protected class TestEntityState : EntityState<TestEntityIdentity>
        {
            
        }

        protected class TestEntityIdentity : Identity<Guid>
        {
            public TestEntityIdentity() : base(Guid.NewGuid()) { }

            public TestEntityIdentity(Guid value) : base(value) { }
        }
    }
}