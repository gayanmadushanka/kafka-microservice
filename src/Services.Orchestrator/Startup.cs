using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Orchestrator.Events.Handlers;
using Services.Orchestrator.Commands.Handlers;
using Shared.Kafka;
using Shared.Dto;
using MediatR;
using System.Reflection;

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
            // services.AddMediatR(typeof(UpdateOrderCommandHandler).GetTypeInfo().Assembly);

            services.AddControllers();

            // services.AddKafkaMessageBus();

            services.AddKafkaConsumer<string, OrchestratorRequestDTO, OrderCreatedHandler>(p =>
            {
                p.Topic = "order-created";
                p.GroupId = "orders-created-group";
                p.BootstrapServers = "localhost:9092";
                p.AllowAutoCreateTopics = true;
            });

            // services.AddKafkaProducer<string, OrchestratorResponseDTO>(p =>
            // {
            //     p.Topic = "order-updated";
            //     p.BootstrapServers = "localhost:9092";
            // });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
