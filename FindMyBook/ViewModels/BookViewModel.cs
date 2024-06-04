using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyBook.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public string Status { get; set; }
        public string BookImage { get; set; }
        public string ISBN { get; set; }
        public double Price { get; set; }
        public string BookLanguage { get; set; }
        public string Rating { get; set; }
        public int Pages { get; set; }
        public System.DateTime PublishedDate { get; set; }
    }
}