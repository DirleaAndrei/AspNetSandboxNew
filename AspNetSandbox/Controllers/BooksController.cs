using System;
using System.Threading.Tasks;
using AspNetSandbox.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSandbox
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;

        public BooksController(IBookRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(repository.GetAllBooks());
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
                var book = repository.GetBookById(id);
                return Ok(book);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            if (ModelState.IsValid)
            {
                repository.AddNewBook(book);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>Updated book at specific id.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updatedBook">The updated book.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book updatedBook)
        {
            repository.UpdateBookById(id, updatedBook);
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            repository.DeleteBookById(id);
            return Ok();
        }
    }
}
