namespace Application.Common.Responses
{
    public class ResponseCodes
    {
        private const string CODE_PREFIX = "SERVICE_CODE:";
        public string ResponseStatusCodes { get; set; }
        public string StatusDescription { get; set; }
        public ResponseCodes()
        {}
        public ResponseCodes(string statusCode)
        {
            ResponseStatusCodes = statusCode;
        }

        public const string USER_ALREADY_EXIST = CODE_PREFIX + "01";
        public const string SUCCESSFUL = CODE_PREFIX + "00";
        public const string FAILED = CODE_PREFIX + "11";
        public const string USER_NOTFOUND = CODE_PREFIX + "40";
    }

    public class ResponseStatusDescription
    {
        public static string GetMessage(ResponseCodes responeStatus)
        {
            if(!responeStatus.Equals(null))
            {
                if (responeStatus.ResponseStatusCodes.Contains("01"))
                    return string.Format(NormalDescriptionToString(responeStatus.ResponseStatusCodes));
                if (responeStatus.ResponseStatusCodes.Contains("00"))
                    return string.Format(NormalDescriptionToString(responeStatus.ResponseStatusCodes));
                if (responeStatus.ResponseStatusCodes.Contains("11"))
                    return string.Format(NormalDescriptionToString(responeStatus.ResponseStatusCodes));
                if (responeStatus.ResponseStatusCodes.Contains("40"))
                    return string.Format(NormalDescriptionToString(responeStatus.ResponseStatusCodes));
            }
            return string.Empty;
        }

        static internal string NormalDescriptionToString(string errorCode)
        {
            if (string.IsNullOrEmpty(errorCode))
            {
                var newString = errorCode.Replace("_", "").ToLower();
                var firstChar = newString[..].ToUpper();
                return string.Concat(firstChar, newString);
            }
            return string.Empty;
        }
    }

}
