using Application.Common.Utilities;
using System;
using System.Text;

namespace Application.Common.Helpers.PasswordHelper
{
    public class FirstTimePasswordGeneration
    {
        static readonly Random random = new Random();
        static readonly string chars = Util.CHAR;
        public static string CreatePassword(string email)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Util.LENGTH; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
                sb.Append(email.ToLowerInvariant());
            }
            return sb.ToString();
        }



    }
}
