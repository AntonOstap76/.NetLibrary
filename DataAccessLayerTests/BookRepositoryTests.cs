using BookRepositoryProject;
using DomainProject;

namespace DataAccessLayerTests;

public class BookRepositoryTests
{
    private Book _book;
    private Author _author;
    private Publisher _publisher;
    private BookRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new BookRepository();

        var country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _publisher= new Publisher(
            name: "Test Publisher",
            country: country,
            foundedYear: new DateTime(2000, 1, 1),
            entities: new List<CommonEntity>(){_book},
            magazines: null
        );
        _author = new Author(
            name: "Bob",
            country: country,
            lastName: "Bobowicz",
            birthdayYear: new DateTime(1997, 1,1),
            patentId: null,
            entities: new List<CommonEntity>(){_book}
        );

        _book = new Book(
            title: "Test Title",
            content: "Test Content",
            isbn: "123456789123",
            authorId: new List<Author>(){_author},
            publisherId: _publisher
        );
    }

    [Test]
    public async Task GetAllByAuthor_ProvideValidAuthorID_GetBook()
    {
        //Arrange
        _repository.Create(_book);
        
        //Act
        var books = await _repository.GetAllByAuthorAsync(_author.Id);
        
        //Assert
        Assert.IsNotNull(books);
        Assert.True(books.Any());
    }
    
    [Test]
    public async Task GetAllByAuthor_ProvideNotValidAuthorID_DontGetBook()
    {
       //Arrange
       _repository.Create(_book);
       var fakeId = new Guid();
       
       //Act
       var books = await _repository.GetAllByAuthorAsync(fakeId);
       
       //Assert
       Assert.IsNull(books);
    }

    [Test]
    public async Task GetAllByPublisher_ProvideValidPublisherID_GetBook()
    {
        //Arrange
        _repository.Create(_book);
        
        //Act
        var books = await _repository.GetAllByPublisherAsync(_publisher.Id);
        
        //Assert
        Assert.That(books, Is.Not.Null);
        Assert.That(books.Any(), Is.True);
    }
    
    [Test]
    public async Task GetAllByPublisher_ProvideInvalidPublisherID_DontGetBook()
    {
        //Arrange
        _repository.Create(_book);
        var fakeId = new Guid();
        
        //Act
        var books = await _repository.GetAllByPublisherAsync(fakeId);
        
        //Assert
        Assert.That(books, Is.Null);
    }
}