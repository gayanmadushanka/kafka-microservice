/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

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