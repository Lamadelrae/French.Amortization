using French.Amortization.Api.Loans.Queries;
using MediatR;

namespace French.Amortization.Api.Loans
{
    public class LoanHandler : IRequestHandler<CreateLoanCommand, CreateLoanResponse>
    {
        public Task<CreateLoanResponse> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = new Loan(request.RequestedValue, request.FirstExpiration, request.InstallmentQuantity, request.MonthlyInterestRate);
            return Task.FromResult(CreateLoanResponse.FromEntity(loan));
        }
    }
}
