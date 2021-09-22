using System.Collections.Generic;
using System.Linq;
using AspNetSandbox.Data;
using AspNetSandbox.Interfaces;
using AspNetSandbox.Models;

namespace AspNetSandbox.Services
{
    public class DbBooksRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public DbBooksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void AddNewBook(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public void DeleteBookById(int id)
        {
            _context.Books.Remove(GetBookById(id));
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        /// <inheritdoc/>
        public Book GetBookById(int id)
        {
            var book = _context.Books
            .FirstOrDefault(m => m.BookId == id);
            return book;
        }

        /// <inheritdoc/>
        public void UpdateBookById(int id, Book updatedBook)
        {
            var selectedBook = GetBookById(id);
            selectedBook.BookTitle = updatedBook.BookTitle;
            selectedBook.BookAuthor = updatedBook.BookAuthor;
            selectedBook.BookLanguage = updatedBook.BookLanguage;
            _context.Update(selectedBook);
            _context.SaveChanges();
        }
    }
}
