
using System;
using MediatR;
using Services.Order.Data;

namespace Services.Order.Commands
{
    public class CreateOrderCommand : IRequest<OrderData>
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }

        public CreateOrderCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}