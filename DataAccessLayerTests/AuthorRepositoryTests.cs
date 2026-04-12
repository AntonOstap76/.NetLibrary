using AuthorRepositoryProject;
using DomainProject;

namespace DataAccessLayerTests;

public class AuthorRepositoryTests
{
    private AuthorRepository _repository;
    private Author _author;
    private Patent _patent;
    private Book _book;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new AuthorRepository();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _patent = new Patent(
            title: "Test Patent",
            content: "Test Content",
            patentCode: "1234561234",
            publishDate: new DateTime(2005, 3, 4),
            authors: new List<Author>()
        );

        _book = new Book(
            title: "Test Book",
            content: "Test Content",
            isbn: "123456789123",
            authorId: new List<Author>(),
            publisherId: null
        );

        _author = new Author(
            name: "Bob",
            lastName: "Bobowicz",
            country: _country,
            birthdayYear: new DateTime(1997, 1, 1),
            entities: new List<CommonEntity>() { _patent, _book },
            patentId: _patent
        );

        _patent.Authors.Add(_author);
        _book.AuthorIDs.Add(_author);
    }

    [Test]
    public async Task GetAllPatentsAsync_AuthorWithPatent_ReturnsPatents()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        var result = await _repository.GetAllPatentsAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result, Contains.Item(_patent));
    }

    [Test]
    public async Task GetAllPatentsAsync_AuthorWithNoPatents_ReturnsEmptyList()
    {
        // Arrange
        _author.Entities = new List<CommonEntity>();
        _repository.Add(_author);

        // Act
        var result = await _repository.GetAllPatentsAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllPatentsAsync_AuthorNotInDatabase_ThrowsNotFoundException()
    {
        // Act
        TestDelegate act = () => _repository.GetAllPatentsAsync(_author.Id);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetAllBooksAsync_AuthorWithBook_ReturnsBooks()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        var result = await _repository.GetAllBooksAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result, Contains.Item(_book));
    }

    [Test]
    public async Task GetAllBooksAsync_AuthorWithNoBooks_ReturnsEmptyList()
    {
        // Arrange
        _author.Entities = new List<CommonEntity>(); // no books
        _repository.Add(_author);

        // Act
        var result = await _repository.GetAllBooksAsync(_author.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllBooksAsync_AuthorNotInDatabase_ThrowsNotFoundException()
    {
        // Act
        TestDelegate act = () => _repository.GetAllPatentsAsync(_author.Id);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetAuthorAsync_ValidPatentId_ReturnsAuthor()
    {
        // Arrange
        _repository.Add(_author);

        // Act
        var result = await _repository.GetAuthorAsync(_patent.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(_author));
    }

    [Test]
    public async Task GetAuthorAsync_InvalidPatentId_ReturnsNull()
    {
        // Arrange
        _repository.Add(_author);
        var fakeId = Guid.NewGuid();

        // Act
        TestDelegate act = ()=>  _repository.GetAuthorAsync(fakeId);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }
}