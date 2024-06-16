using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyBook.ViewModels
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public int BookId { get; set; }

        public string BookName { get; set; }

        public double Price { get; set; }

        public string BookImage { get; set; }
    }
}