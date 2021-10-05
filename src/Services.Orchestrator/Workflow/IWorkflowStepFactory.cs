/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System.Threading.Tasks;
using Shared.Dto;

namespace Services.Orchestrator.Workflow
{
    public interface IWorkflowStepFactory
    {
        IWorkflowStep GetWorkflowStep(string stepName);
    }
}