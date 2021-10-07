/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

namespace Services.Orchestrator.Workflow
{
    public interface IWorkflowStepFactory
    {
        IWorkflowStep GetWorkflowStep(string stepName);
    }
}