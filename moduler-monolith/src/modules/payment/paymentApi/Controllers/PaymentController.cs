using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace paymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderPayments()
        {
            return Ok(await this._mediator.Send(new paymentApplication.GetOrderPayments.Query()));
        }
    }
}
