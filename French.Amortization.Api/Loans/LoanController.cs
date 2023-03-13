using French.Amortization.Api.Loans.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace French.Amortization.Api.Loans
{
    [ApiController]
    [Route("loan")]
    public class LoanController : Controller
    {
        public readonly IMediator _mediator;

        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
