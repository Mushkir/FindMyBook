using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyBook.ViewModels
{
    public class CartViewModel
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public double Price { get; set; }

        public string BookImage { get; set; }

        public string AuthorName { get; set; }
    }
}