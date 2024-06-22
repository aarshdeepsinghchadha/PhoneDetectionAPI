using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberDetection.Common.Models
{
    /// <summary>
    /// Represents the response object for detecting phone numbers in text.
    /// </summary>
    public class PhoneNumberDetectionResponse
    {
        /// <summary>
        /// Gets or sets the list of detected phone numbers.
        /// </summary>
        public List<string> DetectedNumbers { get; set; }
    }

}
