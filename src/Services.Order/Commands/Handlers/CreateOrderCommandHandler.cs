using System.Threading.Tasks;
using System;
using System.Threading;
using Services.Order.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Kafka;

namespace Services.Order.Commands.Handlers
{
    public class CreateOrderCommandHandler : AsyncRequestHandler<CreateOrderCommand>
    {
        private readonly OrderDBContext _dbContext;
        private readonly IKafkaMessageBus<int, OrderData> _bus;

        public CreateOrderCommandHandler(OrderDBContext dbContext, IKafkaMessageBus<int, OrderData> bus)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        protected override async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (await _dbContext.Orders.AsNoTracking().AnyAsync(s => s.UserId == command.UserId))
                throw new ApplicationException("User is already exist.");

            var order = new OrderData
            {
                Id = command.Id,
                UserId = command.UserId,
                ProductId = command.ProductId,
                Price = command.Price,
            };

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();

            await _bus.PublishAsync(command.UserId, order);
        }
    }
}
