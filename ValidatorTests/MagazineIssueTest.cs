using DomainProject;
using ValidatorProject;
using NUnit.Framework;

namespace ValidatorTests;

[TestFixture]
public class MagazineIssueValidatorTests
{
    private IValidator<MagazineIssue> _validator;
    private MagazineIssue _magazineIssue;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _validator = new Validator<MagazineIssue>();

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

        _magazineIssue = new MagazineIssue(
            number:       1,
            title:        "Valid Title",
            content:      "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn: "12345678",
            magazine:     _magazine
        );
    }

    [Test]
    public void ValidateNumber_ValidNumber_NoErrors()
    {
        //Act
        var result = _validator.ValidateNumber(1).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateNumber_Zero_AddsNumberError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateNumber(0).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Number: value must be more than 0"));
    }

    [Test]
    public void ValidateNumber_Negative_AddsNumberError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateNumber(-1).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Number: value must be more than 0"));
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
    public void ValidateTitle_TooShort_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle("Ab").Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains($"Title: value length must be between 5 and 255"));
    }

    [Test]
    public void ValidateTitle_TooLong_AddsTitleError()
    {
        //Arrange
        //Act
        var result = _validator.ValidateTitle(new string('A', 256)).Validate();

        //Assert
        Assert.That(result.IsValid, Is.False);
        Assert.That(result.Errors, Has.Exactly(1).Contains("Title: value length must be between 5 and 255"));
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
    public void ValidateMagazine_ValidMagazineId_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateMagazine(_magazineIssue).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
        Assert.That(result.Errors, Is.Null);
    }

    [Test]
    public void ValidateMagazineIssue_AllFieldsValid_NoErrors()
    {
        //Arrange
        //Act
        var result = _validator.ValidateMagazineIssue(_magazineIssue).Validate();

        //Assert
        Assert.That(result.IsValid, Is.True);
    }

    [Test]
    public void ValidateMagazineIssue_MultipleInvalidFields_AccumulatesAllErrors()
    {
        //Arrange
        var invalidMagazineIssue = new MagazineIssue(
            number:        0,                       
            title:         "Ab",                    
            content:       "Short",                 
            publisherDate: DateTime.Now.AddYears(1),
            magazineIssn:  "123",                   
            magazine:      _magazine
        );

        //Act
        var result = _validator.ValidateMagazineIssue(invalidMagazineIssue).Validate();

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors, Has.Some.Contains("Number:"));
            Assert.That(result.Errors, Has.Some.Contains("Title:"));
            Assert.That(result.Errors, Has.Some.Contains("Date:"));
            Assert.That(result.Errors, Has.Some.Contains("Issn:"));
            Assert.That(result.Errors, Has.Some.Contains("Content:"));
        });
    }
}