using System;

namespace Shared.Dto
{
    public class OrchestratorResponseDTO
    {
        public Guid OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}