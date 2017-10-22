using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LanguageExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rehearsal.WebApi.Infrastructure
{
    public class GenericJsonTypeDeserializer : JsonConverter
    {
        public GenericJsonTypeDeserializer(Type baseType, IEnumerable<Type> subTypes, string discriminatorField, string suffix)
        {
            BaseType = baseType;
            DiscriminatorField = discriminatorField;
            Suffix = suffix;
            SubTypes = subTypes.ToDictionary(x => x.Name);
        }

        private Type BaseType { get; }
        private IDictionary<string, Type> SubTypes { get; }
        private string DiscriminatorField { get; }
        private string Suffix { get; }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var discriminatorToken = jsonObject.GetValue(DiscriminatorField, StringComparison.OrdinalIgnoreCase);

            if (discriminatorToken == null)
                throw new JsonException($"Discriminator {DiscriminatorField} token is absent");
            
            var typeName = (discriminatorToken.Value<string>() ?? "") + Suffix;

            var resultingType = SubTypes.TryGetValue(typeName)
                .IfNone(() =>
                    throw new JsonException($"Failed to find suitable subclass for {typeName}"));

            return serializer.Deserialize(jsonObject.CreateReader(), resultingType);
        }

        public override bool CanConvert(Type objectType)
        {
            return BaseType == objectType;
        }
    }
}