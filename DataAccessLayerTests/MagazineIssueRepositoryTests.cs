using DomainProject;
using MagazineIssueRepositoryProject;

namespace DataAccessLayerTests;

public class MagazineIssueRepositoryTests
{
    private MagazineIssueRepository _repository;
    private Magazine _magazine;
    private MagazineIssue _magazineIssue;
    private Publisher _publisher;

    [SetUp]
    public void Setup()
    {
        _repository = new MagazineIssueRepository();
        
        var country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _publisher = new Publisher(
            name: "Test Publisher",
            country: country,
            foundedYear: new DateTime(2000, 1, 1),
            entities: null,
            magazines: new List<Magazine>()
        );

        _magazine = new Magazine(
            issn:"1234",
            title:"Title",
            publisherId: _publisher,
            publisherDate:new DateTime(1978, 1, 1),
            endOfPublish: DateTime.Now
        );

        _magazineIssue = new MagazineIssue(
            number: 1,
            title: "Test Issue",
            content: "Test Content",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn: "12345678",
            magazine: _magazine
        );

        _publisher.Magazines.Add(_magazine);
        
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_ValidMagazineId_GetMAgazineIssue()
    {
        //Arrange
        _repository.Create(_magazineIssue);
        
        //Act
        var result = await _repository.GetAllByMagazineIdAsync(_magazine.Id);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetAllByMagazineIdAsync_InValidMagazineId_NullResult()
    {
        //Arrange
        _repository.Create(_magazineIssue);
        var fake = new Guid();
        
        //Act
        var result = await _repository.GetAllByMagazineIdAsync(fake);
        
        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_ValidMagazineIdWithMultipleMagazines_GetMAgazineIssues()
    {
        //Arrange
        var magazineIssue2 = new MagazineIssue(
            number: 1,
            title: "Test Issue2",
            content: "Test Content2",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn: "12345678",
            magazine: null
        );
        magazineIssue2.Magazine = _magazine;
        _repository.Create(_magazineIssue);
        _repository.Create(magazineIssue2);
        
        //Act
        var result = await _repository.GetAllByMagazineIdAsync(_magazine.Id);
        
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }
    
    
}