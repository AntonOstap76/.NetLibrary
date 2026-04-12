using DomainProject;
using ValidatorProject;
using NUnit.Framework;

namespace ValidatorTests;

[TestFixture]
public class PublisherValidatorTests
{
    private IValidator<Publisher> _validator;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _validator = new Validator<Publisher>();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _publisher = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: null,
            magazines: null
        );
    }

    [Test]
    public void ValidateName_ValidName_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("Ban").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateName_TooShort_AddsNameError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("Ab").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: value length must be between 3 and 15"));
    }

    [Test]
    public void ValidateName_TooLong_AddsNameError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("PublisherNameXXX").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Name: value length must be between 3 and 15"));
    }

    [Test]
    public void ValidateName_ContainsNumbers_AddsNameError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("Ban123").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Some.Contains("Name: value must contain only alphabetic characters"));
    }

    [Test]
    public void ValidateName_EmptyName_AddsNameError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateName("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Some.Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateFoundedYear_ValidYear_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateFoundedYear(new DateTime(1999, 1, 1)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateFoundedYear_FutureDate_AddsFoundedYearError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateFoundedYear(DateTime.Now.AddYears(1)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors,
            Has.Exactly(1).Contains($"Birthday: value must be after {nameof(DateTime.Now)} == {DateTime.Now}"));
    }

    [Test]
    public void ValidatePublisher_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidatePublisher(_publisher).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidatePublisher_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        var invalidPublisher = new Publisher(
            name: "Ab", 
            country: _country,
            foundedYear: DateTime.Now.AddYears(1), 
            entities: null,
            magazines: null
        );

        //Act
        var result = _validator.ValidatePublisher(invalidPublisher).Validate();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Name:"));
            Assert.That(result.Errors, Has.Some.Contains("Birthday:"));
        });
    }
}