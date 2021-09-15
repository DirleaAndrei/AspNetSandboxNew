using System;
using System.Threading.Tasks;
using AspNetSandbox.DTOs;
using AspNetSandbox.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSandbox
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;

        public BooksController(IBookRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetAllBooks());
        }

        /// <summary>Gets the specified book by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Book object.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = repository.GetBookById(id);
            if (book != null)
            {
                return Ok(book);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateBookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                Book book = mapper.Map<Book>(bookDto);
                repository.AddNewBook(book);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>Updated book at specific id.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bookDto">The updated book.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateBookDto bookDto)
        {
            Book updatedBook = mapper.Map<Book>(bookDto);
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
