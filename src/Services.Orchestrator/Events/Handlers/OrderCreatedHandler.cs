using System.Threading.Tasks;
using Shared.Kafka.Consumer;
using Shared.Dto;
using System;
using Shared.Kafka;
using Services.Orchestrator.Commands;
using MediatR;

namespace Services.Orchestrator.Events.Handlers
{
    public class OrderCreatedHandler : IKafkaHandler<string, OrchestratorRequestDTO>
    {
        // private readonly IMediator _mediator;
        // public OrderCreatedHandler(IMediator mediator)
        // {
        //     _mediator = mediator;
        // }

        public async Task HandleAsync(string key, OrchestratorRequestDTO value)
        {
            var command = new UpdateOrderCommand
            {
                OrderId = value.OrderId,
                Status = OrderStatus.ORDER_COMPLETED.ToString()
            };
            // await _mediator.Send(command);

            Console.WriteLine("KLAS");
            await Task.Delay(1000);
        }
    }
}