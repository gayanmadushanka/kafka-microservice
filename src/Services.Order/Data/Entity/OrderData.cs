using System;
using System.Text.Json.Serialization;

namespace Services.Order.Data
{
    public class OrderData : BaseEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public string FailedReason { get; set; }
    }
}