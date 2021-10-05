/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Order.Commands;
using Services.Order.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace Services.Order.Controllers
{
    [Route("api/order")]
    [ApiController]
    [AllowAnonymous]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly OrderDBContext _dbContext;

        public OrderController(IMediator mediator, OrderDBContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return Ok(await _dbContext.Orders.FindAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _dbContext.Orders.ToListAsync());
        }
    }
}