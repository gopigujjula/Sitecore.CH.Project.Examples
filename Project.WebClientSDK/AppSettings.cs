using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Sitecore.CH.Project.WebClientSDK.Examples
{
    public static class AppSettings
    {
        private static IConfiguration _config;
        public static IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

                    _config = builder.Build();
                }

                return _config;
            }
        }
        public static Uri Host { get { return new Uri($"{Configuration["M:Host"]}"); } }
        public static string ClientId { get { return $"{Configuration["M:ClientId"]}"; } }
        public static string ClientSecret { get { return $"{Configuration["M:ClientSecret"]}"; } }
        public static string Username { get { return $"{Configuration["M:Username"]}"; } }
        public static string Password { get { return $"{Configuration["M:Password"]}"; } }        
    }
}
