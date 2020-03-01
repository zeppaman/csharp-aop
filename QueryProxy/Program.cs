using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using QueryProxy.Model;
using QueryProxy.Proxy;
using System;
using System.Collections.Generic;

namespace QueryProxy
{
    class Program
    {

        static ServiceProvider serviceProvider;
        static  ILogger<Program> logger;

        static void Main(string[] args)
        {
           
            Setup();          

          
            var fruitRepository = serviceProvider.GetService<IFruitRepository>();
            var fruits=fruitRepository.GetFruits("citruses");
            PrintData(fruits);

            Console.ReadKey();
        }

        private static void PrintData(object input)
        {
            var json=JsonConvert.SerializeObject(input, Formatting.Indented);
            logger.LogInformation(json);
        }

        private static void Setup()
        {
            serviceProvider = new ServiceCollection()
                .AddLogging((builder) =>
                {
                    builder
                     .AddFilter("Microsoft", LogLevel.Warning)
                     .AddFilter("System", LogLevel.Warning)
                     .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                     .AddConsole();
                })
                .AddSingleton<IFruitRepository>(ProxyFacotory<IFruitRepository>.Create<IFruitRepository>())
                .BuildServiceProvider();
            logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");
        }
    }
}
