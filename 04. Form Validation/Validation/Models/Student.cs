using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Validation.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Please provide a name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetic characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide an Id")]
        [RegularExpression(@"^\d{2}-\d{5}-\d{1,3}$", ErrorMessage = "Id format must be xx-xxxxx-x")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please provide a date of birth")]
        [CustomValidation(typeof(StudentValidation), "ValidateDob")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Please provide an email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [CustomValidation(typeof(StudentValidation), "ValidateEmail")]
        public string Email { get; set; }
    }

    public static class StudentValidation
    {
        public static ValidationResult ValidateDob(DateTime dob, ValidationContext context)
        {
            var age = DateTime.Now.Year - dob.Year;

            if (age <= 20)
                return new ValidationResult("Age must be greater than 20.");
            return ValidationResult.Success;
        }

        public static ValidationResult ValidateEmail(string email, ValidationContext context)
        {
            var student = (Student)context.ObjectInstance; // Get the current student instance
            var expectedEmail = $"{student.Id}@student.aiub.edu"; // Construct the expected email

            // Check if the email matches the expected format
            if (email != expectedEmail)
                return new ValidationResult($"Email format must be {expectedEmail}.");

            return ValidationResult.Success;
        }
    }
}
