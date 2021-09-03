using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetSandbox
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private Book[] books;

        public BooksController()
        {
            books = new Book[2];

            books[0] = new Book();
            books[0].BookID = 0;
            books[0].BookTitle = "Arta manipularii";
            books[0].BookLanguage = "Romanian";
            books[0].BookAuthor = "Kevin Dutton";

            books[1] = new Book();
            books[1].BookID = 1;
            books[1].BookTitle = "Puterea prezentului";
            books[1].BookLanguage = "Romanian";
            books[1].BookAuthor = "Eckhart Tolle";
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return books;
        }


        private bool GetBookByID(Book book)
        {
            return book.BookID == 1;
        }
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            //GetBookByID(books[0], id);
            return books.Single(GetBookByID);
        }
        

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
