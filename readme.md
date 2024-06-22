# Phone Number Detection API
This project is a .NET 8 Web API application that detects phone numbers in text using various formats and validates them. It utilizes .NET Core libraries and extensions for efficient phone number detection and validation.

## Project Structure
The project is structured into several components:

#### API (PhoneNumberDetection)
    The API project handles HTTP requests and houses the main entry point for the application.

## Controllers

#### PhoneNumberDetectionController: 
    Handles incoming HTTP requests related to phone number detection.

#### Extensions

#### ServiceExtensions:
    Configures services for dependency injection in the application.

#### Program.cs

    Configures the application pipeline, sets up Swagger/OpenAPI for documentation, and defines HTTP endpoints.

## 2. Common (PhoneNumberDetection.Common)
This class library contains shared models and interfaces used across the project.

### Models

PhoneNumberDetectionRequest: Represents the request object for detecting phone numbers in text.
PhoneNumberDetectionResponse: Represents the response object for detected phone numbers.

### Interfaces

IPhoneNumberDetector: Defines the interface for detecting phone numbers within text.

## 3. Services (PhoneNumberDetection.Services)
Contains the implementation for detecting phone numbers.

#### PhoneNumberDetector: 
    Implements the IPhoneNumberDetector interface to detect phone numbers in a given text using regular expressions and helper methods.
    
## 4. Utility (PhoneNumberDetection.Utility)
Contains utility classes for phone number detection and conversion.

#### Helpers: 
    Provides static methods for validating phone numbers and checking if a matched number is standalone in text.

#### PhoneNumberPatterns: 
    Provides utility methods to convert words representing digits into their corresponding numerical digits.

## 5. Tests
This project contains unit tests using NUnit to verify the functionality of the phone number detection and validation.

### How to Run
To run the application locally, follow these steps:

#### Clone the repository.
Open the solution in Visual Studio or your preferred IDE.
Set the API project (PhoneNumberDetection) as the startup project.
Build and run the solution.
####  Ensure you have .NET 8 SDK installed on your machine.

### Usage
Send HTTP POST requests to the /api/PhoneNumberDetection/DetectNumbers endpoint with JSON payload containing the Text field to detect phone numbers.
Example request payload:

`
{
  "Text": "Hello! My number is +1 (234) 567-8901."
}
`
Example response:

`
{
  "DetectedNumbers": [
    "+1 (234) 567-8901"
  ]
}
`

## Testing
The project includes unit tests for the phone number detection functionality. 

This README provides a comprehensive overview of your project structure, its components, usage instructions, and how to contribute. Modify it further based on additional details or specific configurations of your project.