// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace Services.Orchestrator.Controllers
// {
//     [Route("api/orchestrator")]
//     [ApiController]
//     public class CustomersController : ControllerBase
//     {
//         [HttpGet("{id}")]
//         public async Task<ActionResult> Get(Guid id)
//         {
//             await Task.Delay(1000);
//             return Ok("Hi");
//         }

//         [HttpGet]
//         public async Task<ActionResult> Get()
//         {
//             await Task.Delay(1000);
//             return Ok("Hi");
//         }
//     }
// }