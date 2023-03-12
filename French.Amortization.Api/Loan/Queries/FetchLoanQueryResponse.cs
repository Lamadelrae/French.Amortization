namespace French.Amortization.Api.Loan.Queries
{
    public class FetchLoanQueryResponse
    {
        public double TotalFinancedValue { get; set; }
        public double MonthlyInterestRate { get; set; }
        public FetchLoanQueryResponseDates Dates { get; set; }
        public IEnumerable<FetchLoanQueryResponseInstallment> Installments { get; set; }

        public static FetchLoanQueryResponse FromEntity(Loan loan) =>
            new FetchLoanQueryResponse()
            {
                TotalFinancedValue = loan.TotalFinancedValue,
                MonthlyInterestRate = loan.MonthlyInterestRate,
                Dates = new FetchLoanQueryResponseDates()
                {
                    From = loan.FirstExpiration,
                    To = loan.LastExpiration,
                },
                Installments = loan.Installments.Select(installment => new FetchLoanQueryResponseInstallment()
                {
                    Description = installment.Description,
                    Expiration = installment.Expiration,
                    Principal = installment.Principal,
                    Interest = installment.Interest,
                    TotalValue = installment.TotalValue,
                })
            };

        public class FetchLoanQueryResponseDates
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }

        public class FetchLoanQueryResponseInstallment
        {
            public string Description { get; set; } = string.Empty;
            public DateTime Expiration { get; set; }
            public double Principal { get; set; }
            public double Interest { get; set; }
            public double TotalValue { get; set; }
        }
    }
}
