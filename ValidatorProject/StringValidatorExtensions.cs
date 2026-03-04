namespace ValidatorProject;

public static class StringValidatorExtensions
{
    public static IValidator<string> IsNotEmpty(this IValidator<string> validator, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ((Validator<string>)validator).result.Errors.Add($"Value cannot be empty");
        }
        return validator;
    }
    
    public static IValidator<string> MaxLength(this IValidator<string> validator, int maxLength, string value)
    {
        if (value.Length > maxLength)
        {
            ((Validator<string>)validator).result.Errors.Add($"Value cannot be longer than {maxLength}");
        }
        return validator;
    }
}