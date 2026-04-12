using DomainProject;
using MagazineIssueRepositoryProject;
using MagazineIssueServiceProject;
using NUnit.Framework;

namespace IntegrationTests;

public class MagazineIssueServiceIntegrationTests
{
    private MagazineIssueRepository _repository;
    private MagazineIssueService _service;
    private MagazineIssue _magazineIssue;
    private Magazine _magazine;
    private Publisher _publisher;
    private Country _country;

    [SetUp]
    public void Setup()
    {
        _repository = new MagazineIssueRepository();
        _service = new MagazineIssueService(_repository);

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

        _magazineIssue = new MagazineIssue(
            number:        1,
            title:         "Valid Title",
            content:       "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn:  "12345678",
            magazine:      _magazine
        );
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_ExistingMagazineId_ReturnsIssues()
    {
        //Arrange
        _service.Create(_magazineIssue);

        //Act
        var result = await _service.GetAllByMagazineIdAsync(_magazine.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_magazineIssue.Id));
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_NonExistingMagazineId_ReturnsEmptyList()
    {
        //Arrange
        _service.Create(_magazineIssue);

        //Act
        var result = await _service.GetAllByMagazineIdAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_MultipleIssues_ReturnsAllForMagazine()
    {
        //Arrange
        var secondIssue = new MagazineIssue(
            number:        2,
            title:         "Second Valid Title",
            content:       "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 2, 1),
            magazineIssn:  "12345678",
            magazine:      _magazine
        );

        _service.Create(_magazineIssue);
        _service.Create(secondIssue);

        //Act
        var result = await _service.GetAllByMagazineIdAsync(_magazine.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllByMagazineIdAsync_IssueFromDifferentMagazine_NotReturned()
    {
        //Arrange
        var otherMagazine = new Magazine(
            issn:          "87654321",
            title:         "Other Magazine Title",
            publisherId:   _publisher,
            publisherDate: new DateTime(2000, 1, 1),
            endOfPublish:  new DateTime(2020, 1, 1)
        );

        var otherIssue = new MagazineIssue(
            number:        1,
            title:         "Other Issue Title",
            content:       "This is a valid content that is definitely longer than fifty characters long.",
            publisherDate: new DateTime(2020, 1, 1),
            magazineIssn:  "87654321",
            magazine:      otherMagazine
        );

        _service.Create(_magazineIssue);
        _service.Create(otherIssue);

        //Act
        var result = await _service.GetAllByMagazineIdAsync(_magazine.Id);

        //Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0]!.Id, Is.EqualTo(_magazineIssue.Id));
    }
}