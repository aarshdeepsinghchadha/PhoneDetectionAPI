using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumberDetection.Common.Models
{
    /// <summary>
    /// Represents the request object for detecting phone numbers in text.
    /// </summary>
    public class PhoneNumberDetectionRequest
    {
        /// <summary>
        /// Gets or sets the text in which phone numbers are to be detected.
        /// </summary>
        public string Text { get; set; }
    }

}
