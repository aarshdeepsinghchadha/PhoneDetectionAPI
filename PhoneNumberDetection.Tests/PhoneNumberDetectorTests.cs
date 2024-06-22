using PhoneNumberDetection.Services;

namespace PhoneNumberDetection.Tests
{
    [TestFixture]
    public class PhoneNumberDetectorTests
    {
        private PhoneNumberDetector _phoneNumberDetector;

        [SetUp]
        public void Setup()
        {
            _phoneNumberDetector = new PhoneNumberDetector();
        }

        [Test]
        public void DetectPhoneNumbers_Format_1234567890()
        {
            // Arrange
            string inputText = "Hello, my number is 1234567890.";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item("1234567890"));
            Assert.AreEqual(1, detectedNumbers.Count);
        }

        [Test]
        public void DetectPhoneNumbers_Format_WithCountryCode()
        {
            // Arrange
            string inputText = "You can call me at +1-123-456-7890.";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item("1-123-456-7890"));
            Assert.AreEqual(1, detectedNumbers.Count);
        }

        [Test]
        public void DetectPhoneNumbers_Format_WithParentheses()
        {
            // Arrange
            string inputText = "You can call me at (555) 123-4567.";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item("(555)123-4567"));
            Assert.AreEqual(1, detectedNumbers.Count);
        }

        [Test]
        public void DetectPhoneNumbers_MultipleNumbersInText()
        {
            // Arrange
            string inputText = "You can call me at +1-123-456-7890 or (555) 123-4567.";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item("1-123-456-7890"));
            Assert.That(detectedNumbers, Contains.Item("(555)123-4567"));
            Assert.AreEqual(2, detectedNumbers.Count);
        }

        [Test]
        public void DetectPhoneNumbers_NumbersInDifferentLanguages()
        {
            // Arrange
            string inputText = "Numbers in Hindi: एक दो तीन चार पांच सात आठ नौ शू एक।";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item("1234578901"));
        }

        [Test]
        public void DetectPhoneNumbers_EmptyInput_ReturnsEmptyList()
        {
            // Arrange
            string inputText = "";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.IsEmpty(detectedNumbers);
        }

        [Test]
        public void DetectPhoneNumbers_EmptyText_ReturnsEmptyList()
        {
            // Arrange
            var inputText = "";

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.IsEmpty(detectedNumbers);
        }

        [TestCase("Invalid text")]
        [TestCase("This text does not contain any phone numbers.")]
        public void DetectPhoneNumbers_NoNumbers_ReturnsEmptyList(string inputText)
        {
            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.IsEmpty(detectedNumbers);
        }

        [TestCase("This number 100 is important.", "100")]
        [TestCase("Ambulance service: 108", "108")]
        public void DetectPhoneNumbers_StandaloneEmergencyNumbers_ReturnsCorrectNumbers(string inputText, string expectedNumber)
        {
            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Contains.Item(expectedNumber));
            Assert.That(detectedNumbers.Count, Is.EqualTo(1));
        }

        [TestCase("Invalid 911 number")]
        public void DetectPhoneNumbers_NotStandaloneEmergencyNumber_DoesNotDetect(string inputText)
        {
            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.IsEmpty(detectedNumbers);
        }

        [Test]
        public void DetectPhoneNumbers_AlphanumericInput_ReturnsCorrectNumbers()
        {
            // Arrange
            string inputText = "Hi Alix, you can contact me at 9 8 ONE ZERO zero two 3 4 five five or at 123-456-7890.";
            var expectedNumbers = new List<string> { "9810023455", "123-456-7890" };

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Is.EquivalentTo(expectedNumbers));
        }

        [Test]
        public void DetectPhoneNumbers_MixedFormatsInput_ReturnsCorrectNumbers()
        {
            // Arrange
            string inputText = "Hello! My number is 123-456-7890. You can also reach me at +1 (234) 567-8901. " +
                               "For international calls, dial +91-9876543210. Oh, and don't forget my office line: " +
                               "(555) 123-4567. Feel free to text me at 9876543210.";
            var expectedNumbers = new List<string> { "123-456-7890", "+1(234)567-8901", "+91-9876543210", "(555)123-4567", "9876543210" };

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Is.EquivalentTo(expectedNumbers));
        }

        [Test]
        public void DetectPhoneNumbers_ComplexInput_ReturnsCorrectNumbers()
        {
            // Arrange
            string inputText = "Hi there! You can call me at 123-456-7890 or reach me at +1 (234) 567-8901 for urgent matters. " +
                               "My office number is (555) 123-4567. Feel free to text me at 9876543210. " +
                               "For international calls, dial +91-9876543210. I prefer texts over calls, so please use WhatsApp if possible. " +
                               "Don't forget my alternate number: 987-654-3210. It's essential for backup communication. " +
                               "Reach out anytime at 9810023455. I'm available during business hours and occasionally on weekends. " +
                               "In case of emergencies, dial 911 or contact my assistant at +1 (555) 123-4567. " +
                               "My personal line is (987) 654-3210. I look forward to hearing from you soon! " +
                               "For work inquiries, call +91-9876543210 or send an email to work@example.com. " +
                               "You can also connect with me on LinkedIn: linkedin.com/in/username. " +
                               "Feel free to leave a message if I'm unavailable. Remember, my number is 1234567890. " +
                               "If you have any questions, please don't hesitate to call or text me at 9876543210. " +
                               "I'm always reachable at (555) 123-4567 or via email at contact@example.com. " +
                               "Let's schedule a call for next week. Please confirm at +1 (234) 567-8901. " +
                               "Here's my mobile number: 9810023455. Let's discuss the details further. " +
                               "Thank you for reaching out! Looking forward to our conversation at 123-456-7890.";

            var expectedNumbers = new List<string>
            {
                "123-456-7890", "+1(234)567-8901", "(555)123-4567", "9876543210", "+91-9876543210", "987-654-3210", "9810023455", "+1(555)123-4567", "(987)654-3210", "1234567890"
            };

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Is.EquivalentTo(expectedNumbers));
        }

        [Test]
        public void DetectPhoneNumbers_ComplexInput_ReturnsCorrectNumbers_V2()
        {
            // Arrange
            string inputText = "Hi Alix, you can reach me at +1 (123) 456-7890 or +91-9876543210 for any queries. " +
                               "My office number is (555) 123-4567. Feel free to text me at 9810023455. " +
                               "In emergencies, contact +1-911. My alternate number is (987) 654-3210. " +
                               "For international calls, dial +1 (234) 567-8901. " +
                               "Remember, my mobile number is 123-456-7890. " +
                               "I prefer texts over calls, so use WhatsApp if possible. " +
                               "Here are the digits in English: ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO. " +
                               "And in Hindi: एक दो तीन चार पांच छह सात आठ नौ शू. Let's connect soon.";

            var expectedNumbers = new List<string>
            {
                "+1(123)456-7890", "+91-9876543210", "(555)123-4567", "9810023455", "(987)654-3210", "+1(234)567-8901", "123-456-7890", "1234567890", "+1-911"
            };

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Is.EquivalentTo(expectedNumbers));
        }


        [Test]
        public void DetectPhoneNumbers_ComplexInput_ReturnsCorrectNumbers_V3()
        {
            // Arrange
            string inputText = "Hello, you can reach me at +1-123-456-7890 or +91 (123) 456-7890 for any inquiries. " +
                               "My office number is (555) 123-4567. Feel free to text me at 9810023455. " +
                               "In emergencies, contact +1-911. My alternate number is (987) 654-3210. " +
                               "For international calls, dial +1 (234) 567-8901. Remember, my mobile number is 123-456-7890. " +
                               "I prefer texts over calls, so use WhatsApp if possible. " +
                               "Here are the digits in English: ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO. " +
                               "And in Hindi: एक दो तीन चार पांच छह सात आठ नौ शू. Let's connect soon!\n\n" +
                               "I live at 123 Main Street. You can reach my family at +1-800-555-1234. " +
                               "My father's mobile is (987) 654-3210 and my mother's is +91-9876543210. " +
                               "In our neighborhood, you can contact the local police at 100 in emergencies. " +
                               "For international assistance, call +1 (234) 567-8901. " +
                               "We also have a community hotline at (555) 123-4567 for local events and updates. " +
                               "Feel free to visit us anytime.\n\n" +
                               "My work number is +1-800-123-4567 and my colleague's number is +91 (234) 567-8901. " +
                               "For business inquiries, please call our office at (555) 123-4567. " +
                               "In case of technical issues, contact our IT support at 9810023455. " +
                               "We provide 24/7 customer service at +1-800-555-6789. " +
                               "Our team members are available round the clock.\n\n" +
                               "For language assistance, you can call the helpline at +91-9876543210. " +
                               "We speak English, Hindi, and Spanish fluently. " +
                               "Our customer care team is reachable at +1 (123) 456-7890 for any queries regarding our services. " +
                               "Please note that our call center operates from 9 AM to 6 PM, Monday to Friday.\n\n" +
                               "Feel free to contact me via WhatsApp at 9810023455 or call my mobile at +1-911 for immediate assistance. " +
                               "We value your feedback, so please reach out to us at (987) 654-3210. " +
                               "Remember to dial +1 (234) 567-8901 for international support. " +
                               "Our emergency hotline is +91-9876543210. " +
                               "Thank you for choosing our services. For ambulance get 108";

            var expectedNumbers = new List<string>
            {
               "1-123-456-7890",
                "+91(123)456-7890",
                "(555)123-4567",
                "9810023455",
                "(987)654-3210",
                "+1(234)567-8901",
                "123-456-7890",
                "1234567890",
                "1-800-555-1234",
                "+91-9876543210",
                "1-800-123-4567",
                "+91(234)567-8901",
                "1-800-555-6789",
                "+1(123)456-7890",
                "9876543210",
                "+1-911",
                "100",
                "108"
            };

            // Act
            var detectedNumbers = _phoneNumberDetector.DetectPhoneNumbers(inputText);

            // Assert
            Assert.That(detectedNumbers, Is.EquivalentTo(expectedNumbers));
        }

    }
}
