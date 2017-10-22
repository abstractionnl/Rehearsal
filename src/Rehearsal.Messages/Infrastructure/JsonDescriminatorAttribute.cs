using System;

namespace Rehearsal.Messages.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class JsonDescriminatorAttribute : Attribute
    {
        public string DiscriminatorField { get; }
        public string Suffix { get; }
        
        public JsonDescriminatorAttribute(string discriminatorField, string suffix)
        {
            DiscriminatorField = discriminatorField;
            Suffix = suffix;
        }
    }
}