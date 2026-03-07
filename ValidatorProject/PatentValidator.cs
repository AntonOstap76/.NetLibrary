using DomainProject;

namespace ValidatorProject;

public static class PatentValidator
{
    public static IValidator<Patent> ValidateTitle(this IValidator<Patent> validator, string title)
    {
        var titleValidator = new Validator<string>();
        titleValidator.IsNotNull(title).IsNotEmpty(title).MinLength(5, title).MaxLength(500, title);
        var result = titleValidator.Validate();

        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Title: {error}");
            }
        }

        return validator;
    }

    public static IValidator<Patent> ValidateCode(this IValidator<Patent> validator, string code)
    {
        var codeValidator = new Validator<string>();
        codeValidator.IsNotNull(code).IsNotEmpty(code).ExactLength(10,code).OnlyNumbers(code);

        var result = codeValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Code: {error}");
            }
        }

        return validator;
    }

    public static IValidator<Patent> ValidateDate(this IValidator<Patent> validator, DateTime date)
    {
        var dateValidator = new Validator<DateTime>();
        dateValidator.DateValidation(DateTime.MinValue, date).NotFutureDate(date);
        
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
    
    //TODO 
    //Validator for the content? Dont have ideas for now
    //Should I validate for the authors?
    
}