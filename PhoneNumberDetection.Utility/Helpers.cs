using System.Text.RegularExpressions;

namespace PhoneNumberDetection.Utility
{
    /// <summary>
    /// Utility class containing helper methods for phone number detection and validation.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Checks if a matched number is standalone in the input text.
        /// </summary>
        /// <param name="input">The input text where the number was found.</param>
        /// <param name="startIndex">The starting index of the matched number.</param>
        /// <param name="length">The length of the matched number.</param>
        /// <returns>True if the matched number is standalone, otherwise false.</returns>
        public static bool IsStandaloneNumber(string input, int startIndex, int length)
        {
            // Check if the match is surrounded by non-digit characters or is at the start/end of the input
            if ((startIndex == 0 || !char.IsDigit(input[startIndex - 1])) &&
                (startIndex + length == input.Length || !char.IsDigit(input[startIndex + length])))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Validates if the provided phone number has at least 10 digits.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>True if the phone number is valid (has at least 10 digits), otherwise false.</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Remove non-digit characters and check the length
            string digitsOnly = Regex.Replace(phoneNumber, @"[^\d]", "");
            return digitsOnly.Length >= 10;
        }

    }
}
