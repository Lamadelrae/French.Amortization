using French.Amortization.Api.Shared.Extensions;

namespace French.Amortization.Api.Loan
{
    public class Loan
    {
        public double RequestedValue { get; private set; }
        public DateTime FirstExpiration { get; private set; }
        public int InstallmentQuantity { get; private set; }
        public double MonthlyInterestRate { get; private set; }

        public double TotalFinancedValue => (MonthlyPaymentValue * InstallmentQuantity).ToRounded(2);
        public double MonthlyPaymentValue
        {
            get
            {
                var monthlyInterestRate = MonthlyInterestRate / 100;

                return
                    (
                        RequestedValue *
                        monthlyInterestRate *
                        (Math.Pow(1 + monthlyInterestRate, InstallmentQuantity) / (Math.Pow(1 + monthlyInterestRate, InstallmentQuantity) - 1))
                    ).ToRounded(2);
            }
        }
        public DateTime LastExpiration => FirstExpiration.AddMonths(InstallmentQuantity);
        public List<Installment> Installments => Installment.Generate(this);

        public Loan(double requestedValue, DateTime firstExpiration, int installmentQuantity, double monthlyInterestRate)
        {
            RequestedValue = requestedValue;
            FirstExpiration = firstExpiration;
            InstallmentQuantity = installmentQuantity;
            MonthlyInterestRate = monthlyInterestRate;
        }

        public class Installment
        {
            public string Description { get; private set; } = string.Empty;
            public DateTime Expiration { get; private set; }
            public double Principal { get; private set; }
            public double Interest { get; private set; }
            public double TotalValue { get; private set; }

            public static List<Installment> Generate(Loan loan)
            {
                var result = new List<Installment>();

                var expiration = loan.FirstExpiration;
                var balance = loan.RequestedValue;
                for (int i = 1; i <= loan.InstallmentQuantity; i++)
                {
                    var interest = loan.MonthlyInterestRate / 100 * balance;
                    var principal = loan.MonthlyPaymentValue - interest;
                    balance -= principal;

                    result.Add(new Installment
                    {
                        Description = $"Installment {i} out of {loan.InstallmentQuantity}",
                        Principal = principal.ToRounded(2),
                        Interest = interest.ToRounded(2),
                        TotalValue = loan.MonthlyPaymentValue,
                        Expiration = expiration,
                    });
                    expiration = expiration.AddMonths(1);
                }

                return result;
            }
        }
    }
}
