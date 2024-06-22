using Microsoft.AspNetCore.Mvc;
using PhoneNumberDetection.Common.Interface;
using PhoneNumberDetection.Common.Models;

namespace PhoneNumberDetection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumberDetectionController : ControllerBase
    {
        private readonly IPhoneNumberDetector _phoneNumberDetector;

        public PhoneNumberDetectionController(IPhoneNumberDetector phoneNumberDetector)
        {
            _phoneNumberDetector = phoneNumberDetector;
        }

        /// <summary>
        /// Endpoint to detect phone numbers from the input text.
        /// Phone numbers can be in various formats including 10-digit numbers, numbers with country codes,
        /// numbers with parentheses for area codes, and numbers with spaces or dashes as separators.
        /// Numbers can also be represented in English and Hindi, or a combination of both.
        /// </summary>
        /// <param name="request">The request containing the text to analyze for phone numbers.</param>
        /// <returns>An ActionResult containing the detected phone numbers wrapped in a PhoneNumberDetectionResponse.</returns>
        [HttpPost]
        [Route("detect")]
        public ActionResult<PhoneNumberDetectionResponse> DetectPhoneNumbers([FromBody] PhoneNumberDetectionRequest request)
        {
            // Check if the request or text is null or empty
            if (request == null || string.IsNullOrEmpty(request.Text))
            {
                // Return a 400 Bad Request response with an error message
                return BadRequest("Invalid input text.");
            }

            // Call the phone number detector service to detect phone numbers in the provided text
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(request.Text);

            // Return a 200 OK response with the detected phone numbers wrapped in a PhoneNumberDetectionResponse
            return Ok(new PhoneNumberDetectionResponse
            {
                DetectedNumbers = detectedNumbers
            });
        }

    }
}
