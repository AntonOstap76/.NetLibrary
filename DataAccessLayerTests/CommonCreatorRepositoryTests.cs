using CommonRepositoryProject;
using DomainProject;

namespace DataAccessLayerTests;

public class CommonCreatorRepositoryTests
{
    private CommonCreatorRepository<Author> _repository;
    private Author _author;
    private Country _country;
    private Book _book;

    [SetUp]
    public void Setup()
    {
        _repository = new CommonCreatorRepository<Author>();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _book = new Book(
            title: "Test Book",
            content: "Test Content",
            isbn: "123456789123",
            authorId: new List<Author>() { _author },
            publisherId: null
        );

        _author = new Author(
            name: "Bob",
            lastName: "Bobowicz",
            country: _country,
            birthdayYear: new DateTime(1997, 1, 1),
            entities: new List<CommonEntity>(),
            patentId: null
        );
    }

    [Test]
    public void Add_ValidAuthor_AddedToDatabase()
    {
        // Act
        _repository.Add(_author);

        // Assert
        Assert.That(_repository._databaseCreator, Contains.Item(_author));
        Assert.That(_repository._databaseCreator.Count, Is.EqualTo(1));
    }

    [Test]
    public void Update_ExistingAuthor_Updated()
    {
        // Arrange
        _repository.Add(_author);
        _author.LastName = "Bob";

        // Act
        _repository.Update(_author);
        var result = _repository._databaseCreator.First(a => a.Id == _author.Id);

        // Assert
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._databaseCreator.Count, Is.EqualTo(1));
                Assert.That(_repository._databaseCreator, Is.Not.Null);
                Assert.That(result.LastName, Is.EqualTo("Bob"));
            }
        );

    }

    [Test]
    public void Update_NotExistingAuthor_DatabaseUnchanged()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        _repository.Update(_author);
        var result = _repository._databaseCreator.First(a => a.Id == _author.Id);

        // Assert
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._databaseCreator.Count, Is.EqualTo(1));
                Assert.That(_repository._databaseCreator, Is.Not.Null);
                Assert.That(result.LastName, Is.Not.EqualTo("Bob"));
            }
        );
    }

    [Test]
    public void Delete_ExistingAuthor_Deleted()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        _repository.Delete(_author);

        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._databaseCreator.Count, Is.EqualTo(0));
                Assert.That(_repository._databaseCreator.Count(b => b.Id == _author.Id), Is.EqualTo(0));
            }
        );
    }

    [Test]
    public void Delete_NotExistingAuthor_DatabaseUnchanged()
    {
        // Act
        TestDelegate  act = () => _repository.Delete(_author);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public void Exists_ExistingAuthor_ReturnsTrue()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        var result = _repository.Exists(_author);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Exists_NotExistingAuthor_ReturnsFalse()
    {
        // Act
        var result = _repository.Exists(_author);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task SaveChangesAsync_Nothing_True()
    {
        //Arrange
        //Act
        var result = await _repository.SaveChangesAsync();
        //Assert
        Assert.That(result, Is.True);
    }

[Test]
    public async Task GetAllEntitiesAsync_ExistingAuthorWithEntities_ReturnsEntities()
    {
        // Arrange
        _repository.Add(_author);
        _author.Entities?.Add(_book);

        // Act
        var result = await _repository.GetAllEntitiesAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result, Contains.Item(_book));
    }

    [Test]
    public async Task GetAllEntitiesAsync_NotExistingAuthor_ReturnsEmptyList()
    {
        // Act
        var result = await _repository.GetAllEntitiesAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Empty);
    }
}