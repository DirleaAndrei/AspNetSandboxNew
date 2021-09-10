using System;
using System.Threading.Tasks;
using AspNetSandbox.Data;
using AspNetSandbox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetSandbox
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBooksService booksService;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Books.ToListAsync());
        }

        /// <summary>Gets the specified book by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Book object.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
                return Ok(book);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>Updated book at specific id.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedBook">The updated book.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book updatedBook)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Books.FindAsync(id);

            if (bookToUpdate != null)
            {
                bookToUpdate.BookTitle = updatedBook.BookTitle;
                bookToUpdate.BookAuthor = updatedBook.BookAuthor;
                bookToUpdate.BookLanguage = updatedBook.BookLanguage;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
