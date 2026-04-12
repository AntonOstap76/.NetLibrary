using DomainProject;
using NUnit.Framework;
using PublisherRepositoryProject;
using PublisherServiceProject;

namespace IntegrationTests;

public class PublisherServiceIntegrationTests
{
    private PublisherRepository _repository;
    private PublisherService _service;
    private Publisher _publisher;
    private Magazine _magazine;
    private Book _book;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new PublisherRepository();
        _service = new PublisherService(_repository);

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _magazine = new Magazine(
            issn: "12345678",
            title: "Valid Magazine Title",
            publisherId: null!,
            publisherDate: new DateTime(2000, 1, 1),
            endOfPublish: new DateTime(2020, 1, 1)
        );

        _publisher = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: null,
            magazines: new List<Magazine> { _magazine }
        );

        _book = new Book(
            title: "Valid Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            isbn: "1234567890123",
            authorId: new List<Author>(),
            publisherId: _publisher
        );
    }

    [Test]
    public async Task GetAllMagazinesAsync_PublisherWithMagazines_ReturnsMagazines()
    {
        //Arrange
        _service.Add(_publisher);

        //Act
        var result = await _service.GetAllMagazinesAsync(_publisher.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(_magazine.Id));
    }

    [Test]
    public async Task GetAllMagazinesAsync_PublisherWithNoMagazines_ReturnsEmptyList()
    {
        //Arrange
        var publisherWithNoMagazines = new Publisher(
            name: "Empty",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: null,
            magazines: new List<Magazine>()
        );
        _service.Add(publisherWithNoMagazines);

        //Act
        var result = await _service.GetAllMagazinesAsync(publisherWithNoMagazines.Id);

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllMagazinesAsync_PublisherWithMultipleMagazines_ReturnsAll()
    {
        //Arrange
        var secondMagazine = new Magazine(
            issn: "87654321",
            title: "Second Magazine Title",
            publisherId: null!,
            publisherDate: new DateTime(2001, 1, 1),
            endOfPublish: new DateTime(2021, 1, 1)
        );

        var publisherWithMultipleMagazines = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: null,
            magazines: new List<Magazine> { _magazine, secondMagazine }
        );
        _service.Add(publisherWithMultipleMagazines);

        //Act
        var result = await _service.GetAllMagazinesAsync(publisherWithMultipleMagazines.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetAllMagazinesAsync_NonExistingPublisherId_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.ThrowsAsync<NotFoundException>(() => _service.GetAllMagazinesAsync(Guid.NewGuid()));
    }

    [Test]
    public async Task GetAllBooksAsync_PublisherWithBooks_ReturnsBooks()
    {
        //Arrange
        var publisherWithBooks = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: new List<CommonEntity> { _book },
            magazines: null
        );
        _service.Add(publisherWithBooks);

        //Act
        var result = await _service.GetAllBooksAsync(publisherWithBooks.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetAllBooksAsync_PublisherWithNoBooks_ReturnsEmptyList()
    {
        //Arrange
        var publisherWithNoBooks = new Publisher(
            name: "Empty",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: new List<CommonEntity>(),
            magazines: null
        );
        _service.Add(publisherWithNoBooks);

        //Act
        var result = await _service.GetAllBooksAsync(publisherWithNoBooks.Id);

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllBooksAsync_PublisherWithMultipleBooks_ReturnsAll()
    {
        //Arrange
        var secondBook = new Book(
            title: "Second Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            isbn: "1234567890124",
            authorId: new List<Author>(),
            publisherId: _publisher
        );

        var publisherWithMultipleBooks = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: new List<CommonEntity> { _book, secondBook },
            magazines: null
        );
        _service.Add(publisherWithMultipleBooks);

        //Act
        var result = await _service.GetAllBooksAsync(publisherWithMultipleBooks.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetAllBooksAsync_NonExistingPublisherId_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.ThrowsAsync<NotFoundException>(() => _service.GetAllBooksAsync(Guid.NewGuid()));
    }
}