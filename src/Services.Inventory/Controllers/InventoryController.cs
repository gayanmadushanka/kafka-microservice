using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            await Task.Delay(1000);
            return Ok("Deduct");
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] AddInventoryCommand command)
        {
            await Task.Delay(1000);
            return Ok("Add");
        }
    }
}