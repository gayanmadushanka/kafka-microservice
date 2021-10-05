/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
namespace Shared.Dto
{
    public class OrchestratorResponseDTO
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public string FailedReason { get; set; }
    }
}