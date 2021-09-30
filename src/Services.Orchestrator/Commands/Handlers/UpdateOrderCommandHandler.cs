using System.Threading.Tasks;
using Shared.Kafka.Consumer;
using Shared.Dto;
using System;
using Shared.Kafka;
using MediatR;
using System.Threading;
using Services.Orchestrator.Commands;

namespace Services.Orchestrator.Commands.Handlers
{
    public class UpdateOrderCommandHandler : AsyncRequestHandler<UpdateOrderCommand>
    {
        private readonly IKafkaMessageBus<string, OrchestratorResponseDTO> _bus;

        public UpdateOrderCommandHandler(IKafkaMessageBus<string, OrchestratorResponseDTO> bus)
        {
            _bus = bus;
        }

        protected override async Task Handle(UpdateOrderCommand value, CancellationToken cancellationToken)
        {
            var orchestratorResponseDTO = new OrchestratorResponseDTO
            {
                OrderId = value.OrderId,
                Status = value.Status
            };
            Console.WriteLine("DFC");
            await Task.Delay(1000);
            // await _bus.PublishAsync(value.OrderId.ToString(), orchestratorResponseDTO);
        }
    }
}