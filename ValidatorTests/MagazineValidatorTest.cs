using DomainProject;
using ValidatorProject;
using NUnit.Framework;

namespace ValidatorTests;

[TestFixture]
public class MagazineValidatorTests
{
    private IValidator<Magazine> _validator;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _validator = new Validator<Magazine>();

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

        _magazine = new Magazine(
            issn:          "12345678",
            title:         "Valid Magazine Title",
            publisherId:   _publisher,
            publisherDate: new DateTime(2000, 1, 1),
            endOfPublish:  new DateTime(2020, 1, 1)
        );
    }

    [Test]
    public void ValidateIssn_ValidIssn_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateIssn("12345678").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateIssn_TooShort_AddsIssnError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateIssn("1234").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Issn: value must exactly 8 characters long"));
    }

    [Test]
    public void ValidateIssn_TooLong_AddsIssnError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateIssn("123456789").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Issn: value must exactly 8 characters long"));
    }

    [Test]
    public void ValidateIssn_ContainsLetters_AddsIssnError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateIssn("1234567A").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Issn: value must contain only digits"));
    }

    [Test]
    public void ValidateIssn_EmptyIssn_AddsIssnError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateIssn("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateTitle_ValidTitle_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Valid Magazine Title").Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateTitle_TooShort_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Short").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Title: value length must be between 10 and 250"));
    }

    [Test]
    public void ValidateTitle_TooLong_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle(new string('A', 251)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Title: value length must be between 10 and 250"));
    }

    [Test]
    public void ValidateTitle_EmptyTitle_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Value cannot be empty"));
    }

    [Test]
    public void ValidateStartDate_ValidDate_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateStartDate(
            startDate: new DateTime(2000, 1, 1),
            endDate:   new DateTime(2020, 1, 1)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateStartDate_FutureDate_AddsStartDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateStartDate(
            startDate: DateTime.Now.AddYears(1),
            endDate:   DateTime.Now.AddYears(2)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("StartDate:"));
    }

    [Test]
    public void ValidateStartDate_AfterEndDate_AddsStartDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateStartDate(
            startDate: new DateTime(2020, 1, 1),
            endDate:   new DateTime(2000, 1, 1)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("StartDate:"));
    }

    [Test]
    public void ValidateEndDate_ValidDate_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateEndDate(
            startDate: new DateTime(2000, 1, 1),
            endDate:   new DateTime(2020, 1, 1)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateEndDate_FutureDate_AddsEndDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateEndDate(
            startDate: new DateTime(2000, 1, 1),
            endDate:   DateTime.Now.AddYears(1)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("StartDate:"));
    }

    [Test]
    public void ValidateEndDate_BeforeStartDate_AddsEndDateError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateEndDate(
            startDate: new DateTime(2020, 1, 1),
            endDate:   new DateTime(2000, 1, 1)
        ).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Some.Contains("StartDate:"));
    }

    [Test]
    public void ValidatePublisherId_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidatePublisherId(_magazine).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidatePublisherId_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        var invalidMagazine = new Magazine(
            issn:          "123",                           
            title:         "Short",                         
            publisherId:   _publisher,
            publisherDate: new DateTime(2020, 1, 1),
            endOfPublish:  new DateTime(2000, 1, 1) 
        );

        //Act
        var result = _validator.ValidatePublisherId(invalidMagazine).Validate();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Issn:"));
            Assert.That(result.Errors, Has.Some.Contains("Title:"));
            Assert.That(result.Errors, Has.Some.Contains("StartDate:"));
        });
    }
}