using System.Runtime.CompilerServices;

namespace ValidatorProject.Numbers;

public static class NumberValidatorExtensions 
{
    public static IValidator<int> LessThan(this IValidator<int> validator, int min, int value)
    {
        var numberValidator = new Validator<int>();
        if (min > value)
        {
            numberValidator.result.Errors.Add($"{nameof(value)} must be more than {nameof(min)} == {min}");
        }

        return numberValidator;
    }
}