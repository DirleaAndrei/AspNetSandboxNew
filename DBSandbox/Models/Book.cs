﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBSandbox.Models
{
    public class Book
    {
        public int BookId { get; set; }

        public string BookTitle { get; set; }

        public string BookAuthor { get; set; }

        public string BookLanguage { get; set; }
    }
}
