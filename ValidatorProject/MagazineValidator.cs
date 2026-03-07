using DomainProject;

namespace ValidatorProject;

public static class MagazineValidator
{
    public static IValidator<Magazine> ValidateIssn(this IValidator<Magazine> validator, string issn)
    {
        var issnValidator = new Validator<string>();
        issnValidator.IsNotNull(issn).IsNotEmpty(issn).ExactLength(8,issn).OnlyNumbers(issn);
        
        var result = issnValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Issn: {error}");
            }
        }

        return validator;
    }
    
    public static IValidator<Magazine> ValidateTitle(this IValidator<Magazine> validator, string title)
    {
        var titleValidator = new Validator<string>();

        titleValidator.IsNotNull(title).IsNotEmpty(title).LengthRange(10, 250, title);

        var result = titleValidator.Validate();

        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"Title: {error}");
            }

        return validator;
    }

    public static IValidator<Magazine> ValidateStartDate(this IValidator<Magazine> validator, DateTime startDate, DateTime endDate)
    {
        var startDateValidator = new Validator<DateTime>();
        startDateValidator.IsNotNull(startDate).NotFutureDate(startDate).DateValidation(DateTime.MinValue, startDate).NotAfterTheDate(endDate, startDate);
        var result = startDateValidator.Validate();
        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"StartDate: {error}");
            }

        return validator;
    }
    public static IValidator<Magazine> ValidateEndDate(this IValidator<Magazine> validator,DateTime startDate, DateTime endDate)
    {
        var startDateValidator = new Validator<DateTime>();
        startDateValidator.IsNotNull(endDate).NotFutureDate(endDate).NotEarlierThanDate(startDate, endDate);
        var result = startDateValidator.Validate();
        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"StartDate: {error}");
            }

        return validator;
    }
    
    //TODO 
    //Validator for the MagazineIssue and publisher? 
}