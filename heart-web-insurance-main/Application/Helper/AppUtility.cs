using Application.Common.Exceptions;

namespace Application.Helper
{
    public class AppUtility
    {
        public static void BrokerFailureMessage(string message)
        {
            throw new CustomException(message);
        }
    }
}
