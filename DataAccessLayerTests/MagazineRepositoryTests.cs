using DomainProject;
using MagazineRepositoryProject;

namespace DataAccessLayerTests;

public class MagazineRepositoryTests
{
    private MagazineRepository _repository;
    private Publisher _publisher;
    private Magazine _magazine;
    private Magazine _magazine2;
    private MagazineIssue _magazineIssue;

    [SetUp]
    public void Setup()
    {
        _repository = new MagazineRepository();

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
            magazines: null
        );

        _magazine = new Magazine(
            issn: "1234",
            title: "Title",
            publisherId: _publisher,
            publisherDate: new DateTime(1978, 1, 1),
            endOfPublish: DateTime.Now
        );

        _magazine2 = new Magazine(
            issn: "12345",
            title: "Title2",
            publisherId: _publisher,
            publisherDate: new DateTime(1978, 1, 1),
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

        _publisher.Magazines?.Add(_magazine);
    }

    [Test]
    public void Create_ValidMagazine_Added()
    {
        //Arrange
        //Act
        _repository.Create(_magazine);
        //Assert
        Assert.That(_repository._database.Count, Is.EqualTo(1));
        Assert.That(_repository._database, Is.Not.Null);
    }

    [Test]
    public void Update_ValidMagazine_Updated()
    {
        //Arrange
        _repository.Create(_magazine);
        _magazine.Title = "Updated Title";
        _magazine.Issn = "12345678";
        //Act
        _repository.Update(_magazine);
        var result = _repository._database.First(b => b.Id == _magazine.Id);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._database.Count, Is.EqualTo(1));
                Assert.That(_repository._database, Is.Not.Null);
                Assert.That(result.Issn, Is.EqualTo("12345678"));
                Assert.That(result.Title, Is.EqualTo("Updated Title"));
            }
        );
    }

    [Test]
    public void Update_NotUpdatedMagazine_NotUpdated()
    {
        //Arrange
        _repository.Create(_magazine);

        //Act
        _repository.Update(_magazine);
        var result = _repository._database.First(b => b.Id == _magazine.Id);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._database.Count, Is.EqualTo(1));
                Assert.That(_repository._database, Is.Not.Null);
                Assert.That(result.Issn, Is.Not.EqualTo("12345678"));
                Assert.That(result.Title, Is.Not.EqualTo("Updated Title"));
            }
        );
    }

    [Test]
    public void Delete_CreatedMagazine_Deleted()
    {
        //Arrange
        _repository.Create(_magazine);
        //Act
        _repository.Delete(_magazine);
        //Assert
        Assert.Multiple(() =>
            {
                Assert.That(_repository._database.Count, Is.EqualTo(0));
                Assert.That(_repository._database.Count(b => b.Id == _magazine.Id), Is.EqualTo(0));
                Assert.True(_repository._database.Count == 0);
            }
        );
    }

    [Test]
    public void Delete_NotCreatedMagazine_ThrowsNotFoundException()
    {
        //Arrange
        //Act
        TestDelegate act = ()=> _repository.Delete(_magazine);

        //Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public void Exist_CreatedMagazine_Exists()
    {
        //Arrange
        _repository.Create(_magazine);
        //Act
        var result = _repository.Exists(_magazine);
        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Exist_NoMagazine_NoExists()
    {
        //Arrange
        //Act
        var result = _repository.Exists(_magazine);
        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetByIdAsync_CreatedMagazine_Retrieved()
    {
        //Arrange
        _repository.Create(_magazine);
        //Act
        var result = await _repository.GetByIdAsync(_magazine.Id);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_magazine));
        });
    }

    [Test]
    public async Task GetByIdAsync_NoCreatedMagazine_NoRetrieved()
    {
        //Arrange
        //Act
        var result = await _repository.GetByIdAsync(_magazine.Id);
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Null);
            Assert.That(result, Is.Not.EqualTo(_magazine));
        });
    }

    [Test]
    public async Task GetAllAsync_CreatedOneMagazine_Retrieved()
    {
        //Arrange
        _repository.Create(_magazine);
        //Act
        var result = await _repository.GetAllAsync();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task GetAllAsync_CreatedMultipleMagazine_Retrieved()
    {
        //Arrange
        _repository.Create(_magazine);
        _repository.Create(_magazine2);
        //Act
        var result = await _repository.GetAllAsync();
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task GetMagazineAsync_ValidIssueIdCreatedMagazine_Retrieved()
    {
        // Arrange
        _magazine.MagazineIssues = new List<MagazineIssue> { _magazineIssue };
        _repository.Create(_magazine);

        // Act
        var result = await _repository.GetMagazineAsync(_magazineIssue.Id);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.MagazineIssues.Any(i => i.Id == _magazineIssue.Id), Is.True);
        });
    }

    [Test]
    public async Task GetMagazineAsync_InValidIssueIdCreatedMagazine_ThrowsNotFoundException()
    {
        // Arrange
        _magazine.MagazineIssues = new List<MagazineIssue> { _magazineIssue };
        _repository.Create(_magazine);
        var fakeId = Guid.NewGuid();

        // Act
        TestDelegate act = () => _repository.GetMagazineAsync(fakeId);

        // Assert
        Assert.Throws<NotFoundException>(act);
    }

    [Test]
    public async Task GetMagazineAsync_ValidIssueIdNOtMagazine_ThrowsNotFoundException()
    {
        // Arrange
        _magazine.MagazineIssues = new List<MagazineIssue> { _magazineIssue };

        // Act
        TestDelegate act = () => _repository.GetMagazineAsync(_magazineIssue.Id);

        // Assert
        Assert.Throws<NotFoundException>(act);
        
    }
    
    [Test]
    public async Task GetAllAsync_ValidPublisherId_Retrieved()
    {
        //Arrange
        _repository.Create(_magazine);
        //Act
        var result = await _repository.GetAllAsync(_publisher.Id);
        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetAllAsync_InvalidPublisherId_NotRetrieved()
    {
        //Arrange
        _repository.Create(_magazine);
        var fakeId = Guid.NewGuid();
        //Act
        var result = await _repository.GetAllAsync(fakeId);
        //Assert
        Assert.That(result, Is.Empty);
        Assert.That(result.Count, Is.EqualTo(0));
    }
    
    [Test]
    public async Task GetAllAsync_ValidPublisherIdNotCreatedMagazine_NotRetrieved()
    {
        //Arrange
        //Act
        var result = await _repository.GetAllAsync(_publisher.Id);
        //Assert
        Assert.That(result, Is.Empty);
        
    }
}