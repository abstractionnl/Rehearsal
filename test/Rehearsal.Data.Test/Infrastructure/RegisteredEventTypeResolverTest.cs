using System;
using NFluent;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Data.Test.Mocks;
using Xunit;

namespace Rehearsal.Data.Test.Infrastructure
{
    public class RegisteredEventTypeResolverTest
    {
        protected readonly RegisteredEventTypeResolver Resolver;
        
        public RegisteredEventTypeResolverTest()
        {
            Resolver = new RegisteredEventTypeResolver();
        }

        [Fact]
        public void CanRegisterAndRetrieveTypeByName()
        {
            Resolver.RegisterEventType("SomeEvent", typeof(SomeEvent));

            Check.That(Resolver.StringToType("SomeEvent")).IsEqualTo(typeof(SomeEvent));
        }

        [Fact]
        public void CanRegisterAndRetrieveNameByType()
        {
            Resolver.RegisterEventType("SomeEvent", typeof(SomeEvent));

            Check.That(Resolver.TypeToString(typeof(SomeEvent))).IsEqualTo("SomeEvent");
        }

        [Fact]
        public void CanRegisterTypeUnderMultipleNames()
        {
            Resolver.RegisterEventType("SomeEvent", typeof(SomeEvent));
            Resolver.RegisterEventType("OtherEventName", typeof(SomeEvent), false);

            Check.That(Resolver.StringToType("SomeEvent")).IsEqualTo(typeof(SomeEvent));
            Check.That(Resolver.StringToType("OtherEventName")).IsEqualTo(typeof(SomeEvent));
        }

        [Fact]
        public void CanRegisterTypeUnderMultipleNamesAndRetrieveDefaultName()
        {
            Resolver.RegisterEventType("SomeEvent", typeof(SomeEvent), false);
            Resolver.RegisterEventType("DefaultEventName", typeof(SomeEvent), true);

            Check.That(Resolver.TypeToString(typeof(SomeEvent))).IsEqualTo("DefaultEventName");
        }

        [Fact]
        public void CannotRegisterEventUnderSameName()
        {
            Resolver.RegisterEventType("SomeEvent", typeof(SomeEvent));
            
            Check.ThatCode(() => Resolver.RegisterEventType("SomeEvent", typeof(AnotherEvent)))
                .Throws<InvalidOperationException>();
        }
    }
}