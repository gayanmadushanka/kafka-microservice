using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Services.Payment.Commands;

namespace Services.Payment.Controllers
{
    [Route("api/payment")]
    [ApiController]
    [AllowAnonymous]
    public class PaymentController : ControllerBase
    {
        [HttpPost("debit")]
        public async Task<ActionResult> Debit([FromBody] DebitPaymentCommand command)
        {
            Console.WriteLine("Debit CALLED");
            await Task.Delay(1000);
            return Ok();
            // return BadRequest();
        }

        [HttpPost("credit")]
        public async Task<ActionResult> Credit([FromBody] CreditPaymentCommand command)
        {
            Console.WriteLine("Credit CALLED");
            await Task.Delay(1000);
            return Ok();
        }
    }
}