using DomainProject;
using ValidatorProject.Numbers;

namespace ValidatorProject;

public static class BookValidator
{
    public static IValidator<Book> ValidateTitle(this IValidator<Book> validator, string title)
    {
        var titleValidator = new Validator<string>()
            .IsNotNull(title)
            .IsNotEmpty(title)
            .LengthRange(3, 100, title);

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

    public static IValidator<Book> ValidateContent(this IValidator<Book> validator, string content)
    {
        var contentValidator = new Validator<string>();
        contentValidator.IsNotNull(content).IsNotEmpty(content).MinLength(50, content);
        
        var result = contentValidator.Validate();
        if (result.Errors != null)
            foreach (var error in result.Errors)
            {
                validator.AddError($"Content: {error}");
            }

        return validator;
    }

    public static IValidator<Book> ValidateAuthor(this IValidator<Book> validator, Book book)
    {
        if (book.AuthorIDs == null)
        {
            validator.AddError("There is no authors for this book");
        }

        return validator;
    }
    
    public static IValidator<Book> ValidatePublisher(this IValidator<Book> validator, Book book)
    {
        if (book.PublisherID == null)
        {
            validator.AddError("There is no publisher for this book");
        }

        return validator;
    }

    public static IValidator<Book> ValidateBook(this IValidator<Book> validator, Book book)
    {
        validator.ValidateTitle(book.Title);
        validator.ValidateISBN(book.Isbn);
        validator.ValidateContent(book.Content);
        validator.ValidateAuthor(book);
        validator.ValidatePublisher(book);
        return validator;
    }
}