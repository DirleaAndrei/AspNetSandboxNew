using System;
using System.Collections.Generic;
using AspNetSandbox.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSandbox
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return booksService.GetAllBooks();
        }

        /// <summary>Gets the specified book by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Book object.
        /// </returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(booksService.GetBookById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public void Post([FromBody] Book book)
        {
            booksService.AddNewBook(book);
        }

        /// <summary>Updated book at specific id.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedBook">The updated book.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Book updatedBook)
        {
            try
            {
                booksService.UpdateBookById(id, updatedBook);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            booksService.DeleteBookById(id);
        }
    }
}
