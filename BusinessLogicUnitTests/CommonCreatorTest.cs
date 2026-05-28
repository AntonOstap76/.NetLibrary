using CommonRepositoryProject;
using CommonServiceProject;
using DomainProject;
using Moq;

namespace BusinessLogicTests;

public class CommonCreatorTest
{
    private Mock<ICommonCreatorRepository<Author>> _repositoryMock;
    private ICommonCreatorService<Author> _service;
    private Author _author;
    private Country _country;
    private Patent _patent;

    private class AuthorService : CommonCreatorService<Author>
    {
        public AuthorService(ICommonCreatorRepository<Author> repository) : base(repository)
        {
        }
    }

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<ICommonCreatorRepository<Author>>();

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _patent = new Patent(
            title: "stub",
            content: "stub",
            patentCode: "stub",
            publishDate: DateTime.MinValue,
            authors: new List<Author>()
        );

        _author = new Author(
            name: "John",
            lastName: "Johnson",
            country: _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities: null,
            patentId: _patent
        );

        _patent.Authors.Add(_author);

        _service = new AuthorService(_repositoryMock.Object);
    }

    [Test]
    public void Add_ValidAuthor_CallRepository()
    {
        //Arrange
        //Act
        _service.Add(_author);
        //Assert
        _repositoryMock.Verify(x => x.Add(_author), Times.Once);
    }

    [Test]
    public void Update_ValidAuthor_CallRepository()
    {
        //Arrange
        //Act
        _service.Update(_author);
        //Assert
        _repositoryMock.Verify(x => x.Update(_author), Times.Once);
    }

    [Test]
    public void Delete_ValidAuthor_CallRepository()
    {
        //Arrange
        //Act
        _service.Delete(_author);
        //Assert
        _repositoryMock.Verify(x => x.Delete(_author), Times.Once);
    }

    [Test]
    public async Task SaveChanges_Saved_ReturnsTrue()
    {
        //Arrange
        _repositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        //Act
        var result = await _service.SaveChangesAsync(It.IsAny<CancellationToken>());
        //Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task SaveChanges_NotSaved_ReturnsFalse()
    {
        //Arrange
        _repositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        //Act
        var result = await _service.SaveChangesAsync(It.IsAny<CancellationToken>());
        //Assert
        Assert.That(result, Is.False);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public void Exists_Exist_ReturnsTrue()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_author))
            .Returns(true);
        //Act
        var result = _service.Exists(_author);
        //Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.Exists(_author), Times.Once);
    }

    [Test]
    public void Exists_NotExisted_ReturnsFalse()
    {
        //Arrange
        _repositoryMock.Setup(r => r.Exists(_author))
            .Returns(false);
        //Act
        var result = _service.Exists(_author);
        //Assert
        Assert.That(result, Is.False);
        _repositoryMock.Verify(r => r.Exists(_author), Times.Once);
    }

    [Test]
    public async Task GetAllEntitiesAsync_ExistingId_ReturnsEntities()
    {
        //Arrange
        var id = Guid.NewGuid();
        var entities = new List<CommonEntity> { _patent };
        _repositoryMock
            .Setup(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entities);

        //Act
        var result = await _service.GetAllEntitiesAsync(id);

        //Assert
        Assert.That(result, Is.EqualTo(entities));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllEntitiesAsync_NoEntities_ReturnsEmptyList()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CommonEntity>());

        //Act
        var result = await _service.GetAllEntitiesAsync(id);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAllEntitiesAsync_NonExistingId_ReturnsEmptyList()
    {
        //Arrange
        var id = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CommonEntity>());

        //Act
        var result = await _service.GetAllEntitiesAsync(id);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAllEntitiesAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}