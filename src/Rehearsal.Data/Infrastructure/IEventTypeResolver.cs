using System;

namespace Rehearsal.Data.Infrastructure
{
    public interface IEventTypeResolver
    {
        string TypeToString(Type type);
        Type StringToType(string typeName);
    }
}