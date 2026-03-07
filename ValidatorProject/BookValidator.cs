using DomainProject;
using ValidatorProject.Numbers;

namespace ValidatorProject;

public static class BookValidator
{
    public static IValidator<Book> ValidateTitle(this IValidator<Book> validator, string title)
    {
        var titleValidator = new Validator<string>();

        titleValidator.IsNotNull(title).IsNotEmpty(title).LengthRange(3, 100, title);

        var result = titleValidator.Validate();

        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"Title: {error}");
            }

        return validator;
    }

    public static IValidator<Book> ValidateISBN(this IValidator<Book> validator, string isbn)
    {
        var isbnValidator = new Validator<string>();

        isbnValidator.IsNotNull(isbn).IsNotEmpty(isbn).ExactLength(13,isbn).OnlyNumbers(isbn);

        var result = isbnValidator.Validate();
        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"ISBN: {error}");
            }

        return validator;
    }
    
    //TODO 
    //Validator for the content? Dont have ideas for now
}