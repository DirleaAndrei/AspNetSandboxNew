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
        private static List<Book> books;

        static BooksController()
        {
            books = new List<Book>();

            books.Add(new Book
            {
                BookId = 0,
                BookTitle = "Arta manipularii",
                BookLanguage = "Romanian",
                BookAuthor = "Kevin Dutton"
            });


            books.Add(new Book
            {
                BookId = 1,
                BookTitle = "Puterea prezentului",
                BookLanguage = "Romanian",
                BookAuthor = "Eckhart Tolle"
            });
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return books;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return books.Single(book => book.BookId ==id);
        }
        

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            int id = books.Count+1;
            book.BookId = id;
            books.Add(book);
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
            books.Remove(Get(id));
        }

    }
}
