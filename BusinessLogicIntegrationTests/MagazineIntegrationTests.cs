using DomainProject;
using MagazineRepositoryProject;
using MagazineServiceProject;
using NUnit.Framework;

namespace IntegrationTests;

public class MagazineServiceIntegrationTests
{
    private MagazineRepository _repository;
    private MagazineService _service;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new MagazineRepository();
        _service = new MagazineService(_repository);

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

        _magazine = new Magazine(
            issn: "12345678",
            title: "Valid Magazine Title",
            publisherId: _publisher,
            publisherDate: new DateTime(2000, 1, 1),
            endOfPublish: new DateTime(2020, 1, 1)
        );
    }

    [Test]
    public void Create_ValidMagazine_ExistsInRepository()
    {
        //Act
        _service.Create(_magazine);

        //Assert
        Assert.That(_service.Exists(_magazine), Is.True);
    }

    [Test]
    public async Task Create_ValidMagazine_CanBeRetrievedById()
    {
        //Act
        _service.Create(_magazine);
        var result = await _service.GetByIdAsync(_magazine.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(_magazine.Id));
        Assert.That(result.Title, Is.EqualTo(_magazine.Title));
    }

    [Test]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        //Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Create_MultipleMagazines_GetAllReturnsAll()
    {
        //Arrange
        var secondMagazine = new Magazine(
            issn: "87654321",
            title: "Second Magazine Title",
            publisherId: _publisher,
            publisherDate: new DateTime(2001, 1, 1),
            endOfPublish: new DateTime(2021, 1, 1)
        );

        //Act
        _service.Create(_magazine);
        _service.Create(secondMagazine);
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllAsync_NoMagazines_ReturnsEmptyList()
    {
        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task Update_ExistingMagazine_ChangesAreReflected()
    {
        //Arrange
        _service.Create(_magazine);
        var magazineToUpdate = await _service.GetByIdAsync(_magazine.Id);
        magazineToUpdate!.Title = "Updated Title";

        //Act
        _service.Update(magazineToUpdate);
        var result = await _service.GetByIdAsync(_magazine.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("Updated Title"));
    }

    [Test]
    public void Update_NonExistingMagazine_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Update(_magazine));
    }

    [Test]
    public void Delete_ExistingMagazine_NoLongerExists()
    {
        //Arrange
        _service.Create(_magazine);

        //Act
        _service.Delete(_magazine);

        //Assert
        Assert.That(_service.Exists(_magazine), Is.False);
    }

    [Test]
    public void Delete_NonExistingMagazine_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Delete(_magazine));
    }

    [Test]
    public async Task GetMagazineAsync_ExistingIssueId_ReturnsMagazine()
    {
        //Arrange
        var magazineIssue = new MagazineIssue(
            number: 1,
            title: "Valid Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn: "12345678",
            magazine: _magazine
        );

        _magazine.MagazineIssues = new List<MagazineIssue> { magazineIssue };
        _service.Create(_magazine);

        //Act
        var result = await _service.GetMagazineAsync(magazineIssue.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(_magazine.Id));
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_ExistingPublisher_ReturnsMagazines()
    {
        //Arrange
        _service.Create(_magazine);

        //Act
        var result = await _service.GetAllAsync(_publisher.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(_magazine.Id));
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_NonExistingPublisher_ReturnsEmptyList()
    {
        //Arrange
        _service.Create(_magazine);

        //Act
        var result = await _service.GetAllAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_MultipleMagazines_ReturnsAllForPublisher()
    {
        //Arrange
        var secondMagazine = new Magazine(
            issn: "87654321",
            title: "Second Magazine Title",
            publisherId: _publisher,
            publisherDate: new DateTime(2001, 1, 1),
            endOfPublish: new DateTime(2021, 1, 1)
        );

        _service.Create(_magazine);
        _service.Create(secondMagazine);

        //Act
        var result = await _service.GetAllAsync(_publisher.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_MagazineFromDifferentPublisher_NotReturned()
    {
        //Arrange
        var otherPublisher = new Publisher(
            name: "Other",
            country: _country,
            foundedYear: new DateTime(2000, 1, 1),
            entities: null,
            magazines: null
        );

        var otherMagazine = new Magazine(
            issn: "87654321",
            title: "Other Magazine Title",
            publisherId: otherPublisher,
            publisherDate: new DateTime(2001, 1, 1),
            endOfPublish: new DateTime(2021, 1, 1)
        );

        _service.Create(_magazine);
        _service.Create(otherMagazine);

        //Act
        var result = await _service.GetAllAsync(_publisher.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(_magazine.Id));
    }
}