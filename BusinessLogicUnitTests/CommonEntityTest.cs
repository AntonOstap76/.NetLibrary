using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;
using Moq;

namespace BusinessLogicTests;

public class CommonEntityServiceTests
{
    private Mock<ICommonEntityRepository<Book>> _repositoryMock;
    private ICommonEntityService<Book> _service;
    private Book _book;
    private Publisher _publisher;
    private Country _country;

    private class BookService : CommonEntityService<Book>
    {
        public BookService(ICommonEntityRepository<Book> repository) : base(repository)
        {
        }
    }

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<ICommonEntityRepository<Book>>();

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
    public void Create_ValidBook_CallsRepositoryCreate()
    {
        //Arrange
        //Act
        _service.Create(_book);

        //Assert
        _repositoryMock.Verify(r => r.Create(_book), Times.Once);
    }

    [Test]
    public void Update_ValidBook_CallsRepositoryUpdate()
    {
        //Arrange
        //Act
        _service.Update(_book);

        //Assert
        _repositoryMock.Verify(r => r.Update(_book), Times.Once);
    }

    [Test]
    public void Delete_ValidBook_CallsRepositoryDelete()
    {
        //Arrange
        //Act
        _service.Delete(_book);

        //Assert
        _repositoryMock.Verify(r => r.Delete(_book), Times.Once);
    }

    [Test]
    public void Exists_BookExists_ReturnsTrue()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_book)).Returns(true);

        //Act
        var result = _service.Exists(_book);

        //Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.Exists(_book), Times.Once);
    }

    [Test]
    public void Exists_BookDoesNotExist_ReturnsFalse()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_book)).Returns(false);

        //Act
        var result = _service.Exists(_book);

        //Assert
        Assert.That(result, Is.False);
        _repositoryMock.Verify(r => r.Exists(_book), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_ExistingId_ReturnsBook()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_book);

        //Act
        var result = await _service.GetByIdAsync(id);

        //Assert
        Assert.That(result, Is.EqualTo(_book));
        _repositoryMock.Verify(r => r.GetAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        //Act
        var result = await _service.GetByIdAsync(id);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_BooksExist_ReturnsAllBooks()
    {
        //Arrange
        var books = new List<Book> { _book };
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.EqualTo(books));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_NoBooksExist_ReturnsEmptyList()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Book>());

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetContentAsync_ExistingId_ReturnsContent()
    {
        //Arrange
        var id = Guid.NewGuid();
        var content = "This is the book content.";
        _repositoryMock
            .Setup(r => r.GetContentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(content);

        //Act
        var result = await _service.GetContentAsync(id);

        //Assert
        Assert.That(result, Is.EqualTo(content));
        _repositoryMock.Verify(r => r.GetContentAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetContentAsync_NonExistingId_ReturnsNull()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetContentAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((string?)null);

        //Act
        var result = await _service.GetContentAsync(id);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetContentAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}