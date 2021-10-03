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
using Services.Order.Commands.Handlers;
using Services.Order.Events.Handlers;
using Services.Order.Data;
using Shared.Kafka;
using Shared.Dto;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace Services.Order
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
                   services.AddKafkaConsumer<string, OrchestratorResponseDTO, OrderUpdatedHandler>(p =>
                   {
                       p.Topic = "order-updated";
                       p.GroupId = "orders-updated-group";
                       p.BootstrapServers = "kafka:29092";
                       p.AllowAutoCreateTopics = true;
                   })
                );
    }
}
