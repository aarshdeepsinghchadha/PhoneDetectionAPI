using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberDetection.Common.Interface
{
    /// <summary>
    /// Interface for detecting phone numbers within text.
    /// </summary>
    public interface IPhoneNumberDetector
    {
        /// <summary>
        /// Detects phone numbers within the provided input text.
        /// </summary>
        /// <param name="inputText">The text in which phone numbers are to be detected.</param>
        /// <returns>A list of strings representing the detected phone numbers.</returns>
        List<string> DetectPhoneNumbers(string inputText);
    }

}
