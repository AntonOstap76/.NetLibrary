using DomainProject;
using NUnit.Framework;
using PatentRepositoryProject;
using PatentServiceProject;

namespace IntegrationTests;

public class PatentServiceIntegrationTests
{
    private PatentRepository _repository;
    private PatentService _service;
    private Patent _patent;
    private Author _author;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new PatentRepository();
        _service = new PatentService(_repository);

        _country = new Country(
            name:     "Poland",
            code:     "32444",
            language: "Polish"
        );

        _patent = new Patent(
            title:       "Valid Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "1234567890",
            publishDate: new DateTime(2020, 1, 1),
            authors:     new List<Author>()
        );

        _author = new Author(
            name:         "John",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1990, 5, 20),
            entities:     null,
            patentId:     _patent
        );

        _patent.Authors.Add(_author);
    }

    [Test]
    public async Task GetAsync_ByAuthor_ExistingAuthor_ReturnsPatents()
    {
        //Arrange
        _service.Create(_patent);

        //Act
        var result = await _service.GetAsync(_author);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_patent.Id));
    }

    [Test]
    public async Task GetAsync_ByAuthor_NonExistingAuthor_ReturnsNull()
    {
        //Arrange
        _service.Create(_patent);

        var otherAuthor = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     null,
            patentId:     _patent
        );

        //Act
        var result = await _service.GetAsync(otherAuthor);

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAsync_ByAuthor_MultiplePatents_ReturnsAllForAuthor()
    {
        //Arrange
        var secondPatent = new Patent(
            title:       "Second Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "0987654321",
            publishDate: new DateTime(2021, 1, 1),
            authors:     new List<Author> { _author }
        );

        _service.Create(_patent);
        _service.Create(secondPatent);

        //Act
        var result = await _service.GetAsync(_author);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetAsync_ByAuthor_NullAuthor_ThrowsArgumentNullException()
    {
        //Act + Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetAsync((Author)null!));
    }

    [Test]
    public async Task GetAsync_ByAuthors_ExistingAuthors_ReturnsPatents()
    {
        //Arrange
        _service.Create(_patent);
        var authors = new List<Author> { _author };

        //Act
        var result = await _service.GetAsync(authors);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_patent.Id));
    }

    [Test]
    public async Task GetAsync_ByAuthors_NoMatchingAuthors_ReturnsEmptyList()
    {
        //Arrange
        _service.Create(_patent);

        var otherAuthor = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     null,
            patentId:     _patent
        );

        //Act
        var result = await _service.GetAsync(new List<Author> { otherAuthor });

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAsync_ByAuthors_PatentFromDifferentAuthor_NotReturned()
    {
        //Arrange
        var otherAuthor = new Author(
            name:         "Jane",
            lastName:     "Johnson",
            country:      _country,
            birthdayYear: new DateTime(1995, 1, 1),
            entities:     null,
            patentId:     _patent
        );

        var otherPatent = new Patent(
            title:       "Other Patent Title",
            content:     "This is a valid content that is definitely longer than fifty characters long.",
            patentCode:  "0987654321",
            publishDate: new DateTime(2021, 1, 1),
            authors:     new List<Author> { otherAuthor }
        );

        _service.Create(_patent);
        _service.Create(otherPatent);

        //Act
        var result = await _service.GetAsync(new List<Author> { _author });

        //Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_patent.Id));
    }

    [Test]
    public void GetAsync_ByAuthors_NullAuthors_ThrowsArgumentNullException()
    {
        //Act + Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetAsync((IEnumerable<Author>)null!));
    }

    [Test]
    public void GetAsync_ByAuthors_EmptyList_ThrowsArgumentNullException()
    {
        //Act + Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetAsync((IEnumerable<Author>)new List<Author>()));
    }
}