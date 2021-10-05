
using System;
using MediatR;

namespace Services.Payment.Commands
{
    public class DebitPaymentCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}