using DomainProject;
using ValidatorProject;

namespace ValidatorTests;

public class AuthorValidatorTests
{
    private IValidator<Author> _validator;
    private Author _author;
    private Patent _patent;
    private Country _country;
    private Author _authorWithNoCountry;
    private Author _authorError;
    private Author _authorName;


    [SetUp]
    public void SetUp()
    {
        _validator = new Validator<Author>();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );
        _patent = new Patent(
            title: "stub",
            content: "stub",
            patentCode: "stub",
            publishDate: DateTime.MinValue,
            authors: new List<Author>()
        );
        _author = new Author(
            name: "John",
            lastName: "Johnson",
            country: _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities: null,
            patentId: _patent
        );
        _authorWithNoCountry = new Author(
            name: "John",
            lastName: "Johnson",
            country: null!,
            birthdayYear: new DateTime(1990, 5, 20),
            entities: null,
            patentId: _patent
        );
        _authorError = new Author(
            name: "Jo",
            lastName: "Doe",
            country: null!,
            birthdayYear: DateTime.Now.AddYears(1),
            entities: null,
            patentId: _patent
        );
        _authorName = new Author(
            name: "Jo",
            lastName: "Johnson",
            country: _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities: null,
            patentId: _patent
        );

        _patent.Authors.Add(_author);
    }

    //ValidateName

    [Test]
    public void ValidateName_ValidName_NoErrors()
    {
        //Arrange 

        //Act
        var result = _validator.ValidateName("John").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateName_TooShort_AddsNameRangeError()
    {
        //Arange
        //Act
        var result = _validator.ValidateName("Jo").Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: value length must be between 3 and 15"));
    }

    [Test]
    public void ValidateName_TooLong_AddsNameRangeError()
    {
        var result = _validator.ValidateName("JohnJohnJohnJohn").Validate();

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: value length must be between 3 and 15"));
    }

    [Test]
    public void ValidateName_ContainsNumbers_AddsNameAlphabeticError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("J0hn").Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: value must contain only alphabetic characters"));
    }

    [Test]
    public void ValidateName_EmptyString_AddsNameIsNotEmptyErrorError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("").Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: Value cannot be empty"));
    }

    //ValidateLastName 

    [Test]
    public void ValidateLastName_ValidLastName_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateLastName("Johnson").Validate();
        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateLastName_TooShort_AddsLastNameLengthRangeError()
    {
        //Arange
        //Act
        var result = _validator.ValidateLastName("Doe").Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Lastname: value length must be between 7 and 20"));
    }

    [Test]
    public void ValidateLastName_TooLong_AddsLastNameLengthRangeError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateLastName("JohnsonJohnsonJohnsonX").Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Lastname: value length must be between 7 and 20"));
    }

    [Test]
    public void ValidateLastName_ContainsNumbers_AddsLastNameAlphabeticError()
    {
        var result = _validator.ValidateLastName("John50n").Validate();

        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Lastname: value must contain only alphabetic characters"));
    }

    //ValidateBirthdayYear

    [Test]
    public void ValidateBirthdayYear_ValidDate_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateBirthdayYear(new DateTime(1990, 5, 20)).Validate();
        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateBirthdayYear_FutureDate_AddsBirthdayNotFutureDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateBirthdayYear(DateTime.Now.AddYears(1)).Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors,
            Has.Exactly(1).Contains($"Birthday: value must be after {nameof(DateTime.Now)} == {DateTime.Now}"));
    }

    //ValidateCountry

    [Test]
    public void ValidateCountry_NullCountry_AddsCountryError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCountry(_authorWithNoCountry).Validate();
        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Country should be written for the author"));
    }

    [Test]
    public void ValidateCountry_ValidCountry_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCountry(_author).Validate();
        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    //ValidateAuthor

    [Test]
    public void ValidateAuthor_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateAuthor(_author).Validate();
        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateAuthor_InvalidName_OnlyNameErrorPresent()
    {
        //Arrange
        //Act
        var result = _validator.ValidateAuthor(_authorName).Validate();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Name:"));
        });
    }

    [Test]
    public void ValidateAuthor_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateAuthor(_authorError).Validate();
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors, Has.Some.Contains("Name:"));
                Assert.That(result.Errors, Has.Some.Contains("Lastname:"));
                Assert.That(result.Errors, Has.Some.Contains("Birthday:"));
                Assert.That(result.Errors, Has.Some.Contains("Country"));
            }
        );
    }
}