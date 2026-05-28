using DomainProject;
using MagazineIssueRepositoryProject;
using MagazineIssueServiceProject;
using Moq;

namespace BusinessLogicTests;

public class MagazineIssueTest
{
    private Mock<IMagazineIssueRepository> _repositoryMock;
    private MagazineIssueService _magazineIssueService;
    private MagazineIssue _magazineIssue;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;
    
    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IMagazineIssueRepository>();
        
        _country = new Country(
            name: "Poland",
            code: "32444",
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

        _magazineIssue = new MagazineIssue(
            number:        1,
            title:         "Valid Title",
            content:       "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn:  "12345678",
            magazine:      _magazine
        );
        
        _magazineIssueService =  new MagazineIssueService(_repositoryMock.Object);
        
    }
    
    [Test]
    public async Task GetAllByMagazineIdAsync_ExistingMagazineId_ReturnsIssues()
    {
        //Arrange
        var magazineId = Guid.NewGuid();
        var mIssues = new List<MagazineIssue>() { _magazineIssue };
        _repositoryMock
            .Setup(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(mIssues);
        //Act
        var result = await _magazineIssueService.GetAllByMagazineIdAsync(magazineId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(mIssues));
            Assert.That(result, Has.Count.EqualTo(1));
            _repositoryMock.Verify(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
    
    [Test]
    public async Task GetAllByMagazineIdAsync_ExistingMagazineId_ReturnsEmpty()
    {
        //Arrange
        var magazineId = Guid.NewGuid();
        var mIssues = new List<MagazineIssue>();
        _repositoryMock
            .Setup(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(mIssues);
        //Act
        var result = await _magazineIssueService.GetAllByMagazineIdAsync(magazineId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
    
    [Test]
    public async Task GetAllByMagazineIdAsync_NoExistingMagazineId_ReturnsNull()
    {
        //Arrange
        var magazineId = Guid.NewGuid();
        _repositoryMock
            .Setup(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((List<MagazineIssue>?)null);
        //Act
        var result = await _magazineIssueService.GetAllByMagazineIdAsync(magazineId);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAllByMagazineIdAsync(magazineId, It.IsAny<CancellationToken>()), Times.Once);
        });
    }
}