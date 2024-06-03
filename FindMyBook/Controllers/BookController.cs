using FindMyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyBook.Controllers
{
    public class BookController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            ViewBag.Authors = new SelectList(db.table_author.ToList(), "author_id", "author_name");
            ViewBag.Publishers = new SelectList(db.table_publisher.ToList(), "publisher_id", "publisher_name");
            ViewBag.BookStatus = new SelectList(db.table_book_status.ToList(), "book_status_id", "book_status");
            return View();
        }

    }
}