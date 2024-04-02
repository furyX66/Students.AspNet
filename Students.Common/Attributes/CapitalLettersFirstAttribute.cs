using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class CapitalLettersFirstAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var result = new ValidationResult("The field must start with capital letter and can't contain numbers or special symbols.");
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[^\d\W][\p{L}\s]*$"))
            {
                result = ValidationResult.Success;
            }
        }

        return result;
    }
}