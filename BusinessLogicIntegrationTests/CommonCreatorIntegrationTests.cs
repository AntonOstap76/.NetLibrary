using AuthorRepositoryProject;
using AuthorServiceProject;
using DomainProject;
using NUnit.Framework;

namespace IntegrationTests;

public class CommonCreatorServiceIntegrationTests
{
    private AuthorRepository _repository;
    private AuthorService _service;
    private Author _author;
    private Patent _patent;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new AuthorRepository();
        _service = new AuthorService(_repository);

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

        _patent.Authors.Add(_author);
    }

    [Test]
    public void Add_ValidAuthor_ExistsInRepository()
    {
        //Act
        _service.Add(_author);

        //Assert
        Assert.That(_service.Exists(_author), Is.True);
    }

    [Test]
    public void Exists_NonExistingAuthor_ReturnsFalse()
    {
        //Act + Assert
        Assert.That(_service.Exists(_author), Is.False);
    }

    [Test]
    public void Update_ExistingAuthor_ChangesAreReflected()
    {
        //Arrange
        _service.Add(_author);
        _author.Name = "Updated";

        //Act
        _service.Update(_author);

        //Assert
        Assert.That(_repository._databaseCreator[0].Name, Is.EqualTo("Updated"));
    }

    [Test]
    public void Update_NonExistingAuthor_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Update(_author));
    }

    [Test]
    public void Delete_ExistingAuthor_NoLongerExists()
    {
        //Arrange
        _service.Add(_author);

        //Act
        _service.Delete(_author);

        //Assert
        Assert.That(_service.Exists(_author), Is.False);
    }

    [Test]
    public void Delete_NonExistingAuthor_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Delete(_author));
    }

    [Test]
    public async Task SaveChangesAsync_ReturnsTrue()
    {
        //Act
        var result = await _service.SaveChangesAsync();

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public async Task GetAllEntitiesAsync_AuthorWithEntities_ReturnsEntities()
    {
        //Arrange
        var authorWithEntities = new Author(
            name: "John",
            lastName: "Johnson",
            country: _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities: new List<CommonEntity> { _patent },
            patentId: _patent
        );
        _service.Add(authorWithEntities);

        //Act
        var result = await _service.GetAllEntitiesAsync(authorWithEntities.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetAllEntitiesAsync_AuthorWithNoEntities_ReturnsEmptyList()
    {
        //Arrange
        _service.Add(_author);

        //Act
        var result = await _service.GetAllEntitiesAsync(_author.Id);

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllEntitiesAsync_NonExistingAuthorId_ReturnsEmptyList()
    {
        //Act
        var result = await _service.GetAllEntitiesAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Empty);
    }
}