using FluentValidation;
using StructureMap;

namespace Rehearsal.WebApi.Infrastructure.StructureMap
{
    public class ValidatorRegistry : Registry
    {
        public ValidatorRegistry()
        {
            Scan(_ =>
            {
                _.TheCallingAssembly();
                _.ConnectImplementationsToTypesClosing(typeof(IValidator<>))
                    .OnAddedPluginTypes(x => x.Singleton()) // Validators are singletons, dependencies must be Lazy<T> or Func<T>
                ;    
            });
        }
    }
}