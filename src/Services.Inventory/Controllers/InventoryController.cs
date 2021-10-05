/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Inventory.Commands;

namespace Services.Inventory.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    [AllowAnonymous]
    public class InventoryController : ControllerBase
    {
        [HttpPost("deduct")]
        public async Task<ActionResult> Deduct([FromBody] DeductInventoryCommand command)
        {
            Console.WriteLine("Inventory Deduct Succeed");
            return Ok();
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddInventoryCommand command)
        {
            Console.WriteLine("Inventory Add Succeed");
            return Ok();
        }
    }
}