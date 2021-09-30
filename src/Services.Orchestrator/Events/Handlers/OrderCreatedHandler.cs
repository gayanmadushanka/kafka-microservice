using System.Threading.Tasks;
using Shared.Kafka.Consumer;
using Shared.Dto;
using System;
using Shared.Kafka;
using Services.Orchestrator.Commands;
using MediatR;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Services.Orchestrator.Workflow;

namespace Services.Orchestrator.Events.Handlers
{
    public class OrderCreatedHandler : IKafkaHandler<string, OrchestratorRequestDTO>
    {
        private readonly IMediator _mediator;
        private readonly IWorkflowStepFactory _workflowStepFactory;
        public OrderCreatedHandler(IMediator mediator, IWorkflowStepFactory workflowStepFactory)
        {
            _mediator = mediator;
            _workflowStepFactory = workflowStepFactory;
        }

        public async Task HandleAsync(string key, OrchestratorRequestDTO value)
        {
            Console.WriteLine("CALLED");

            // var paymentStep = _workflowStepFactory.GetWorkflowStep("Payment");
            // if (!await paymentStep.Process(value))
            // {

            // }

            // var inventoryStep = _workflowStepFactory.GetWorkflowStep("Inventory");
            // if (!await inventoryStep.Process(value))
            // {
            //     await paymentStep.Revert(value);
            // }


            // var command = new UpdateOrderCommand
            // {
            //     OrderId = value.OrderId,
            //     Status = OrderStatus.ORDER_COMPLETED.ToString()
            // };
            // await _mediator.Send(command);
        }
    }
}