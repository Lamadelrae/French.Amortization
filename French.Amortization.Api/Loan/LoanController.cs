using French.Amortization.Api.Loan.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace French.Amortization.Api.Loan
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

        [HttpGet]
        public async Task<IActionResult> FetchLoan([FromQuery] FetchLoanQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
