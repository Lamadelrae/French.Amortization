using French.Amortization.Api.Loans.Queries;
using MediatR;

namespace French.Amortization.Api.Loans
{
    public class LoanHandler : IRequestHandler<FetchLoanQuery, FetchLoanQueryResponse>
    {
        public Task<FetchLoanQueryResponse> Handle(FetchLoanQuery request, CancellationToken cancellationToken)
        {
            var loan = new Loan(request.RequestedValue, request.FirstExpiration, request.InstallmentQuantity, request.MonthlyInterestRate);
            return Task.FromResult(FetchLoanQueryResponse.FromEntity(loan));
        }
    }
}
