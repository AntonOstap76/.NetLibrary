using BookRepositoryProject;
using BookServiceProject;
using DomainProject;
using NUnit.Framework;

namespace IntegrationTests;


public class BookServiceIntegrationTests
{
    private BookRepository _repository;
    private BookService _service;
    private Book _book;
    private Publisher _publisher;
    private Country _country;
    private Author _author;
    private Patent _patent;

    [SetUp]
    public void Setup()
    {
        _repository = new BookRepository();
        _service = new BookService(_repository);

        _country = new Country(
            name:     "Poland",
            code:     "32444",
            language: "Polish"
        );

        _publisher = new Publisher(
            name:        "Ban",
            country:     _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities:    null,
            magazines:   null
        );

        _patent = new Patent(
            title:       "stub",
            content:     "stub",
            patentCode:  "stub",
            publishDate: DateTime.MinValue,
            authors:     new List<Author>()
        );

        _author = new Author(
            name:         "John",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities:     null,
            patentId:     _patent
        );

        _patent.Authors.Add(_author);

        _book = new Book(
            title:       "Valid Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890123",
            authorId:    new List<Author> { _author },
            publisherId: _publisher
        );
    }

    [Test]
    public async Task GetAllByAuthorAsync_ExistingAuthor_ReturnsBooks()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetAllByAuthorAsync(_author.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetAllByAuthorAsync_NonExistingAuthor_ReturnsNull()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetAllByAuthorAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllByAuthorAsync_MultipleBooks_ReturnsAllForAuthor()
    {
        //Arrange
        var secondBook = new Book(
            title:       "Second Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890124",
            authorId:    new List<Author> { _author },
            publisherId: _publisher
        );

        _service.Create(_book);
        _service.Create(secondBook);

        //Act
        var result = await _service.GetAllByAuthorAsync(_author.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllByAuthorAsync_BookFromDifferentAuthor_NotReturned()
    {
        //Arrange
        var otherAuthor = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     null,
            patentId:     _patent
        );

        var otherBook = new Book(
            title:       "Other Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890125",
            authorId:    new List<Author> { otherAuthor },
            publisherId: _publisher
        );

        _service.Create(_book);
        _service.Create(otherBook);

        //Act
        var result = await _service.GetAllByAuthorAsync(_author.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetAllByPublisherAsync_ExistingPublisher_ReturnsBooks()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetAllByPublisherAsync(_publisher.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetAllByPublisherAsync_NonExistingPublisher_ReturnsNull()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetAllByPublisherAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllByPublisherAsync_MultipleBooks_ReturnsAllForPublisher()
    {
        //Arrange
        var secondBook = new Book(
            title:       "Second Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890124",
            authorId:    new List<Author> { _author },
            publisherId: _publisher
        );

        _service.Create(_book);
        _service.Create(secondBook);

        //Act
        var result = await _service.GetAllByPublisherAsync(_publisher.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllByPublisherAsync_BookFromDifferentPublisher_NotReturned()
    {
        //Arrange
        var otherPublisher = new Publisher(
            name:        "Other",
            country:     _country,
            foundedYear: new DateTime(2000, 1, 1),
            entities:    null,
            magazines:   null
        );

        var otherBook = new Book(
            title:       "Other Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890125",
            authorId:    new List<Author> { _author },
            publisherId: otherPublisher
        );

        _service.Create(_book);
        _service.Create(otherBook);

        //Act
        var result = await _service.GetAllByPublisherAsync(_publisher.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result![0].Id, Is.EqualTo(_book.Id));
    }
}