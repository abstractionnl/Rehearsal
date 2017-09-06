using System;
using AutoMapper;
using Xunit;

namespace Rehearsal.Data.Test
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