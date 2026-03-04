using System.Runtime.CompilerServices;

namespace ValidatorProject.Numbers;

public static class NumberValidatorExtensions 
{
    public static IValidator<int> LessThan(this IValidator<int> validator, int min, int value)
    {
        if (min > value)
        {
            ((Validator<int>)validator).result.Errors.Add($"{nameof(value)} must be more than {nameof(min)} == {min}");
        }

        return validator;
    }
}