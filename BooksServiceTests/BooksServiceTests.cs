using AspNetSandbox;
using Xunit;

namespace BooksServiceTests
{
    public class BooksServiceTests
    {
        [Fact]
        public void BooksServiceIdTests()
        {
            // Asume
            var booksService = new BooksService();

            // Act
            booksService.AddNewBook(new Book {
                BookTitle = "Scurta istorie a betiei Test1",
                BookAuthor = "Mark Forsyth Test1",
                BookLanguage = "Romanian"
            });
            booksService.DeleteBookById(2);
            booksService.AddNewBook(new Book
            {
                BookTitle = "Scurta istorie a betiei Test2",
                BookAuthor = "Mark Forsyth Test2",
                BookLanguage = "Romanian"
            });


            // Assert
            Assert.Equal("Scurta istorie a betiei Test2", booksService.GetBookById(3).BookTitle);
        }

        [Fact]
        public void BooksServiceUpdateBookTests()
        {
            // Asume
            var booksService = new BooksService();

            // Act
            booksService.AddNewBook(new Book
            {
                BookTitle = "Scurta istorie a betiei Test1",
                BookAuthor = "Mark Forsyth Test1",
                BookLanguage = "Romanian"
            });
            booksService.UpdateBookById(4, new Book
            {
                BookTitle = "Scurta istorie a betiei Edited",
                BookAuthor = "Mark Forsyth Edited",
                BookLanguage = "Romanian"
            });


            // Assert
            Assert.Equal("Scurta istorie a betiei Edited", booksService.GetBookById(4).BookTitle);
        }
    }
}
