using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppRecoveryServer
{
    public static class Configuration
    {
        public static void InitConfiguration(IConfigurationRoot configuration)
        {
            ConnectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
        }

        public static String ConnectionString { get; private set; }
    }
}
