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
using System.Collections.Generic;

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
            Console.WriteLine("OrderCreatedHandler Called");
            var paymentStep = _workflowStepFactory.GetWorkflowStep("Payment");
            var inventoryStep = _workflowStepFactory.GetWorkflowStep("Inventory");
            var processTasks = new List<Task<bool>>()
            {
                paymentStep.Process(value),
                inventoryStep.Process(value)
            };
            var processTasksResults = await Task.WhenAll(processTasks);
            if (IsAllSucceed(processTasksResults))
            {
                await SendUpdateOrderCommand(value.OrderId, OrderStatus.ORDER_COMPLETED);
                Console.WriteLine("ORDER_COMPLETED");
                Console.WriteLine("------------------");
                return;
            }
            await Retry(10, async () =>
            {
                var revertTasks = new List<Task<bool>>()
                {
                    paymentStep.Revert(value),
                    inventoryStep.Revert(value)
                };
                var revertTasksResults = await Task.WhenAll(revertTasks);
                return IsAllSucceed(revertTasksResults);
            });
            await SendUpdateOrderCommand(value.OrderId, OrderStatus.ORDER_CANCELLED);
            Console.WriteLine("ORDER_CANCELLED");
            Console.WriteLine("------------------");
            return;
        }

        private async Task SendUpdateOrderCommand(Guid orderId, OrderStatus status)
        {
            await _mediator.Send(new UpdateOrderCommand
            {
                OrderId = orderId,
                Status = status.ToString()
            });
        }

        private bool IsAllSucceed(bool[] results)
        {
            foreach (var result in results)
            {
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task Retry(int count, Func<Task<bool>> func)
        {
            int i = 0;
            do
            {
                if (await func())
                {
                    break;
                }
                i++;
            } while (i < count);
            return;
        }
    }
}