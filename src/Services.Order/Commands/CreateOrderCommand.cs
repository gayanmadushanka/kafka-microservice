/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using MediatR;
using Services.Order.Data;

namespace Services.Order.Commands
{
    public class CreateOrderCommand : IRequest<OrderData>
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }

        public CreateOrderCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}