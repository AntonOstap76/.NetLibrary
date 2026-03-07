
namespace ValidatorProject;

public static class DataValidatorExtensions
{
    public static IValidator<DateTime> DateValidation(this IValidator<DateTime> validator, DateTime minDate, DateTime value)
    {
        if (value < minDate)
        {
            validator.AddError($"{nameof(value)} must be after {nameof(minDate)} == {minDate}");
        }
        return validator;
    }

    public static IValidator<DateTime> NotFutureDate(this IValidator<DateTime> validator, DateTime value)
    {
        if (value > DateTime.Now)
        {
            validator.AddError($"{nameof(value)} must be after {nameof(DateTime.Now)} == {DateTime.Now}");
        }
        return validator;
    }

    public static IValidator<DateTime> NotEarlierThanDate(this IValidator<DateTime> validator, DateTime minDate,
        DateTime value)
    {
        if (value <= minDate)
        {
            validator.AddError($"{nameof(value)} cannot be earlier than {minDate:yyyy-MM-dd}");
        }
        return validator;
    }
    
    public static IValidator<DateTime> NotAfterTheDate(this IValidator<DateTime> validator, DateTime maxDate,
        DateTime value)
    {
        if (value >= maxDate)
        {
            validator.AddError($"{nameof(value)} cannot be after than {maxDate:yyyy-MM-dd}");
        }
        return validator;
    }
    
    
    
}