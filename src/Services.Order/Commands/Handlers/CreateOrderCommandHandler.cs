/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Services.Order.Data;
using Shared.Dto;
using Shared.Kafka;

namespace Services.Order.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderData>
    {
        private readonly OrderDBContext _dbContext;
        private readonly IKafkaMessageBus<string, OrchestratorRequestDTO> _bus;

        public CreateOrderCommandHandler(OrderDBContext dbContext, IKafkaMessageBus<string, OrchestratorRequestDTO> bus)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        public async Task<OrderData> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // if (await _dbContext.Orders.AsNoTracking().AnyAsync(s => s.UserId == command.UserId))
            //     throw new ApplicationException("User is already exist.");
            var order = new OrderData
            {
                Id = command.Id,
                UserId = command.UserId,
                ProductId = command.ProductId,
                Price = command.Price,
                Status = OrderStatus.ORDER_CREATED.ToString()
            };
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            var orchestratorRequestDTO = new OrchestratorRequestDTO
            {
                OrderId = order.Id,
                UserId = order.UserId,
                ProductId = order.ProductId,
                Price = order.Price,
            };
            await _bus.PublishAsync(orchestratorRequestDTO.OrderId.ToString(), orchestratorRequestDTO);
            return order;
        }
    }
}
