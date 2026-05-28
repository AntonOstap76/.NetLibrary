using AuthorRepositoryProject;
using AuthorServiceProject;
using DomainProject;
using NUnit.Framework;

namespace IntegrationTests;

public class AuthorServiceIntegrationTests
{
    private AuthorRepository _repository;
    private AuthorService _service;
    private Author _author;
    private Patent _patent;
    private Book _book;
    private Country _country;
    private Publisher _publisher;

    [SetUp]
    public void Setup()
    {
        _repository = new AuthorRepository();
        _service = new AuthorService(_repository);

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
            title:       "Valid Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "1234567890",
            publishDate: new DateTime(2020, 1, 1),
            authors:     new List<Author>()
        );

        _book = new Book(
            title:       "Valid Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            isbn:        "1234567890123",
            authorId:    new List<Author>(),
            publisherId: _publisher
        );

        _author = new Author(
            name:         "John",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities:     new List<CommonEntity> { _patent, _book },
            patentId:     _patent
        );

        _patent.Authors.Add(_author);
    }
    
    [Test]
    public async Task GetAllPatentsAsync_AuthorWithPatents_ReturnsPatents()
    {
        //Arrange
        _service.Add(_author);

        //Act
        var result = await _service.GetAllPatentsAsync(_author.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_patent.Id));
    }

    [Test]
    public async Task GetAllPatentsAsync_AuthorWithNoPatents_ReturnsEmptyList()
    {
        //Arrange
        var authorWithNoPatents = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     new List<CommonEntity>(),
            patentId:     _patent
        );
        _service.Add(authorWithNoPatents);

        //Act
        var result = await _service.GetAllPatentsAsync(authorWithNoPatents.Id);

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetAllPatentsAsync_NonExistingAuthorId_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.ThrowsAsync<NotFoundException>(() => _service.GetAllPatentsAsync(Guid.NewGuid()));
    }

    [Test]
    public async Task GetAllBooksAsync_AuthorWithBooks_ReturnsBooks()
    {
        //Arrange
        _service.Add(_author);

        //Act
        var result = await _service.GetAllBooksAsync(_author.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetAllBooksAsync_AuthorWithNoBooks_ReturnsEmptyList()
    {
        //Arrange
        var authorWithNoBooks = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     new List<CommonEntity>(),
            patentId:     _patent
        );
        _service.Add(authorWithNoBooks);

        //Act
        var result = await _service.GetAllBooksAsync(authorWithNoBooks.Id);

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetAllBooksAsync_NonExistingAuthorId_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.ThrowsAsync<NotFoundException>(() => _service.GetAllBooksAsync(Guid.NewGuid()));
    }

    [Test]
    public async Task GetAuthorAsync_ExistingPatentId_ReturnsAuthor()
    {
        //Arrange
        _service.Add(_author);

        //Act
        var result = await _service.GetAuthorAsync(_patent.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(_author.Id));
    }

    [Test]
    public void GetAuthorAsync_NonExistingPatentId_ThrowsNotFoundException()
    {
        //Arrange
        _service.Add(_author);

        //Act + Assert
        Assert.ThrowsAsync<NotFoundException>(() => _service.GetAuthorAsync(Guid.NewGuid()));
    }
}