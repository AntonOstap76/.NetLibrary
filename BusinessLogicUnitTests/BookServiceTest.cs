using BookRepositoryProject;
using BookServiceProject;
using DomainProject;
using Moq;

namespace BusinessLogicTests;

public class BookServiceTest
{
    private Mock<IBookRepository> _repositoryMock;
    private BookService _service;
    private Book _book;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IBookRepository>();

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

        _book = new Book(
            title: "Valid Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            isbn: "1234567890123",
            authorId: new List<Author>(),
            publisherId: _publisher
        );

        _service = new BookService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllByAuthorAsync_ExistingAuthorId_ReturnsBooks()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var books = new List<Book>() { _book };
        _repositoryMock
            .Setup(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        //Act
        var result = await _service.GetAllByAuthorAsync(authorId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(books));
            Assert.That(result, Has.Count.EqualTo(1));
            _repositoryMock.Verify(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }

    [Test]
    public async Task GetAllByAuthorAsync_NoBooks_ReturnsEmptyList()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        var books = new List<Book>();
        _repositoryMock
            .Setup(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);
        //Act
        var result = await _service.GetAllByAuthorAsync(authorId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }

    [Test]
    public async Task GetAllByAuthorAsync_NoExistingAuthorId_ReturnsNull()
    {
        //Arrange
        var authorId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Book>?)null);
        //Act
        var result = await _service.GetAllByAuthorAsync(authorId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAllByAuthorAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }

    [Test]
    public async Task GetAllByPublisherAsync_ExistingPublisherId_ReturnsBooks()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        var books = new List<Book> { _book };
        _repositoryMock
            .Setup(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        //Act
        var result = await _service.GetAllByPublisherAsync(publisherId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(books));
            Assert.That(result, Has.Count.EqualTo(1));
            _repositoryMock.Verify(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()),
                Times.Once);
        });
    }
    
    [Test]
    public async Task GetAllByPublisherAsync_NoBooks_EmptyList()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        var books = new List<Book>();
        _repositoryMock
            .Setup(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        //Act
        var result = await _service.GetAllByPublisherAsync(publisherId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()),
                Times.Once);
        });
    }
    
    [Test]
    public async Task GetAllByPublisherAsync_NoExistingPublisherID_Null()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Book>?)null);

        //Act
        var result = await _service.GetAllByPublisherAsync(publisherId);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAllByPublisherAsync(publisherId, It.IsAny<CancellationToken>()),
                Times.Once);
        });
    }
}