
/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using MediatR;

namespace Services.Orchestrator.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public string FailedReason { get; set; }
    }
}