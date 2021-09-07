using System.Collections.Generic;

namespace AspNetSandbox
{
    public interface IBooksService
    {
        void DeleteBookById(int id);
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddNewBook(Book book);
        void UpdateBookById(int id, Book updatedBook);
    }
}