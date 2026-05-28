using BookRepositoryProject;
using BookServiceProject;
using DomainProject;
using NUnit.Framework;

namespace IntegrationTests;


public class CommonEntityServiceIntegrationTests
{
    private BookRepository _repository;
    private BookService _service;
    private Book _book;
    private Publisher _publisher;
    private Country _country;
    private Author _author;
    private Patent _patent;

    [SetUp]
    public void Setup()
    {
        _repository = new BookRepository();
        _service = new BookService(_repository);

        _country = new Country(
            name: "Poland",
            code: "32444",
            language: "Polish"
        );

        _publisher = new Publisher(
            name: "Ban",
            country: _country,
            foundedYear: new DateTime(1999, 1, 1),
            entities: null,
            magazines: null
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

        _book = new Book(
            title: "Valid Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            isbn: "1234567890123",
            authorId: new List<Author> { _author },
            publisherId: _publisher
        );
    }

    [Test]
    public void Create_ValidBook_ExistsInRepository()
    {
        //Act
        _service.Create(_book);

        //Assert
        Assert.That(_service.Exists(_book), Is.True);
    }

    [Test]
    public void Exists_NonExistingBook_ReturnsFalse()
    {
        //Act + Assert
        Assert.That(_service.Exists(_book), Is.False);
    }

    [Test]
    public async Task GetByIdAsync_ExistingBook_ReturnsBook()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetByIdAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(_book.Id));
    }

    [Test]
    public async Task GetByIdAsync_NonExistingBook_ReturnsNull()
    {
        //Act
        var result = await _service.GetByIdAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetAllAsync_MultipleBooksCreated_ReturnsAll()
    {
        //Arrange
        var secondBook = new Book(
            title: "Second Title",
            content: "This is a valid content that is definitely longer than fifty characters long.",
            isbn: "1234567890124",
            authorId: new List<Author> { _author },
            publisherId: _publisher
        );

        _service.Create(_book);
        _service.Create(secondBook);

        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task GetAllAsync_NoBooks_ReturnsEmptyList()
    {
        //Act
        var result = await _service.GetAllAsync();

        //Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task Update_ExistingBook_ChangesAreReflected()
    {
        //Arrange
        _service.Create(_book);
        var bookToUpdate = await _service.GetByIdAsync(_book.Id);
        bookToUpdate!.Title = "Updated Title";

        //Act
        _service.Update(bookToUpdate);
        var result = await _service.GetByIdAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("Updated Title"));
    }

    [Test]
    public void Update_NonExistingBook_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Update(_book));
    }


    [Test]
    public void Delete_ExistingBook_NoLongerExists()
    {
        //Arrange
        _service.Create(_book);

        //Act
        _service.Delete(_book);

        //Assert
        Assert.That(_service.Exists(_book), Is.False);
    }

    [Test]
    public void Delete_NonExistingBook_ThrowsNotFoundException()
    {
        //Act + Assert
        Assert.Throws<NotFoundException>(() => _service.Delete(_book));
    }

    [Test]
    public async Task GetContentAsync_ExistingBook_ReturnsContent()
    {
        //Arrange
        _service.Create(_book);

        //Act
        var result = await _service.GetContentAsync(_book.Id);

        //Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(_book.Content));
    }

    [Test]
    public async Task GetContentAsync_NonExistingBook_ReturnsNull()
    {
        //Act
        var result = await _service.GetContentAsync(Guid.NewGuid());

        //Assert
        Assert.That(result, Is.Null);
    }
}