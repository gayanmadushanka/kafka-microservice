using System.Threading.Tasks;
using Shared.Kafka.Consumer;
using Shared.Dto;
using System;

namespace Services.Orchestrator.Handlers
{
    public class OrderOrchestratorHandler : IKafkaHandler<int, OrchestratorRequestDTO>
    {
        public async Task HandleAsync(int key, OrchestratorRequestDTO value)
        {
            Console.WriteLine("AZXD");
            await Task.Delay(1000);
        }
    }
}