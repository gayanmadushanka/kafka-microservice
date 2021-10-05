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