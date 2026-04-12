using DomainProject;
using PatentRepositoryProject;

namespace DataAccessLayerTests;

public class PatentRepositoryTests
{
    private Author _author;
    private Patent _patent;
    private Author _author2;
    private Author _author3;
    private PatentRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new PatentRepository();

        var country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );
        _author = new Author(
            name: "Bob",
            country: country,
            lastName: "Bobowicz",
            birthdayYear: new DateTime(1997, 1, 1),
            patentId: null,
            entities: new List<CommonEntity>() { _patent }
        );
        _author2 = new Author(
            name: "Bob",
            country: country,
            lastName: "Bobowicz",
            birthdayYear: new DateTime(1997, 1, 1),
            patentId: null,
            entities: new List<CommonEntity>() { _patent }
        );
        _author3 = new Author(
            name: "Bob",
            country: country,
            lastName: "Bobowicz",
            birthdayYear: new DateTime(1997, 1, 1),
            patentId: null,
            entities: new List<CommonEntity>()
        );

        _patent = new Patent(
            title: "Title",
            content: "Content",
            patentCode: "1234561234",
            publishDate: new DateTime(2005, 3, 4),
            authors: [_author, _author2]
        );
        _author.PatentId = _patent;
        _author2.PatentId = _patent;
    }

    [Test]
    public async Task GetAsync_GiveValidAuthorID_GetPatent()
    {
        //Arrange
        _repository.Create(_patent);

        //Act
        var result = await _repository.GetAsync(_author);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetAsync_InValidAuthorID_NullPatent()
    {
        //Arrange
        _repository.Create(_patent);

        //Act
        var result = await _repository.GetAsync(_author3);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAsync_GiveValidListAuthors_GetPatent()
    {
        //Arrange 
        _repository.Create(_patent);

        //Act
        var result = await _repository.GetAsync(new List<Author> { _author, _author2 });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetAsync_GiveValidListAuthorsWithOneInvalid_GetPatent()
    {
        //Arrange 
        _repository.Create(_patent);

        //Act
        var result = await _repository.GetAsync(new List<Author> { _author, _author2, _author3 });

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
    }
    
    [Test]
    public async Task GetAsync_ListWithOneInvalidAuthor_ReturnsEmptyList()
    {
        //Arrange 
        _repository.Create(_patent);

        //Act
        var result = await _repository.GetAsync(new List<Author> {_author3});

        //Assert
        Assert.That(result, Is.Empty);
        
    }
}