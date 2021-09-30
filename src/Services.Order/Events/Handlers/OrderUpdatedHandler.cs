using System.Threading.Tasks;
using Shared.Kafka.Consumer;
using Shared.Dto;
using System;
using System.Threading;
using Services.Order.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Kafka;

namespace Services.Order.Events.Handlers
{
    public class OrderUpdatedHandler : IKafkaHandler<string, OrchestratorResponseDTO>
    {
        private readonly OrderDBContext _dbContext;
        public OrderUpdatedHandler(OrderDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(string key, OrchestratorResponseDTO value)
        {
            Console.WriteLine("OrderUpdatedHandler Called");
            var order = await _dbContext.Orders.FindAsync(value.OrderId);
            order.Status = value.Status;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}