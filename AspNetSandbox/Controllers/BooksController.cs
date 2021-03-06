namespace AspNetSandbox
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AspNetSandbox.DTOs;
    using AspNetSandbox.Hubs;
    using AspNetSandbox.Interfaces;
    using AspNetSandbox.Models;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooksController"/> class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="hubContext"></param>
        /// <param name="mapper"></param>
        public BooksController(IBookRepository repository, IHubContext<MessageHub> hubContext, IMapper mapper)
        {
            this.repository = repository;
            this.hubContext = hubContext;
            this.mapper = mapper;
        }

        /// <summary>Gets all books.</summary>
        /// <returns>
        ///   Book object.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = repository.GetAllBooks();
            var readBooksDto = mapper.Map<IEnumerable<ReadBookDto>>(books);

            await hubContext.Clients.All.SendAsync("GetBooks", readBooksDto);
            return Ok(readBooksDto);
        }

        /// <summary>Gets the specified book by id.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Book object.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = repository.GetBookById(id);
            ReadBookDto readBookDto = mapper.Map<ReadBookDto>(book);
            await hubContext.Clients.All.SendAsync("SpecificBook", readBookDto);
            if (book != null)
            {
                return Ok(readBookDto);
            }

            return NotFound();
        }

        /// <summary>Create book.</summary>
        /// <param name="bookDto">Book to create.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                Book book = mapper.Map<Book>(bookDto);
                repository.AddNewBook(book);
                await hubContext.Clients.All.SendAsync("BookCreated", book);
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>Updated book at specific id.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="bookDto">The updated book.</param>
        /// <returns>Action result.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateBookDto bookDto)
        {
            if (repository.GetBookById(id) != null)
            {
                Book updatedBookDto = mapper.Map<Book>(bookDto);
                repository.UpdateBookById(id, updatedBookDto);
                await hubContext.Clients.All.SendAsync("UpdatedBook", repository.GetBookById(id));
                return Ok();
            }

            return NotFound();
        }

        /// <summary>Delete book. </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = repository.GetBookById(id);
            ReadBookDto readBookDto = mapper.Map<ReadBookDto>(book);
            repository.DeleteBookById(id);
            await hubContext.Clients.All.SendAsync("BookDeleted", readBookDto);
            return Ok();
        }
    }
}
