using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreCooking.Data
{
    public class Settings
    {
        public string AzureStorageConnectionString { get; set; }
    }

    public static class SettingsFactory
    {
        public static IConfigurationRoot Configuration { get; set; }
        
        public static string GetConnectionString()
        {
            if (Configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.UserSecrets.json", optional: true);

                Configuration = builder.Build();
            }

            IConfigurationSection settings = Configuration.GetSection("Settings");

            string connectionString = settings["AzureStorageConnectionString"];

            return connectionString;
        }
    }
}
