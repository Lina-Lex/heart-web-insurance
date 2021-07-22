using System.Text.RegularExpressions;

namespace Application.Common.Helpers.FeedbackHelper
{
    public class ValidatePhoneNumber
    {
        public static string Validate(string strPhoneNumber)
        {
            string MatchPhoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            var validation = Regex.IsMatch(strPhoneNumber, MatchPhoneNumberPattern);

            if (validation)
                return strPhoneNumber.Trim().Replace(" ", "").Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "");

            return string.Empty;

        }
    }
}
