namespace ValidatorProject;

public static class StringValidatorExtensions
{
    public static IValidator<string> IsNotEmpty(this IValidator<string> validator, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
           validator.AddError($"Value cannot be empty");
        }
        return validator;
    }
    
    public static IValidator<string> MaxLength(this IValidator<string> validator, int maxLength, string value)
    {
        if (value.Length > maxLength)
        {
            validator.AddError($"Value cannot be longer than {maxLength}");
        }
        return validator;
    }
    
    public static IValidator<string> MinLength(this IValidator<string> validator, int minLength, string value)
    {
        if (value.Length < minLength)
        {
            validator.AddError($"{nameof(value)} must be at least {minLength} characters long");
        }
        return validator;
    }
    
    public static IValidator<string> LengthRange(this IValidator<string> validator, int min, int max, string value)
    {
        if (value.Length < min || value.Length > max)
        {
            validator.AddError($"{nameof(value)} length must be between {min} and {max}");
        }
        return validator;
    }
    public static IValidator<string> OnlyLetters(this IValidator<string> validator, string value)
    {
        if (!value.All(char.IsLetter))
        {
            validator.AddError($"{nameof(value)} must contain only alphabetic characters");
        }

        return validator;
    }
    
    public static IValidator<string> OnlyNumbers(this IValidator<string> validator, string value)
    {
        if (!value.All(char.IsDigit))
        {
            validator.AddError($"{nameof(value)} must contain only digits");
        }

        return validator;
    }
    public static IValidator<string> ExactLength(this IValidator<string> validator, int length, string value)
    {
        if (value.Length != length)
        {
            validator.AddError($"{nameof(value)} must exactly {length} characters long");
        }

        return validator;
    }
    
}