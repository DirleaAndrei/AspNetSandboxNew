﻿using AspNetSandbox.Data;
using AspNetSandbox.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetSandbox.Services
{
    public class DbBooksRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public DbBooksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddNewBook(Book book)
        {

                _context.Add(book);
                _context.SaveChangesAsync();
        }

        public void DeleteBookById(int id)
        {
            throw new NotImplementedException();
            _context.Books.Remove(GetBookById(id));
            _context.SaveChangesAsync();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            var book = _context.Books
            .FirstOrDefaultAsync(m => m.BookId == id);
            return book;
        }

        public void UpdateBookById(int id, Book updatedBook)
        {

            var bookToUpdate = _context.Books.FindAsync(id);
                bookToUpdate.BookTitle = updatedBook.BookTitle;
                bookToUpdate.BookAuthor = updatedBook.BookAuthor;
                bookToUpdate.BookLanguage = updatedBook.BookLanguage;
        }
    }
}
