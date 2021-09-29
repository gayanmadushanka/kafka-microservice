
using System;
using MediatR;

namespace Services.Order.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }

        public CreateOrderCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}