using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MembershipCardSystem
{
    public class Config
    {
        public static IConfigurationRoot ApplicationConfiguration =>
            _configuration ?? CreateConfigurationRoot();

        private static IConfigurationRoot _configuration;

        private static IConfigurationRoot CreateConfigurationRoot()
        {
            const string defaultEnvironment = "development";
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? defaultEnvironment;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment.ToLowerInvariant()}.json", optional: true,
                    reloadOnChange: false)
                .Build();

            return _configuration;
        }
    }
}