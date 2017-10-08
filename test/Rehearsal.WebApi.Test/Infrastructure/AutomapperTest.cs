using AutoMapper;
using Rehearsal.WebApi.Infrastructure;
using Xunit;

namespace Rehearsal.WebApi.Test.Infrastructure
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