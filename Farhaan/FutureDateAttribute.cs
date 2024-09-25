using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace // Change to your actual namespace
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Checking if the value is a DateTime
            if (value is DateTime dateTime)
            {
                // Checking if the date is in the past
                if (dateTime < DateTime.Now.Date)
                {
                    // Returns a validation error message if the date is in the past
                    return new ValidationResult("The date must be in the future.");
                }
            }
            // If the value is valid, return success
            return ValidationResult.Success;
        }
    }
}