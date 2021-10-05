using System.Threading.Tasks;
using Shared.Dto;

namespace Services.Orchestrator.Workflow
{
    public interface IWorkflowStep
    {
        Task<bool> Process(OrchestratorRequestDTO value);
        Task<bool> Revert(OrchestratorRequestDTO value);
    }
}