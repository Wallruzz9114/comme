using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using API.Helpers;
using Data.Contexts;
using Data.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();
                    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                    var identityContext = serviceProvider.GetRequiredService<IdentityContext>();

                    await databaseContext.Database.MigrateAsync();
                    await DataSeeder.SeedAsync(databaseContext);
                    await identityContext.Database.MigrateAsync();
                    await DataSeeder.SeedIdentityAsync(userManager);
                }
                catch (Exception exception)
                {
                    Log.Error(
                        $"An error occurred while seeding the database " +
                        $"{exception.Message} {exception.StackTrace} " +
                        $"{exception.InnerException} {exception.Source}"
                    );
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSerilog((hostingContext, loggingConfiguration) => loggingConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Application", "comme")
                    .Enrich.WithProperty("MachineName", Environment.MachineName)
                    .Enrich.WithProperty("CurrentManagedThreadId", Environment.CurrentManagedThreadId)
                    .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                    .Enrich.WithProperty("Version", Environment.Version)
                    .Enrich.WithProperty("UserName", Environment.UserName)
                    .Enrich.WithProperty("ProcessId", Process.GetCurrentProcess().Id)
                    .Enrich.WithProperty("ProcessName", Process.GetCurrentProcess().ProcessName)
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .WriteTo.File(
                        formatter: new LogTextFormatter(),
                        path: Path.Combine(
                            hostingContext.HostingEnvironment.ContentRootPath +
                            $"{Path.DirectorySeparatorChar}Logs{Path.DirectorySeparatorChar}",
                            $"comme_log_{DateTime.Now:yyyyMMdd}.txt"
                        )
                    )
                    .ReadFrom.Configuration(hostingContext.Configuration)
                );

                webBuilder.UseStartup<Startup>();
            });
    }
}
