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
        public InventoryStep(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> Process(OrchestratorRequestDTO value)
        {
            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    var url = "http://localhost:5003/api/Inventory/deduct";
                    var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Revert(OrchestratorRequestDTO value)
        {
            try
            {
                using (var client = _clientFactory.CreateClient())
                {
                    var url = "http://localhost:5003/api/Inventory/add";
                    var data = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}