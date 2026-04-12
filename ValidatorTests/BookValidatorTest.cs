using DomainProject;
using ValidatorProject;
using NUnit.Framework;

namespace ValidatorTests;

[TestFixture]
public class BookValidatorTests
{
    private IValidator<Book> _validator;
    private Book _book;
    private Country _country;
    private Publisher _publisher;

    [SetUp]
    public void Setup()
    {
        _validator = new Validator<Book>();

        _country = new Country(
            name:     "Poland",
            code:     "32444",
            language: "Polish"
        );

        _publisher = new Publisher(
            name:        "Ban",
            country:     _country,
            foundedYear: new DateTime(1999, 1, 1),
            magazines:   new List<Magazine>(),
            entities:    new List<CommonEntity>()
        );

        _book = new Book(
            title:      "Valid Title",
            content:    "This is a valid content that is definitely longer than fifty characters long.",
            isbn:       "1234567890123",
            authorId:   new List<Author>(),
            publisherId: _publisher
        );
    }

    [Test]
    public void ValidateTitle_ValidTitle_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Valid Title").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateTitle_TooShort_ShortTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Ab").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Title: value length must be between 3 and 100"));
    }

    [Test]
    public void ValidateTitle_TooLong_LongTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle(new string('A', 101)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Some.Contains("Title: value length must be between 3 and 100"));
    }

    [Test]
    public void ValidateTitle_EmptyTitle_EmptyTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateISBN_ValidISBN_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateISBN("1234567890123").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateISBN_TooShort_ISBNError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateISBN("12345").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("ISBN: value must exactly 13 characters long"));
    }

    [Test]
    public void ValidateISBN_TooLong_AddsISBNError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateISBN("12345678901234").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("ISBN: value must exactly 13 characters long"));
    }

    [Test]
    public void ValidateISBN_ContainsLetters_AddsISBNError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateISBN("12345678901AB").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("ISBN: value must contain only digits"));
    }

    [Test]
    public void ValidateContent_ValidContent_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateContent("This is a valid content that is definitely longer than fifty characters long.").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateContent_TooShort_AddsContentError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateContent("Too short").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Content: value must be at least 50 characters long"));
    }

    [Test]
    public void ValidateContent_EmptyContent_AddsContentError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateContent("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateAuthor_NullAuthors_AddsAuthorError()
    {
        //Arrange
        var bookWithNoAuthors = new Book(
            title:       "Valid Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890123",
            authorId:    null!,
            publisherId: _publisher
        );

        //Act
        var result = _validator.ValidateAuthor(bookWithNoAuthors).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("There is no authors for this book"));
    }

    [Test]
    public void ValidateAuthor_ValidAuthors_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateAuthor(_book).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidatePublisher_NullPublisher_AddsPublisherError()
    {
        //Arrange
        var bookWithNoPublisher = new Book(
            title:       "Valid Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890123",
            authorId:    new List<Author>(),
            publisherId: null!
        );

        //Act
        var result = _validator.ValidatePublisher(bookWithNoPublisher).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("There is no publisher for this book"));
    }

    [Test]
    public void ValidatePublisher_ValidPublisher_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidatePublisher(_book).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateBook_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateBook(_book).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateBook_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        var invalidBook = new Book(
            title:       "Ab", 
            content:     "Short", 
            isbn:        "123",   
            authorId:    null!,   
            publisherId: null!   
        );

        //Act
        var result = _validator.ValidateBook(invalidBook).Validate();

        //Assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Title:"));
            Assert.That(result.Errors, Has.Some.Contains("ISBN:"));
            Assert.That(result.Errors, Has.Some.Contains("Content:"));
            Assert.That(result.Errors, Has.Some.Contains("authors"));
            Assert.That(result.Errors, Has.Some.Contains("publisher"));
        });
    }
}