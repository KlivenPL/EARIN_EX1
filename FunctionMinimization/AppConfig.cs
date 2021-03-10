using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FunctionMinimization
{
    public static class AppConfig
    {
        public static int MaxExecutionTimeInMs => int.Parse(GetSetting(nameof(MaxExecutionTimeInMs)));
        public static double DesiredPrecision => double.Parse(GetSetting(nameof(DesiredPrecision)));

        private readonly static IConfiguration Configuration;

        static AppConfig()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath(@$"{Environment.CurrentDirectory}\\appsettings.json"), optional: false, reloadOnChange: true)
                .Build();
        }

        private static string GetSetting(string name)
        {
            return Configuration.GetSection("Settings")[name];
        }
    }
}
