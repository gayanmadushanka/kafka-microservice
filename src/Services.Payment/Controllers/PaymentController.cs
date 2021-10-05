/*
 * Author: Gayan Madushanka
 * Date: 29/09/2021
 * Copyright © 2021 Mitra Innovation. All rights reserved.
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (command.Price > 1000)
            {
                Console.WriteLine("Payment Debit Failed");
                return BadRequest();
            }
            Console.WriteLine("Payment Debit Succeed");
            return Ok();
        }

        [HttpPost("credit")]
        public async Task<ActionResult> Credit([FromBody] CreditPaymentCommand command)
        {
            Console.WriteLine("Payment Credit Succeed");
            return Ok();
        }
    }
}