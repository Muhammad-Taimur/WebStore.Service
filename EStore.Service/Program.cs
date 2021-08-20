using System;
using System.IO;
using EStore.Contracts;
using EStore.Service.Services;
using EStore.Service.Services.Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using Serilog;

namespace EStore.Service
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }

        public static void Main(string[] args)
        {
            //NService Bus Configuration
            var endpointConfiguration = new EndpointConfiguration("EStore.Service");
            endpointConfiguration.EnableInstallers();

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");

            //var delayedDelivery = transport.DelayedDelivery();
            //delayedDelivery.DisableTimeoutManager();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(ProductCommand), "Sales");

            var endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Bus Started...");

            Configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", false, true)
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
                  true)
              .AddCommandLine(args)
              .AddEnvironmentVariables()
              .Build();

            // Configure serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            //Configuration = new ConfigurationBuilder()
            //   .SetBasePath(Directory.GetCurrentDirectory())
            //   .AddJsonFile("appsettings.json", false, true)
            //   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true,
            //       true)
            //   .AddCommandLine(args)
            //   .AddEnvironmentVariables()
            //   .Build();

            //This is working
            //Log.Logger = new LoggerConfiguration()
            //    .Enrich.FromLogContext()
            //    .WriteTo.File("C:\\logs\\WebStore-.json", rollingInterval: RollingInterval.Day)
            //    .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            //    .CreateLogger();


            Log.Information("EStore Service is Starting up...");
                CreateHostBuilder(args).Build().Run();
        }

    //    public static void ConfigureBusServices()
    //    {
    //        endpointConfiguration.RegisterComponents(
    //registration: configureComponents =>
    //{
    //    configureComponents.ConfigureComponent<MyService>(DependencyLifecycle.InstancePerCall);
    //});
    //    }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(hostBuilderContext =>
                {
                    var endpointConfiguration = new EndpointConfiguration("EStore.Service");
                    //NService Bus Configuration
                    endpointConfiguration.EnableInstallers();

                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

                    transport.UseConventionalRoutingTopology();
                    transport.ConnectionString("host=localhost");

                    var routing = transport.Routing();
                    routing.RouteToEndpoint(typeof(ProductCommand), "Sales");
                    //var endpointInstance =  Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
                    Console.WriteLine("Bus Started...");
                    //endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());

                    //                endpointConfiguration.RegisterComponents(
                    //registration: configureComponents =>
                    //{
                    //    configureComponents.ConfigureComponent<IEndpointInstance>(DependencyLifecycle.InstancePerCall);
                    //});
                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseConfiguration(Configuration)
                    .UseSerilog();
                }); 
    }
}
