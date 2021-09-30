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
            await Task.Delay(1000);
            return Ok("Debit");
        }

        [HttpPost("credit")]
        public async Task<ActionResult> Credit([FromBody] CreditPaymentCommand command)
        {
            await Task.Delay(1000);
            return Ok("Credit");
        }
    }
}