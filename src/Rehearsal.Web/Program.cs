﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CQRSlite.Events;
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

            host.Services.GetService<StartupService>().Run().Wait();

            host.Run();
        }
    }
}
