using MediatR;

namespace French.Amortization.Api.Loans.Queries
{
    public class CreateLoanCommand : IRequest<CreateLoanResponse>
    {
        public double RequestedValue { get; set; }
        public DateTime FirstExpiration { get; set; }
        public int InstallmentQuantity { get; set; }
        public double MonthlyInterestRate { get; set; }
    }
}
