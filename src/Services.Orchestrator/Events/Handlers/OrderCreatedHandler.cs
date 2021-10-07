/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Services.Orchestrator.Commands;
using Services.Orchestrator.Workflow;
using Shared.Dto;
using Shared.Kafka.Consumer;

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
            await Retry(2, async () =>
            {
                var revertTasks = new List<Task<bool>>()
                {
                    paymentStep.Revert(value),
                    inventoryStep.Revert(value)
                };
                var revertTasksResults = await Task.WhenAll(revertTasks);
                return IsAllSucceed(revertTasksResults);
            });
            await SendUpdateOrderCommand(value.OrderId, OrderStatus.ORDER_CANCELLED, "Order transaction failed due to insufficient account balance.");
            Console.WriteLine("ORDER_CANCELLED");
            Console.WriteLine("------------------");
            return;
        }

        private async Task SendUpdateOrderCommand(Guid orderId, OrderStatus status, string failedReason = null)
        {
            await _mediator.Send(new UpdateOrderCommand
            {
                OrderId = orderId,
                Status = status.ToString(),
                FailedReason = failedReason
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