using DomainProject;

namespace ValidatorProject;

public static class AuthorValidator
{
    public static IValidator<Author> ValidateName(this IValidator<Author> validator, string name)
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
    
    public static IValidator<Author> ValidateLastName(this IValidator<Author> validator, string lastName)
    {
        var nameValidator = new Validator<string>();
        nameValidator.IsNotNull(lastName).IsNotEmpty(lastName).LengthRange(7, 20, lastName).OnlyLetters(lastName);
        
        var result = nameValidator.Validate();
        if (result.Errors != null)
        {
            foreach (var error in result.Errors)
            {
                validator.AddError($"Lastname: {error}");
            }
        }

        return validator;
    }

    public static IValidator<Author> ValidateBirthdayYear(this IValidator<Author> validator, DateTime year)
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

    public static IValidator<Author> ValidateCountry(this IValidator<Author> validator, Author author)
    {
        if (author.Country == null)
        {
            validator.AddError($"Country should be written for the author");
        }

        return validator;
    }

    public static IValidator<Author> ValidateAuthor(this IValidator<Author> validator, Author author)
    {
        validator.ValidateName(author.Name);
        validator.ValidateLastName(author.LastName);
        validator.ValidateBirthdayYear(author.BirthdayYear);
        validator.ValidateCountry(author);

        return validator;
    }
    
}