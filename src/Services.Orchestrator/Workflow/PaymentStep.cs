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
        private readonly string _baseUrl;
        public PaymentStep(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            // _baseUrl = "http://services.payment:5004/api/payment";
            _baseUrl = "http://localhost:5004/api/payment";
        }

        public async Task<bool> Process(OrchestratorRequestDTO value)
        {
            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    var url = $"{_baseUrl}/debit";
                    var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> Revert(OrchestratorRequestDTO value)
        {
            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    var url = $"{_baseUrl}/credit";
                    var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}