using System.Collections.Generic;
using AspNetSandbox.Models;

namespace AspNetSandbox.Interfaces
{
    /// <summary>
    /// This is the interface for BooksService.
    /// </summary>
    public interface IBookRepository
    {
        void DeleteBookById(int id);

        IEnumerable<Book> GetAllBooks();

        Book GetBookById(int id);

        void AddNewBook(Book book);

        void UpdateBookById(int id, Book updatedBook);
    }
}