using DomainProject;
using Moq;
using NUnit.Framework;
using PublisherRepositoryProject;
using PublisherServiceProject;

namespace BusinessLogicTests;

public class PublisherServiceTests
{
    private Mock<IPublisherRepository> _repositoryMock;
    private PublisherService _service;
    private Publisher _publisher;
    private Magazine _magazine;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPublisherRepository>();

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

        _service = new PublisherService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllMagazinesAsync_ExistingPublisherId_ReturnsMagazines()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        var magazines = new List<Magazine?> { _magazine };
        _repositoryMock
            .Setup(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(magazines);

        //Act
        var result = await _service.GetAllMagazinesAsync(publisherId);

        //Assert
        Assert.That(result, Is.EqualTo(magazines));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllMagazinesAsync_NoMagazines_ReturnsEmptyList()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Magazine?>());

        //Act
        var result = await _service.GetAllMagazinesAsync(publisherId);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllMagazinesAsync_NonExistingPublisherId_ReturnsNull()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Magazine?>?)null);

        //Act
        var result = await _service.GetAllMagazinesAsync(publisherId);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetAllMagazinesAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllBooksAsync_ExistingPublisherId_ReturnsBooks()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        var books = new List<Book?>();
        _repositoryMock
            .Setup(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(books);

        //Act
        var result = await _service.GetAllBooksAsync(publisherId);

        //Assert
        Assert.That(result, Is.EqualTo(books));
        _repositoryMock.Verify(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllBooksAsync_NoBooks_ReturnsEmptyList()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Book?>());

        //Act
        var result = await _service.GetAllBooksAsync(publisherId);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllBooksAsync_NonExistingPublisherId_ReturnsNull()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Book?>?)null);

        //Act
        var result = await _service.GetAllBooksAsync(publisherId);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetAllBooksAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }
}