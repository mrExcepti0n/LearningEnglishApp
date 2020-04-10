using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Infrastructure;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IdentityApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();


            var host = BuildWebHost(configuration, args);

            host.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                   .MigrateDbContext<ApplicationDbContext>((_, __) => { }) 
                   .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                    {
                        new ConfigurationDbContextSeed()
                            .SeedAsync(context, configuration)
                            .Wait();
                    });

            host.Run();
        }

        public static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseConfiguration(configuration).Build();


        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();           
            return builder.Build();
        }

    }
}
