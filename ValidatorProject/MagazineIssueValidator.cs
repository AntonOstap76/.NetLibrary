using DomainProject;
using ValidatorProject.Numbers;

namespace ValidatorProject;

public static class MagazineIssueValidator
{
    public static IValidator<MagazineIssue> ValidateNumber(this IValidator<MagazineIssue> validator, int number)
    {
        var validatorNumber = new Validator<int>();
        validatorNumber.IsNotNull(number).Positive(number);
        
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

    public static IValidator<MagazineIssue> ValidateContent(this IValidator<MagazineIssue> validator, string content)
    {
        var contentValidator = new Validator<string>();
        contentValidator.IsNotNull(content).IsNotEmpty(content).MinLength(50, content);
        
        var result = contentValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Content: {error}");
            }
        }

        return validator;
    }

    public static IValidator<MagazineIssue> ValidateMagazine(this IValidator<MagazineIssue> validator,
        MagazineIssue magazineIssue)
    {
        if (magazineIssue.Magazine.Id == null)
        {
            validator.AddError($"Should contain a magazine id");
        }
        return validator;
    }

    public static IValidator<MagazineIssue> ValidateMagazineIssue(this IValidator<MagazineIssue> validator,
        MagazineIssue magazineIssue)
    {
        validator.ValidateNumber(magazineIssue.Number);
        validator.ValidateTitle(magazineIssue.Title);
        validator.ValidateDate(magazineIssue.PublisherDate);
        validator.ValidateIssn(magazineIssue.MagazineIssn);
        validator.ValidateContent(magazineIssue.Content);
        validator.ValidateMagazine(magazineIssue);
        
        return validator;
    }
}