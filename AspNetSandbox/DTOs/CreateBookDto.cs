using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetSandbox.DTOs
{
    public class CreateBookDto
    {
        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string BookLanguage { get; set; }
    }
}
