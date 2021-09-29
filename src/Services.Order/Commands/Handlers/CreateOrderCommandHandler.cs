using System.Threading.Tasks;
using System;
using System.Threading;
using Services.Order.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Kafka;

namespace Services.Order.Commands.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderData>
    {
        private readonly OrderDBContext _dbContext;
        private readonly IKafkaMessageBus<int, OrderData> _bus;

        public CreateOrderCommandHandler(OrderDBContext dbContext, IKafkaMessageBus<int, OrderData> bus)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        public async Task<OrderData> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.Orders.AsNoTracking().AnyAsync(s => s.UserId == command.UserId))
                throw new ApplicationException("User is already exist.");

            var order = new OrderData
            {
                Id = command.Id,
                UserId = command.UserId,
                ProductId = command.ProductId,
                Price = 100,
                Status = OrderStatus.ORDER_CREATED.ToString()
            };

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            await _bus.PublishAsync(command.UserId, order);

            return order;
        }
    }
}
