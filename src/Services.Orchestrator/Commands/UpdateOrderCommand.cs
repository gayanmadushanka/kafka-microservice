
using System;
using MediatR;

namespace Services.Orchestrator.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
    }
}