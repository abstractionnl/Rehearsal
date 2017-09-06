using AutoMapper;
using StructureMap;

namespace Rehearsal.Web
{
    public class AutomapperRegistry : Registry
    {
        public AutomapperRegistry()
        {
            For<IMapper>().Use("Build automapper using config", CreateAutomapper);

            Scan(_ =>
            {
                _.AssembliesFromApplicationBaseDirectory();
                _.AddAllTypesOf<Profile>();
            });
        }

        private IMapper CreateAutomapper(IContext context)
        {
            var profiles = context.GetAllInstances<Profile>();
            var config = new MapperConfiguration(x =>
            {
                foreach (var profile in profiles)
                    x.AddProfile(profile);
            });

            return config.CreateMapper();
        }
    }
}
