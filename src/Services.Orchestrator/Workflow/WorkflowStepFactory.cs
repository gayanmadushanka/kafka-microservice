/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System.Net.Http;

namespace Services.Orchestrator.Workflow
{
    public class WorkflowStepFactory : IWorkflowStepFactory
    {
        private readonly IHttpClientFactory _clientFactory;
        public WorkflowStepFactory(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IWorkflowStep GetWorkflowStep(string stepName)
        {
            if (stepName == "Payment")
            {
                return new PaymentStep(_clientFactory);
            }
            else if (stepName == "Inventory")
            {
                return new InventoryStep(_clientFactory);
            }
            return null;
        }
    }
}