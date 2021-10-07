/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Dto;

namespace Services.Orchestrator.Workflow
{
    public class PaymentStep : IWorkflowStep
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _baseUrl;
        public PaymentStep(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _baseUrl = "http://services.payment:5004/api/payment";
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