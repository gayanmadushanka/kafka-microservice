using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Orchestrator.Commands.Handlers;
using Services.Orchestrator.Events.Handlers;
using Shared.Kafka;
using Shared.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace Services.Orchestrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });


        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .ConfigureServices(services =>
                               services.AddKafkaConsumer<string, OrchestratorRequestDTO, OrderCreatedHandler>(p =>
                               {
                                   p.Topic = "order-created";
                                   p.GroupId = "orders-created-group";
                                   p.BootstrapServers = "kafka:29092";
                                   //    p.BootstrapServers = "localhost:9092";
                                   p.AllowAutoCreateTopics = true;
                               })
                    );
    }
}