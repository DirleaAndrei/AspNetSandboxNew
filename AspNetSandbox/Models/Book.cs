using System.Diagnostics;

namespace AspNetSandbox.Models
{
    public class Book
    {
        [DebuggerDisplay("Title {BookTilte}, Id {BookId}")]
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string BookLanguage { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}
