using PhoneNumberDetection.Common.Interface;
using PhoneNumberDetection.Utility;
using System.Text;
using System.Text.RegularExpressions;

namespace PhoneNumberDetection.Services
{
    /// <summary>
    /// Implementation of the <see cref="IPhoneNumberDetector"/> interface to detect phone numbers in a given text.
    /// </summary>
    public class PhoneNumberDetector : IPhoneNumberDetector
    {
        /// <summary>
        /// Detects phone numbers in the provided input text.
        /// </summary>
        /// <param name="inputText">The text in which phone numbers are to be detected.</param>
        /// <returns>A list of detected phone numbers.</returns>
        public List<string> DetectPhoneNumbers(string inputText)
        {
            var detectedNumbers = new List<string>();

            // Convert words to digits and trim the result
            string nonTrim = PhoneNumberPatterns.ConvertWordsToDigits(inputText).Trim();
            string convertedInput = Regex.Replace(nonTrim, @"\s+", ""); // Remove all whitespace characters

            //// Regex pattern to match phone numbers with various formats
            //string patternWithFormat = @"(?:\+\d{1,2}\s?)?(?:\(\d{1,4}\)\s?)?\d{1,4}[-\s]?\d{1,4}[-\s]?\d{4,}";
            string patternWithFormat = @"(?:\+\d{1,2}\s?)?(?:\(\d{1,4}\)\s?)?\d{1,4}(?:[-\s]?\d{1,4}){2,}";


            // Regex pattern to match plain 10-digit numbers
            string patternWithoutFormat = @"\b\d{10}\b";

            // Regex pattern to match emergency number formats
            string patternEmergency = @"\b(?:\+?\d{1,2}-)?(?:911|100|108)\b";

            // Find all matches with formatted numbers
            var matchesWithFormat = Regex.Matches(convertedInput, patternWithFormat);
            foreach (Match match in matchesWithFormat)
            {
                // Clean the matched number to keep only digits and essential characters (+()-)
                string cleanedNumber = Regex.Replace(match.Value, @"[^\d+()\-]", "");
                if (Helpers.IsValidPhoneNumber(cleanedNumber) && !detectedNumbers.Contains(cleanedNumber))
                {
                    detectedNumbers.Add(cleanedNumber.Trim());
                }
            }

            // Find all matches with plain 10-digit numbers
            var matchesWithoutFormat = Regex.Matches(convertedInput, patternWithoutFormat);
            foreach (Match match in matchesWithoutFormat)
            {
                // Keep the number as is for plain digits
                string cleanedNumber = match.Value;
                if (Helpers.IsValidPhoneNumber(cleanedNumber) && !detectedNumbers.Contains(cleanedNumber))
                {
                    detectedNumbers.Add(cleanedNumber);
                }
            }

            // Find emergency numbers
            var matchesEmergency = Regex.Matches(convertedInput, patternEmergency);
            foreach (Match match in matchesEmergency)
            {
                // Add emergency numbers if not already detected
                if (!detectedNumbers.Contains(match.Value))
                {
                    detectedNumbers.Add(match.Value.Trim());
                }
            }

            // Define specific emergency numbers to look for
            string[] emergencyNumbers = { "100", "108" };

            // Iterate through each emergency number and find standalone occurrences
            foreach (var number in emergencyNumbers)
            {
                int index = 0;
                while ((index = convertedInput.IndexOf(number, index)) != -1)
                {
                    // Check if the match is standalone (not part of a larger number or word)
                    if (Helpers.IsStandaloneNumber(convertedInput, index, number.Length))
                    {
                        if (!detectedNumbers.Contains(number))
                        {
                            detectedNumbers.Add(number);
                        }
                    }
                    index += number.Length; // Move index forward to continue searching
                }
            }

            return detectedNumbers;
        }
    }

}
