using System.Runtime.CompilerServices;

namespace ValidatorProject.Numbers;

public static class NumberValidatorExtensions 
{
    public static IValidator<int> LessThan(this IValidator<int> validator, int min, int value)
    {
        if (min > value)
        {
            validator.AddError($"{nameof(value)} must be more than {nameof(min)} == {min}");
        }
        return validator;
    }
    public static IValidator<int> GreaterThan(this IValidator<int> validator, int max, int value)
    {
        if (value > max)
        {
            validator.AddError($"{nameof(value)} must be less than {max}");
        }
        return validator;
    }
    
    public static IValidator<int> InRange(this IValidator<int> validator, int min, int max, int value)
    {
        if (value < min || value > max)
        {
            validator.AddError($"{nameof(value)} must be between {min} and {max}");
        }
        return validator;
    }
    
    public static IValidator<int> Positive(this IValidator<int> validator, int value)
    {
        if (value <= 0)
        {
            validator.AddError($"{nameof(value)} must be more than 0");
        }
        return validator;
    }
}