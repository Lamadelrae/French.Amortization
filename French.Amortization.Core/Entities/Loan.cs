namespace French.Amortization.Core.Entities
{
    public class Loan
    {
        public double RequestedValue { get; private set; }
        public DateTime FirstExpiration { get; private set; }
        public int InstallmentQuantity { get; private set; }
        public double MonthlyInterestRate { get; private set; }

        //Calculated Fields
        public double TotalFinancedValue => MonthlyPaymentValue * InstallmentQuantity;
        public double MonthlyPaymentValue => RequestedValue * MonthlyInterestRate * Math.Pow(1 + MonthlyInterestRate, InstallmentQuantity) / (Math.Pow(1 + MonthlyInterestRate, InstallmentQuantity) - 1);
        public double YearlyInterestRate => (Math.Pow(MonthlyInterestRate / 100 + 1, 12) - 1) * 100;
        public DateTime LastExpiration => FirstExpiration.AddMonths(InstallmentQuantity);
        public List<Installment> Installments => Installment.Generate(this);

        public Loan(double requestedValue, DateTime firstExpiration, int installmentQuantity, double monthlyInterestRate)
        {
            RequestedValue = requestedValue;
            FirstExpiration = firstExpiration;
            InstallmentQuantity = installmentQuantity;
            MonthlyInterestRate = monthlyInterestRate;
        }
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
                    Principal = principal,
                    Interest = interest,
                    TotalValue = loan.MonthlyPaymentValue,
                    Expiration = expiration,
                });
                expiration = expiration.AddMonths(1);
            }

            return result;
        }
    }
}
