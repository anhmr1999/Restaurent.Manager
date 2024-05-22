using System.Globalization;
using System.Text;

namespace Restaurent.Manager
{
    public static class Common
    {
        public const string JwtSecretKey = "RestaurentSecretKey_Authentication_supper";

        public static string HashMd5(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string FormatCurrency(this double amount, bool hasUnit = true)
        {
            //CultureInfo cultureInfo = new CultureInfo("vi-VN");
            return $"{string.Format("{0:#,0}", amount)}{(hasUnit? " đ" : "")}";
        }
    }
}
