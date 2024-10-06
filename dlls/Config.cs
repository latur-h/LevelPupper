using LevelPupper__Parser.dlls.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    internal static class Config
    {
        private static IConfiguration? _configuration;
        private static ServiceProvider? _provider;

        public static void LoadConfigs(string? pathToEnv)
        {
            try
            {
                DotNetEnv.Env.Load(pathToEnv);

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddEnvironmentVariables();

                _configuration = builder.Build();

                _provider = new ServiceCollection().AddSingleton(new API_Pupser_Configuration(
                        Environment.GetEnvironmentVariable("LEVEL"),
                        Environment.GetEnvironmentVariable("LOGIN"),
                        Environment.GetEnvironmentVariable("PASSWORD"),
                        Environment.GetEnvironmentVariable("URL_LOGIN"),
                        Environment.GetEnvironmentVariable("URL_SERVICES"),
                        Environment.GetEnvironmentVariable("URL_GAMESERVICE"),
                        Environment.GetEnvironmentVariable("URL_ELEMENTOFDESCRIPTION"),
                        Environment.GetEnvironmentVariable("URL_RANGEGRADATION"),
                        Environment.GetEnvironmentVariable("URL_VALUEOPTION")))
                    .AddOptions()
                    .BuildServiceProvider();
            }
            catch (Exception ex)
            {
                RTConsole.Write(ex.Message, Color.Red);
            }
        }

        public static API_Pupser_Configuration? GetAPI() => _provider?.GetService<API_Pupser_Configuration>();
    }
}
