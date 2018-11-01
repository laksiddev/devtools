using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Open.Common.Configuration
{
    public class CommonConfiguration
    {
        protected static IConfiguration __configuration;

        public static string Item(string value)
        {
            return Configuration[value];
        }

        protected static IConfiguration Configuration
        {
            get
            {
                if (__configuration == null)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                    __configuration = builder.Build();
                }

                return __configuration;
            }
        }
    }
}
