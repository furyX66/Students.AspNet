using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes
{
    public class PostalCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var result = new ValidationResult("The field must be a valid Polish postal code (format: XX-XXX).");

            if (value is string postalCode)
            {
                string regexPattern = @"^\d{2}-\d{3}$";

                if (Regex.IsMatch(postalCode, regexPattern))
                {
                    result = ValidationResult.Success;
                }
            }
            return result;
        }
    }
}