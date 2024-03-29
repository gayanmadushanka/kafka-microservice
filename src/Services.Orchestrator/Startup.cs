/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Orchestrator.Commands.Handlers;
using Services.Orchestrator.Events.Handlers;
using Services.Orchestrator.Workflow;
using Shared.Dto;
using Shared.Kafka;

namespace Services.Orchestrator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IWorkflowStepFactory, WorkflowStepFactory>();
            services.AddMediatR(typeof(UpdateOrderCommandHandler).GetTypeInfo().Assembly);
            services.AddKafkaMessageBus();
            services.AddKafkaProducer<string, OrchestratorResponseDTO>(p =>
            {
                p.Topic = "order-updated";
                p.BootstrapServers = "kafka:29092";
            });
            services.AddKafkaConsumer<string, OrchestratorRequestDTO, OrderCreatedHandler>(p =>
            {
                p.Topic = "order-created";
                p.GroupId = "orders-created-group";
                p.BootstrapServers = "kafka:29092";
                p.AllowAutoCreateTopics = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
