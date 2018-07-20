using System;
using Rehearsal.Data.Infrastructure;

namespace Rehearsal.Data.Test.Mocks
{
    public class MockedEventTypeResolver : IEventTypeResolver
    {
        public string TypeToString(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public Type StringToType(string typeName)
        {
            return Type.GetType(typeName);
        }
    }
}