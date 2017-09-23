using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rehearsal.Data;

namespace Rehearsal.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .UseEnvironment("Development")
                .Build();

            //host.Services.GetService<SqliteEventStore>().ProvisionTable().Wait();
            host.Services.GetService<InjectTestData>().Run().Wait();
            host.Services.GetService<EventReplayer>().ReplayEvents(
                host.Services.GetService<IEventRepository>().GetEventStream()
             );

            host.Run();
        }
    }
}
