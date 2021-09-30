using System.Threading.Tasks;
using Shared.Dto;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

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