using CommonRepositoryProject;
using DomainProject;

namespace DataAccessLayerTests;

public class CommonEntityRepositoryTest
{
    private Book _book;
    private CommonEntityRepository<Book> _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new CommonEntityRepository<Book>();

        var country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        var publisher = new Publisher(
            name: "Test Publisher",
            country: country,
            foundedYear: new DateTime(2000, 1, 1),
            entities: null,
            magazines: null
        );

        _book = new Book(
            title: "Test Title",
            content: "Test Content",
            isbn: "123456789123",
            authorId: new List<Author>(),
            publisherId: publisher
        );
    }
    
    [Test]
    public void BookEntityCreateMethod_FullData_AddEntityToDB()
    {
        //Act
        _repository.Create(_book);

        //Assert
        Assert.True(_repository._database.Contains(_book));
    }

    [Test]
    public void BookEntityUpdateMethod_FullData_UpdateEntity()
    {
        // Arrange
        _repository.Create(_book);
        _book.Title = "Updated Title";
        _book.Content = "Updated Content";

        // Act
        _repository.Update(_book);

        // Assert
        var result = _repository._database.First(b => b.Id == _book.Id);
        Assert.That(result.Title, Is.EqualTo("Updated Title"));
        Assert.That(result.Content, Is.EqualTo("Updated Content"));
    }

    [Test]
    public void BookEntityDelete_FullData_DeleteEntity()
    {
        //Assert
        _repository.Create(_book);

        //Act
        _repository.Delete(_book);

        //Assert
        Assert.False(_repository._database.Contains(_book));
        Assert.That(_repository._database.Count(b => b.Id == _book.Id), Is.EqualTo(0));
        Assert.True(_repository._database.Count == 0);
    }

    [Test]
    public void BookEntityUpdateMethod_SameData_NothingChanged()
    {
        //Arrange
        _repository.Create(_book);
        //Act
        _repository.Update(_book);
        //Assert
        var result = _repository._database.First(b => b.Id == _book.Id);
        Assert.That(result.Title, Is.EqualTo("Test Title"));
        Assert.That(result.Content, Is.EqualTo("Test Content"));
    }

    [Test]
    public void BookEntityExistMethod_FullDataCreated_ReturnTrue()
    {
        //Assert
        _repository.Create(_book);

        //Act
        var result = _repository.Exists(_book);

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void BookEntityExistMethod_NotCreated_ReturnFalse()
    {
        //Act
        var result = _repository.Exists(_book);

        //Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task BookEntityGetAsync_FullDataCreated_ReturnBook()
    {
        //Assert
        _repository.Create(_book);

        //Act
        var result = await _repository.GetAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task BookEntityGetAsync_NotCreated_ReturnNull()
    {
        //Act
        var result = await _repository.GetAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task BookEntityGetContent_CreatedBook_ReturnContent()
    {
        //Arrange
        _repository.Create(_book);

        //Act
        var result = await _repository.GetContentAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Equals("Test Content"));
    }

    [Test]
    public async Task BookEntityGetContent_NotCreated_ReturnNull()
    {
        //Arrange
        var fake = Guid.NewGuid();
        
        //Act
        var result = await _repository.GetContentAsync(fake);

        //Assert
        Assert.That(result, Is.Null);
    }
}