using DomainProject;
using ValidatorProject;
using NUnit.Framework;

namespace ValidatorTests;

[TestFixture]
public class PatentValidatorTests
{
    private IValidator<Patent> _validator;
    private Patent _patent;

    [SetUp]
    public void Setup()
    {
        _validator = new Validator<Patent>();

        _patent = new Patent(
            title:       "Valid Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "1234567890",
            publishDate: new DateTime(2020, 1, 1),
            authors:     new List<Author>()
        );
    }

    [Test]
    public void ValidateTitle_ValidTitle_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Valid Patent Title").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateTitle_TooShort_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Ab").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Title: value length must be between 5 and 500"));
    }

    [Test]
    public void ValidateTitle_TooLong_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle(new string('A', 501)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Title: value length must be between 5 and 500"));
    }

    [Test]
    public void ValidateTitle_EmptyTitle_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Some.Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateCode_ValidCode_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCode("1234567890").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateCode_TooShort_AddsCodeError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCode("12345").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Code: value must exactly 10 characters long"));
    }

    [Test]
    public void ValidateCode_TooLong_AddsCodeError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCode("12345678901").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Code: value must exactly 10 characters long"));
    }

    [Test]
    public void ValidateCode_ContainsLetters_AddsCodeError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateCode("12345678AB").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Code: value must contain only digits"));
    }

    [Test]
    public void ValidateDate_ValidDate_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateDate(new DateTime(2020, 1, 1)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateDate_FutureDate_AddsDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateDate(DateTime.Now.AddYears(1)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Date: value must be after {nameof(DateTime.Now)} == {DateTime.Now}"));
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
    public void ValidateAuthors_NullAuthors_AddsAuthorsError()
    {
        //Arrange
        var patentWithNoAuthors = new Patent(
            title:       "Valid Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "1234567890",
            publishDate: new DateTime(2020, 1, 1),
            authors:     null!
        );

        //Act
        var result = _validator.ValidateAuthors(patentWithNoAuthors).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Authors are required"));
    }

    [Test]
    public void ValidateAuthors_ValidAuthors_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateAuthors(_patent).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidatePatent_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidatePatent(_patent).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidatePatent_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        var invalidPatent = new Patent(
            title:       "Ab",                      
            content:     "Short",                   
            patentCode:  "123",                    
            publishDate: DateTime.Now.AddYears(1), 
            authors:     null!                   
        );

        //Act
        var result = _validator.ValidatePatent(invalidPatent).Validate();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Title:"));
            Assert.That(result.Errors, Has.Some.Contains("Code:"));
            Assert.That(result.Errors, Has.Some.Contains("Date:"));
            Assert.That(result.Errors, Has.Some.Contains("Content:"));
            Assert.That(result.Errors, Has.Some.Contains("Authors")); 
        });
    }
}