using DomainProject;
using Moq;
using PatentRepositoryProject;
using PatentServiceProject;

namespace BusinessLogicTests;

public class PatentServiceTest
{
    private Mock<IPatentRepository> _repositoryMock;
    private PatentService _service;
    private Patent _patent;
    private Author _author;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IPatentRepository>();

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

        _service = new PatentService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAsync_ExistingAuthorId_ReturnsPatents()
    {
        //Arrange
        var patents = new List<Patent>() { _patent };
        _repositoryMock
            .Setup(r => r.GetAsync(_author, It.IsAny<CancellationToken>()))
            .ReturnsAsync(patents);
        //Act
        var result = await _service.GetAsync(_author);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(patents));
            Assert.That(result, Has.Count.EqualTo(1));
            _repositoryMock.Verify(r => r.GetAsync(_author, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
    
    [Test]
    public async Task GetAsync_ExistingAuthorId_EmptyList()
    {
        //Arrange
        var patents = new List<Patent>();
        _repositoryMock
            .Setup(r => r.GetAsync(_author, It.IsAny<CancellationToken>()))
            .ReturnsAsync(patents);
        //Act
        var result = await _service.GetAsync(_author);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAsync(_author, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
    
    [Test]
    public async Task GetAsync_NoExistingAuthorId_Null()
    {
        //Arrange
        _repositoryMock
            .Setup(r => r.GetAsync(_author, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<Patent>?)null);
        //Act
        var result = await _service.GetAsync(_author);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAsync(_author, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
    
    [Test]
    public async Task GetAsync_ByAuthors_ExistingAuthors_ReturnsPatents()
    {
        //Arrange
        var authors = new List<Author> { _author };
        var patents = new List<Patent?> { _patent };
        _repositoryMock
            .Setup(r => r.GetAsync(authors, It.IsAny<CancellationToken>()))
            .ReturnsAsync(patents);

        //Act
        var result = await _service.GetAsync(authors);

        //Assert
        Assert.That(result, Is.EqualTo(patents));
        Assert.That(result, Has.Count.EqualTo(1));
        _repositoryMock.Verify(r => r.GetAsync(authors, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAsync_ByAuthors_NoPatents_ReturnsEmptyList()
    {
        //Arrange
        var authors = new List<Author> { _author };
        _repositoryMock
            .Setup(r => r.GetAsync(authors, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Patent?>());

        //Act
        var result = await _service.GetAsync(authors);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAsync(authors, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAsync_ByAuthors_EmptyAuthorsList_ReturnsEmptyList()
    {
        //Arrange
        var authors = new List<Author>();
        _repositoryMock
            .Setup(r => r.GetAsync(authors, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Patent?>());

        //Act
        var result = await _service.GetAsync(authors);

        //Assert
        Assert.That(result, Is.Empty);
        _repositoryMock.Verify(r => r.GetAsync(authors, It.IsAny<CancellationToken>()), Times.Once);
    }
}