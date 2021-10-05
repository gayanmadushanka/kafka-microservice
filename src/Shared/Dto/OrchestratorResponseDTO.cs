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