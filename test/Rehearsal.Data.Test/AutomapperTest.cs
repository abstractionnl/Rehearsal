using System;
using AutoMapper;
using Rehearsal.Data.Infrastructure;
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