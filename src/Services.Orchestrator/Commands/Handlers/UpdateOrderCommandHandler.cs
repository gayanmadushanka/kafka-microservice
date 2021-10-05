/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shared.Dto;
using Shared.Kafka;

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
            Console.WriteLine("UpdateOrderCommandHandler Called");
            var orchestratorResponseDTO = new OrchestratorResponseDTO
            {
                OrderId = value.OrderId,
                Status = value.Status,
                FailedReason = value.FailedReason
            };
            await _bus.PublishAsync(value.OrderId.ToString(), orchestratorResponseDTO);
        }
    }
}