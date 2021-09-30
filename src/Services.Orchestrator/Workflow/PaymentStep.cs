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

namespace Services.Orchestrator.Workflow
{
    public class PaymentStep : IWorkflowStep
    {
        private readonly IHttpClientFactory _clientFactory;
        public PaymentStep(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> Process(OrchestratorRequestDTO value)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var url = "http://localhost:5003/api/payment/debit";
                var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> Revert(OrchestratorRequestDTO value)
        {
            using (var client = _clientFactory.CreateClient())
            {
                var url = "http://localhost:5003/api/payment/credit";
                var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, data);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}