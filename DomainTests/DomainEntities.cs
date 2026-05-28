using System.Reflection;
using DomainProject;

namespace DomainTests;

public class Tests
{
   [Test]
   public void ToString_BookEntity_Correct()
   {
      //Arrange
      Book book = new Book(
         title: "Testik",
         content: "Content",
         isbn: "123456789",
         authorId: new List<Author>(),
         publisherId: null
      );
      
      //Act
      var result = book.ToString();
      
      //Assert
      Assert.That(result, Is.EqualTo($"Book called Testik with code 123456789 and with content Content"));
   }

   [Test]
   public void ToString_PatentEntity_Correct()
   {
      //Arrange
      Patent patent = new Patent(
         title: "Patent",
         content: "Content",
         patentCode: "1234567890",
         publishDate: new DateTime(1920, 1, 1),
         authors: new List<Author>()
      );
      
      //Act
      var result = patent.ToString();
      
      //Assert
      Assert.That(result, Is.EqualTo($"Patent Patent with code 1234567890 that was published 1/1/1920 12:00:00 AM " +
                                     $"and have 0 authors with content:Content"));
   }

   [Test]
   public void ToString_MagazineIssue_Correct()
   {
      //Arrange
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

      var magazine = new Magazine(
         issn: "12345678",
         title: "Test Magazine",
         publisherId: publisher,
         publisherDate: new DateTime(2000, 1, 1),
         endOfPublish: new DateTime(2020, 1, 1)
      );

      var issue = new MagazineIssue(
         number: 1,
         title: "Title",
         content: "Content",
         publisherDate: new DateTime(1920, 1, 1),
         magazineIssn: "1234567789",
         magazine: magazine
      );
      
      //Act
      var result = issue.ToString();
      
      //Assert
      Assert.That(result, Is.EqualTo("Magazine called Title with code 1234567789 which is " +
                                     "a 1 from Test Magazinethat started publishing 1/1/1920 12:00:00 AM with content Content"));
   }

   [Test]
   public void ToString_Magazine_Correct()
   {
      // Arrange
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

      var magazine = new Magazine(
         issn: "12345678",
         title: "Test Magazine",
         publisherId: publisher,
         publisherDate: new DateTime(2000, 1, 1),
         endOfPublish: new DateTime(2020, 1, 1)
      );
      
      //Act
      var result = magazine.ToString();
      
      //Assert
      Assert.That(result, Is.EqualTo($"Magazine called {magazine.Title} with code {magazine.Issn} that start published " +
                                     $"{magazine.PublisherDate} and finished {magazine.EndOfPublish} " +
                                     $"and having this much magazines {magazine.MagazineIssues?.Count} " +
                                     $"and who's publisher is {magazine.PublisherId.Id}"));
   }

   [Test]
   public void ToString_Author_Correct()
   {
      //Arrange
      var country = new Country(
         name: "Poland",
         code: "32444",
         language: "Polish"
      );

      Author author = new Author(
         name: "Bob",
         lastName: "Beb",
         country: country,
         birthdayYear: new DateTime(2000, 1, 1),
         patentId: null,
         entities: new List<CommonEntity>()
      );
      
      //Act
      var result = author.ToString();
      //Assert
      Assert.That(result, Is.EqualTo($"Author {author.Name} {author.LastName} from {country.Name} born in {author.BirthdayYear} having this much works 0"));
   }

   [Test]
   public void ToString_Publisher_Correct()
   {
      //Arrange
      var country = new Country(
         name: "Poland",
         code: "32444",
         language: "Polish"
      );
      
      Publisher publisher = new Publisher(
         name:"Ben",
         country:country, 
         foundedYear: new DateTime(2000, 1, 1),
         entities: new List<CommonEntity>(),
         magazines: null
         );
      //Act
      var result = publisher.ToString();
      //Assert
      Assert.That(result, Is.EqualTo($"Publisher {publisher.Name} from {country.Name} FoundedYear {publisher.FoundedYear}" +
                                     $" with 0 works published"));
   }
   
   
}