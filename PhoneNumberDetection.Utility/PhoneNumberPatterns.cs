using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneNumberDetection.Utility
{
    /// <summary>
    /// Utility class to convert words representing digits into their corresponding numerical digits.
    /// Supports conversions for English and Hindi languages.
    /// </summary>
    public static class PhoneNumberPatterns
    {
        // Dictionary mapping words to their respective digit representations
        private static readonly Dictionary<string, string> WordToDigitMap = new Dictionary<string, string>
        {
            // English words to digits mapping
            { "zero", "0" }, { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" },
            { "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" }, { "nine", "9" },
        
            // Hindi words to digits mapping
            { "एक", "1" }, { "दो", "2" }, { "तीन", "3" }, { "चार", "4" }, { "पांच", "5" },
            { "छह", "6" }, { "सात", "7" }, { "आठ", "8" }, { "नौ", "9" }, { "शू", "0" },
        
            // Uppercase English words to digits mapping
            { "ONE", "1" }, { "TWO", "2" }, { "THREE", "3" }, { "FOUR", "4" }, { "FIVE", "5" },
            { "SIX", "6" }, { "SEVEN", "7" }, { "EIGHT", "8" }, { "NINE", "9" }, { "ZERO", "0" }
        };

        /// <summary>
        /// Converts words representing digits in the input text to their corresponding numerical digits.
        /// </summary>
        /// <param name="input">The input text containing words to convert.</param>
        /// <returns>The input text with words converted to digits.</returns>
        //public static string ConvertWordsToDigits(string input)
        //{
        //    foreach (var pair in WordToDigitMap)
        //    {
        //        // Replace whole words (using \b for word boundary) with their corresponding digit surrounded by spaces
        //        input = Regex.Replace(input, @"\b" + pair.Key + @"\b", " " + pair.Value + " ", RegexOptions.IgnoreCase);
        //    }
        //    return input;
        //}

        public static string ConvertWordsToDigits(string input)
        {
            // Iterate through each word-to-digit mapping
            foreach (var pair in WordToDigitMap)
            {
                // Adjust regex pattern to handle Hindi words with optional spaces or punctuation around them
                string pattern = @"(?<!\p{L})" + Regex.Escape(pair.Key) + @"(?!\p{L})";

                // Replace words with their corresponding digit surrounded by spaces
                input = Regex.Replace(input, pattern, " " + pair.Value + " ", RegexOptions.IgnoreCase);
            }

            // Replace multiple spaces with a single space
            input = Regex.Replace(input, @"\s+", " ");

            // Trim leading and trailing spaces
            return input.Trim();
        }

    }

}
