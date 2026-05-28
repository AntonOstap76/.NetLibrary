using DomainProject;

namespace ValidatorProject;

public static class PatentValidator
{
    public static IValidator<Patent> ValidateTitle(this IValidator<Patent> validator, string title)
    {
        var titleValidator = new Validator<string>();
        titleValidator.IsNotNull(title).IsNotEmpty(title).LengthRange(5,500, title);
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

    public static IValidator<Patent> ValidateContent(this IValidator<Patent> validator, string content)
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

    public static IValidator<Patent> ValidateAuthors(this IValidator<Patent> validator, Patent patent )
    {
        if (patent.Authors == null)
        {
            validator.AddError("Authors are required");
        }
        return validator;
    }

    public static IValidator<Patent> ValidatePatent(this IValidator<Patent> validator, Patent patent)
    {
        validator.ValidateTitle(patent.Title);
        validator.ValidateCode(patent.PatentCode);
        validator.ValidateDate(patent.PublishDate);
        validator.ValidateAuthors(patent);
        validator.ValidateContent(patent.Content);
        
        return validator;
    }
    
}