using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Rehearsal.Messages.Infrastructure;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

namespace Rehearsal.WebApi.Infrastructure.StructureMap
{
    public class JsonSerializerRegistry : Registry
    {
        public JsonSerializerRegistry()
        {
            For<JsonSerializer>().Use(kernel => JsonSerializer.CreateDefault(new JsonSerializerSettings()
            {
                Converters = kernel.GetAllInstances<JsonConverter>().ToList()
            }));

            For<JsonConverter>().Add(new StringEnumConverter());
            
            Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory();
                _.Convention<GenericJsonTypeDeserializerConvention>();
            });
        }
        
        private class GenericJsonTypeDeserializerConvention : IRegistrationConvention
        {
            public void ScanTypes(TypeSet types, Registry registry)
            {
                foreach (var type in types.FindTypes(TypeClassification.All))
                {
                    var attribute = type.GetTypeInfo().GetCustomAttribute<JsonDescriminatorAttribute>();

                    if (attribute == null) continue;
                    
                    // Type exists, find descendents
                    var descendentTypes = types.FindTypes(TypeClassification.Concretes)
                        .Where(subType => type.IsAssignableFrom(subType))
                        .ToArray();

                    registry.For<JsonConverter>().Add(new GenericJsonTypeDeserializer(type, descendentTypes, attribute.DiscriminatorField, attribute.Suffix));
                }
            }
        }
    }
}