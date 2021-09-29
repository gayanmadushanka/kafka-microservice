using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Order.Commands.Handlers;
using Services.Order.Data;
using Shared.Kafka;
using Shared.Dto;

namespace Services.Order
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
            services.AddDbContext<OrderDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddMediatR(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly);

            services.AddControllers();

            services.AddKafkaMessageBus();

            services.AddKafkaProducer<int, OrchestratorRequestDTO>(p =>
            {
                p.Topic = "orders";
                p.BootstrapServers = "localhost:9092";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DbInitilializer.Initialize(app.ApplicationServices);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
