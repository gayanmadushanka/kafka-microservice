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

namespace Services.Orchestrator.Events.Handlers
{
    public class OrderCreatedHandler : IKafkaHandler<string, OrchestratorRequestDTO>
    {
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;
        public OrderCreatedHandler(IMediator mediator, IHttpClientFactory clientFactory)
        {
            _mediator = mediator;
            _clientFactory = clientFactory;
        }

        public async Task HandleAsync(string key, OrchestratorRequestDTO value)
        {
            Console.WriteLine("CALLED");

            string json = JsonConvert.SerializeObject(value);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = _clientFactory.CreateClient())
            {
                var url = "http://localhost:5003/api/payment/debit";
                var response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("SUCCEDE");
                }
                else
                {
                    Console.WriteLine("FAILED");
                }
            }

            // var command = new UpdateOrderCommand
            // {
            //     OrderId = value.OrderId,
            //     Status = OrderStatus.ORDER_COMPLETED.ToString()
            // };
            // await _mediator.Send(command);

        }
    }
}