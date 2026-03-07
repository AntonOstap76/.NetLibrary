using DomainProject;

namespace ValidatorProject;

public static class PublisherValidator
{
    public static IValidator<Publisher> ValidateName(this IValidator<Publisher> validator, string name)
    {
        var nameValidator = new Validator<string>();
        nameValidator.IsNotNull(name).IsNotEmpty(name).LengthRange(3, 15, name).OnlyLetters(name);
        
        var result = nameValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Name: {error}");
            }
        }

        return validator;
    }
    
    public static IValidator<Publisher> ValidateFoundedYear(this IValidator<Publisher> validator, DateTime year)
    {
        var birthdayValidator = new Validator<DateTime>();
        birthdayValidator.IsNotNull(year).NotFutureDate(year).DateValidation(DateTime.MinValue, year);
        
        var result = birthdayValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Birthday: {error}");
            }
        }

        return validator;
    }
}