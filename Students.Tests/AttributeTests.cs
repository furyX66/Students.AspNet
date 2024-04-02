using Students.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Students.Tests
{
    public class AttributeTests
    {
        #region CapitalLettersFirstAttributeTests
        [Theory]
        [InlineData("ValidString")]
        [InlineData("Another Valid String")]
        [InlineData("Yet Another Valid String")]
        public void CapitalLettersFirstAttributeIsValid_ValidStrings_ReturnsSuccess(string input)
        {
            // Arrange
            var attribute = new CapitalLettersFirstAttribute();
            var validationContext = new ValidationContext(new { Value = input });

            // Act
            var result = attribute.GetValidationResult(input, validationContext);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }
        [Theory]
        [InlineData("invalidString")]
        [InlineData("123Invalid")]
        [InlineData("!@#Invalid")]
        public void CapitalLettersFirstAttributeIsValid_InvalidStrings_ReturnsError(string input)
        {
            // Arrange
            var attribute = new CapitalLettersFirstAttribute();
            var validationContext = new ValidationContext(new { Value = input });

            // Act
            var result = attribute.GetValidationResult(input, validationContext);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal("The field must start with capital letter and can't contain numbers or special symbols.", result.ErrorMessage);
        }
        #endregion
        #region NameSurnameAttributeTests
        [Theory]
        [InlineData("Szymon Kowalski")]
        [InlineData("John Smith")]
        [InlineData("Daniel Kachowski")]
        public void NameSurnameAttributeIsValid_ValidStrings_ReturnsSuccess(string input)
        {
            // Arrange
            var attribute = new NameSurnameAttribute();
            var validationContext = new ValidationContext(new { Value = input });

            // Act
            var result = attribute.GetValidationResult(input, validationContext);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }
        [Theory]
        [InlineData("Artem")]
        [InlineData("Ararat kochanowski")]
        [InlineData("kolya wakowski")]
        [InlineData("Artem123@#$")]
        public void NameSurnameAttributeIsValid_InValidStrings_ReturnsError(string input)
        {
            // Arrange
            var attribute = new NameSurnameAttribute();
            var validationContext = new ValidationContext(new { Value = input });

            // Act
            var result = attribute.GetValidationResult(input, validationContext);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal("The field must start with capital letter, must contain Name and Surname and only one space.", result.ErrorMessage);
        }
        #endregion
        #region PostalCodeAttribute
        [Theory]
        [InlineData("12-345")]
        [InlineData("99-999")]
        [InlineData("00-000")]
        public void PostalCodeAttributeIsValid_ValidStrings_ReturnsSuccess(string postalCode)
        {
            // Arrange
            var attribute = new PostalCodeAttribute();
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(postalCode, validationContext);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("123-456")]
        [InlineData("123456")]
        [InlineData("1234-567")]
        [InlineData("123-4567")]
        public void PostalCodeAttributeIsValid_ValidStrings_ReturnsError(string postalCode)
        {
            // Arrange
            var attribute = new PostalCodeAttribute();
            var validationContext = new ValidationContext(new object());

            // Act
            var result = attribute.GetValidationResult(postalCode, validationContext);

            // Assert
            Assert.Equal("The field must be a valid Polish postal code (format: XX-XXX).", result.ErrorMessage);
        } 
        #endregion
    }
}
