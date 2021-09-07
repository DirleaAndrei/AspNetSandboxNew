using System.Collections.Generic;
using System.Linq;


namespace AspNetSandbox
{

    public class BooksService : IBooksService
    {
        private List<Book> books;

        public BooksService()
        {
            books = new List<Book>();

            books.Add(new Book
            {
                BookId = 0,
                BookTitle = "Arta manipularii",
                BookAuthor = "Kevin Dutton",
                BookLanguage = "Romanian"
            });

            books.Add(new Book
            {
                BookId = 1,
                BookTitle = "Puterea prezentului",
                BookAuthor = "Eckhart Tolle",
                BookLanguage = "Romanian"
            });
        }

        public IEnumerable<Book> Get()
        {
            return books;
        }

        public Book Get(int id)
        {
            return books.Single(book => book.BookId == id);
        }

        public void Post(Book book)
        {
            int id = books.Count;
            book.BookId = id;
            books.Add(book);
        }

        public void Put(int id, string value)
        {

        }

        public void Delete(int id)
        {
            books.Remove(Get(id));
        }


    }
}
