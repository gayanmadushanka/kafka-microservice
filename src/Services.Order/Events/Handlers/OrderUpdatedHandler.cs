/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Threading.Tasks;
using Services.Order.Data;
using Shared.Dto;
using Shared.Kafka.Consumer;

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
            order.FailedReason = value.FailedReason;
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}