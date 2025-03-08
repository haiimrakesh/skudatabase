using System.ComponentModel.DataAnnotations;
using SKUApp.Domain.Infrastructure.ErrorHandling;

public static class ValidationHelper
{
    public static bool Validate(object contextObject, out List<ValidationResult> validationResults)
    {
        ValidationContext validationContext = new ValidationContext(contextObject);
        validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(contextObject, validationContext, validationResults, true);
    }
}