using DomainProject;
using ValidatorProject.Numbers;

namespace ValidatorProject;

public static class MagazineIssueValidator
{
    public static IValidator<MagazineIssue> ValidateNumber(this IValidator<MagazineIssue> validator, int number)
    {
        var validatorNumber = new Validator<int>();
        validatorNumber.IsNotNull(number).Positive(number).GreaterThan(0, number);
        
        var result = validatorNumber.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Number: {error}");
            }
        }

        return validator;
    }

    public static IValidator<MagazineIssue> ValidateTitle(this IValidator<MagazineIssue> validator, string title)
    {
        var validatorTitle = new Validator<string>();
        validatorTitle.IsNotNull(title).IsNotEmpty(title).LengthRange(5, 255, title);
        
        var result = validatorTitle.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Title: {error}");
            }
        }

        return validator;
    }

    public static IValidator<MagazineIssue> ValidateDate(this IValidator<MagazineIssue> validator, DateTime date)
    {
        var dateValidator = new Validator<DateTime>();
        dateValidator.NotFutureDate(date).DateValidation(DateTime.MinValue, date);
        
        var result = dateValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Date: {error}");
            }
        }

        return validator;
    }
    
    public static IValidator<MagazineIssue> ValidateIssn(this IValidator<MagazineIssue> validator, string issn)
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
    
    
    
    //TODO 
    //Validator for the content? Dont have ideas for now
    //Should I validate for the magazine?
    
}