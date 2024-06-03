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

        [HttpGet]
        public ActionResult AddBook()
        {
            ViewBag.Authors = new SelectList(db.table_author.ToList(), "author_id", "author_name");
            ViewBag.Publishers = new SelectList(db.table_publisher.ToList(), "publisher_id", "publisher_name");
            ViewBag.BookStatus = new SelectList(db.table_book_status.ToList(), "book_status_id", "book_status");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(table_book_detail table_Book_Detail)
        {
            try
            {
                if (db.table_book_detail.Any(bookDetail => bookDetail.book_isbn_number == table_Book_Detail.book_isbn_number))
                {
                    return Json(new { status = "409", message = "The ISBN number already exist!" });

                } else
                {
                    db.table_book_detail.Add(table_Book_Detail);
                    db.SaveChanges();

                    return Json(new { status = "200", message = "The book detail has been saved successfully." });

                }      
            }
            catch (Exception ex)
            {
                ViewBag.Notification = "An error occurred: " + ex.Message;
                ViewBag.Authors = new SelectList(db.table_author.ToList(), "author_id", "author_name", table_Book_Detail.author_id_FK);
                ViewBag.Publishers = new SelectList(db.table_publisher.ToList(), "publisher_id", "publisher_name", table_Book_Detail.publisher_id_FK);
                ViewBag.BookStatus = new SelectList(db.table_book_status.ToList(), "book_status_id", "book_status", table_Book_Detail.book_status_id_FK);

                return View(table_Book_Detail);
            }

            //ViewBag.Authors = new SelectList(db.table_author.ToList(), "author_id", "author_name", table_Book_Detail.author_id_FK);
            //ViewBag.Publishers = new SelectList(db.table_publisher.ToList(), "publisher_id", "publisher_name", table_Book_Detail.publisher_id_FK);
            //ViewBag.BookStatus = new SelectList(db.table_book_status.ToList(), "book_status_id", "book_status", table_Book_Detail.book_status_id_FK);

            //return View(table_Book_Detail);
        }


    }
}