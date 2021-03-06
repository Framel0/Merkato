﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Merkato
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var webHost = new WebHostBuilder()
       .UseKestrel()
       .UseContentRoot(Directory.GetCurrentDirectory())
       .ConfigureAppConfiguration((hostingContext, config) =>
       {
           var env = hostingContext.HostingEnvironment;
           config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
           config.AddEnvironmentVariables();
       })
       .ConfigureLogging((hostingContext, logging) =>
       {
           logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
           logging.AddConsole();
           logging.AddDebug();
       })
       .UseStartup<Startup>()
       .Build();

            webHost.Run();
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
