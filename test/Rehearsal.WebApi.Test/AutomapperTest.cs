using AutoMapper;
using Xunit;

namespace Rehearsal.WebApi.Test
{
    public class AutomapperTest
    {
        [Fact]
        public void TestConfiguration()
        {
            Mapper.Initialize(cfg => 
                cfg.AddProfile<AutomapperProfile>());
    
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}