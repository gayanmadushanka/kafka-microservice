using System;

namespace Services.Order.Data
{
    public class OrderData
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
    }
}