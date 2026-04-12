using DomainProject;
using MagazineRepositoryProject;
using MagazineServiceProject;
using Moq;

namespace BusinessLogicTests;


public class MagazineServiceTests
{
    private Mock<IMagazineRepository> _repositoryMock;
    private MagazineService _service;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IMagazineRepository>();

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

        _magazine = new Magazine(
            issn:          "12345678",
            title:         "Valid Magazine Title",
            publisherId:   _publisher,
            publisherDate: new DateTime(2000, 1, 1),
            endOfPublish:  new DateTime(2020, 1, 1)
        );

        _service = new MagazineService(_repositoryMock.Object);
    }

    [Test]
    public void Create_ValidMagazine_CallsRepositoryCreate()
    {
        //Arrange
        //Act
        _service.Create(_magazine);

        //Assert
        _repositoryMock.Verify(r => r.Create(_magazine), Times.Once);
    }

    [Test]
    public void Update_ValidMagazine_CallsRepositoryUpdate()
    {
        //Arrange
        //Act
        _service.Update(_magazine);

        //Assert
        _repositoryMock.Verify(r => r.Update(_magazine), Times.Once);
    }

    [Test]
    public void Delete_ValidMagazine_CallsRepositoryDelete()
    {
        //Arrange
        //Act
        _service.Delete(_magazine);

        //Assert
        _repositoryMock.Verify(r => r.Delete(_magazine), Times.Once);
    }

    [Test]
    public void Exists_MagazineExists_ReturnsTrue()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_magazine)).Returns(true);

        //Act
        var result = _service.Exists(_magazine);

        //Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.Exists(_magazine), Times.Once);
    }

    [Test]
    public void Exists_MagazineDoesNotExist_ReturnsFalse()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_magazine)).Returns(false);

        //Act
        var result = _service.Exists(_magazine);

        //Assert
        Assert.That(result, Is.False);
        _repositoryMock.Verify(r => r.Exists(_magazine), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_ExistingId_ReturnsMagazine()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_magazine);

        //Act
        var result = await _service.GetByIdAsync(id);

        //Assert
        Assert.That(result, Is.EqualTo(_magazine));
        _repositoryMock.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Magazine?)null);

        //Act
        var result = await _service.GetByIdAsync(id);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_MagazinesExist_ReturnsAllMagazines()
    {
        //Arrange
        var magazines = new List<Magazine> { _magazine };
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(magazines);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.EqualTo(magazines));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_NoMagazines_ReturnsEmptyList()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Magazine>());

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetMagazineAsync_ExistingIssueId_ReturnsMagazine()
    {
        //Arrange
        var magazineIssueId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetMagazineAsync(magazineIssueId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_magazine);

        //Act
        var result = await _service.GetMagazineAsync(magazineIssueId);

        //Assert
        Assert.That(result, Is.EqualTo(_magazine));
        _repositoryMock.Verify(r => r.GetMagazineAsync(magazineIssueId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetMagazineAsync_NonExistingIssueId_ReturnsNull()
    {
        //Arrange
        var magazineIssueId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetMagazineAsync(magazineIssueId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Magazine?)null);

        //Act
        var result = await _service.GetMagazineAsync(magazineIssueId);

        //Assert
        Assert.That(result, Is.Null);
        _repositoryMock.Verify(r => r.GetMagazineAsync(magazineIssueId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_ExistingPublisher_ReturnsMagazines()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        var magazines = new List<Magazine> { _magazine };
        _repositoryMock
            .Setup(r => r.GetAllAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(magazines);

        //Act
        var result = await _service.GetAllAsync(publisherId);

        //Assert
        Assert.That(result, Is.EqualTo(magazines));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAllAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_ByPublisherId_NoMagazines_ReturnsEmptyList()
    {
        //Arrange
        var publisherId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllAsync(publisherId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Magazine>());

        //Act
        var result = await _service.GetAllAsync(publisherId);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllAsync(publisherId, It.IsAny<CancellationToken>()), Times.Once);
    }
}