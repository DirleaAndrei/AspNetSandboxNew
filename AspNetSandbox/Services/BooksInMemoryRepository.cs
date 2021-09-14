using System.Collections.Generic;
using System.Linq;
using AspNetSandbox.Models;

namespace AspNetSandbox.Services
{
    public class BooksInMemoryRepository : IBookRepository
    {
        private List<Book> books;
        private static int id = 0;

        public BooksInMemoryRepository()
        {
            books = new List<Book>();

            var book1 = new Book
            {
                BookTitle = "Arta manipularii",
                BookAuthor = "Kevin Dutton",
                BookLanguage = "Romanian",
            };

            var book2 = new Book
            {
                BookTitle = "Puterea prezentului",
                BookAuthor = "Eckhart Tolle",
                BookLanguage = "Romanian",
            };
            AddNewBook(book1);
            AddNewBook(book2);
        }

        public static void ResetId()
        {
            id = 0;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.Single(_ => _.BookId == id);
        }

        public void AddNewBook(Book book)
        {
            int bookId = GetNewId();
            book.BookId = bookId;
            books.Add(book);
        }

        private static int GetNewId()
        {
            return id++;
        }

        public void UpdateBookById(int id, Book updatedBook)
        {
            var bookToBeUpdated = books.Single(_ => _.BookId == id);
            bookToBeUpdated.BookTitle = updatedBook.BookTitle;
            bookToBeUpdated.BookAuthor = updatedBook.BookAuthor;
            bookToBeUpdated.BookLanguage = updatedBook.BookLanguage;
        }

        public void DeleteBookById(int id)
        {
            if (GetBookById(id) != null)
            {
                books.Remove(GetBookById(id));
            }
        }
    }
}
