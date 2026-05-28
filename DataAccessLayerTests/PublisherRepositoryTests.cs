using DomainProject;
using PublisherRepositoryProject;

namespace DataAccessLayerTests;

public class PublisherRepositoryTests
{
    private PublisherRepository _repository;
    private Publisher _publisher;
    private Magazine _magazine;
    private Book _book;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new PublisherRepository();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _book = new Book(
            title: "Test Book",
            content: "Test Content",
            isbn: "123456789123",
            authorId: new List<Author>(),
            publisherId: null
        );

        _publisher = new Publisher(
            name: "Test Publisher",
            country: _country,
            foundedYear: new DateTime(2000, 1, 1),
            entities: new List<CommonEntity>() { _book },
            magazines: new List<Magazine>()
        );

        _magazine = new Magazine(
            issn: "1234",
            title: "Test Magazine",
            publisherId: _publisher,
            publisherDate: new DateTime(1978, 1, 1),
            endOfPublish: DateTime.Now
        );

        _publisher.Magazines.Add(_magazine);
        _book.PublisherID = _publisher;
    }

    [Test]
    public async Task GetAllMagazinesAsync_ValidPublisherWithMagazines_ReturnsMagazines()
    {
        // Arrange
        _repository.Add(_publisher);

        // Act
        var result = await _repository.GetAllMagazinesAsync(_publisher.Id);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result, Contains.Item(_magazine));
    }

    [Test]
    public async Task GetAllMagazinesAsync_PublisherWithNoMagazines_ReturnsEmptyList()
    {
        // Arrange
        _publisher.Magazines = new List<Magazine>(); 
        _repository.Add(_publisher);

        // Act
        var result = await _repository.GetAllMagazinesAsync(_publisher.Id);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllMagazinesAsync_PublisherNotInDatabase_ThrowsNotFoundException()
    {
        // Act
        TestDelegate act = () => _repository.GetAllMagazinesAsync(_publisher.Id);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetAllMagazinesAsync_InvalidPublisherId_ThrowsNotFoundException()
    {
        // Arrange
        _repository.Add(_publisher);
        var fakeId = Guid.NewGuid();

        // Act
        TestDelegate act = () => _repository.GetAllMagazinesAsync(fakeId);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetAllBooksAsync_ValidPublisherWithBooks_ReturnsBooks()
    {
        // Arrange
        _repository.Add(_publisher);

        // Act
        var result = await _repository.GetAllBooksAsync(_publisher.Id);

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result, Contains.Item(_book));
    }

    [Test]
    public async Task GetAllBooksAsync_PublisherWithNoBooks_ReturnsEmptyList()
    {
        // Arrange
        _publisher.Entities = new List<CommonEntity>(); 
        _repository.Add(_publisher);

        // Act
        var result = await _repository.GetAllBooksAsync(_publisher.Id);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllBooksAsync_PublisherNotInDatabase_ThrowsNotFoundExceptio()
    {
        // Act
        TestDelegate act = () => _repository.GetAllBooksAsync(_publisher.Id);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetAllBooksAsync_InvalidPublisherId_ThrowsNotFoundException()
    {
        // Arrange
        _repository.Add(_publisher);
        var fakeId = Guid.NewGuid();

        // Act
        TestDelegate act = () => _repository.GetAllBooksAsync(fakeId);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }
}