using System.Globalization;

namespace Restaurent.Manager
{
    public static class CurrencyFormat
    {
        public static string FormatCurrency(this double amount)
        {
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            return string.Format(cultureInfo, "{0:C0}", amount);
        }
    }
}
