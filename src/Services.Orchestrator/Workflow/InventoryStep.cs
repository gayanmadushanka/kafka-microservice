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
    public class InventoryStep : IWorkflowStep
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUrl;
        public InventoryStep(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _baseUrl = "http://services.inventory:5003/api/Inventory";
        }

        public async Task<bool> Process(OrchestratorRequestDTO value)
        {
            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    var url = $"{_baseUrl}/deduct";
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
                    var url = $"{_baseUrl}/add";
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