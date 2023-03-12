using MediatR;

namespace French.Amortization.Api.Loans.Queries
{
    public class FetchLoanQuery : IRequest<FetchLoanQueryResponse>
    {
        public double RequestedValue { get; set; }
        public DateTime FirstExpiration { get; set; }
        public int InstallmentQuantity { get; set; }
        public double MonthlyInterestRate { get; set; }
    }
}
