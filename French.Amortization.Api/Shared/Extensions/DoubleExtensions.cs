namespace French.Amortization.Api.Shared.Extensions
{
    public static class DoubleExtensions
    {
        public static double ToRounded(this double input, int decimalPlaces)
            => Math.Round(input, decimalPlaces);
    }
}
