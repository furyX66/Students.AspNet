using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Students.Common.Attributes;

public class NameSurnameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var result = new ValidationResult("The field must start with capital letter and contain only one space.");
        if (value is string str)
        {
            if (Regex.IsMatch(str, @"^[A-Z][a-z]*(\s[A-Z][a-z]*)?$"))
            {
                result = ValidationResult.Success;
            }
        }

        return result;
    }
}