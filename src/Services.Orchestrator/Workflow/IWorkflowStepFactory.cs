using System.Threading.Tasks;
using Shared.Dto;

namespace Services.Orchestrator.Workflow
{
    public interface IWorkflowStepFactory
    {
        IWorkflowStep GetWorkflowStep(string stepName);
    }
}