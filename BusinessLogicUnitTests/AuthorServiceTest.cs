using AuthorRepositoryProject;
using AuthorServiceProject;
using DomainProject;
using Moq;

namespace BusinessLogicTests;

public class AuthorServiceTest
{
    public class AuthorServiceTests
    {
        private Mock<IAuthorRepository> _repositoryMock;
        private AuthorService _service;
        private Author _author;
        private Patent _patent;
        private Country _country;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAuthorRepository>();

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

            _service = new AuthorService(_repositoryMock.Object);
        }

        [Test]
        public async Task GetAllPatentsAsync_ExistingAuthorId_ReturnsPatents()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var patents = new List<Patent?> { _patent };
            _repositoryMock
                .Setup(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(patents);

            //Act
            var result = await _service.GetAllPatentsAsync(authorId);

            //Assert
            Assert.That(result, Is.EqualTo(patents));
            Assert.That(result, Has.Count.EqualTo(1));
            _repositoryMock.Verify(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllPatentsAsync_NoPatents_ReturnsEmptyList()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Patent?>());

            //Act
            var result = await _service.GetAllPatentsAsync(authorId);

            //Assert
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllPatentsAsync_NonExistingAuthorId_ReturnsNull()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Patent?>?)null);

            //Act
            var result = await _service.GetAllPatentsAsync(authorId);

            //Assert
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAllPatentsAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBooksAsync_ExistingAuthorId_ReturnsBooks()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            var books = new List<Book?>();
            _repositoryMock
                .Setup(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(books);

            //Act
            var result = await _service.GetAllBooksAsync(authorId);

            //Assert
            Assert.That(result, Is.EqualTo(books));
            _repositoryMock.Verify(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBooksAsync_NoBooks_ReturnsEmptyList()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Book?>());

            //Act
            var result = await _service.GetAllBooksAsync(authorId);

            //Assert
            Assert.That(result, Is.Empty);
            _repositoryMock.Verify(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBooksAsync_NonExistingAuthorId_ReturnsNull()
        {
            //Arrange
            var authorId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Book?>?)null);

            //Act
            var result = await _service.GetAllBooksAsync(authorId);

            //Assert
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAllBooksAsync(authorId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAuthorAsync_ExistingPatentId_ReturnsAuthor()
        {
            //Arrange
            var patentId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAuthorAsync(patentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_author);

            //Act
            var result = await _service.GetAuthorAsync(patentId);

            //Assert
            Assert.That(result, Is.EqualTo(_author));
            _repositoryMock.Verify(r => r.GetAuthorAsync(patentId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAuthorAsync_NonExistingPatentId_ReturnsNull()
        {
            //Arrange
            var patentId = Guid.NewGuid();
            _repositoryMock
                .Setup(r => r.GetAuthorAsync(patentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Author?)null);

            //Act
            var result = await _service.GetAuthorAsync(patentId);

            //Assert
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetAuthorAsync(patentId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}