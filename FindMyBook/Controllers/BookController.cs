using FindMyBook.Models;
using FindMyBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace FindMyBook.Controllers
{
    public class BookController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();

        //private void FetchBookDetails()
        //{
        //    var bookDetails = (from book in db.table_book_detail
        //                       join author in db.table_author on book.author_id_FK equals author.author_id
        //                       join publisher in db.table_publisher on book.publisher_id_FK equals publisher.publisher_id
        //                       join status in db.table_book_status on book.book_status_id_FK equals status.book_status_id
        //                       select new BookViewModel
        //                       {
        //                           BookId = book.book_id,
        //                           Title = book.book_name,
        //                           ISBN = book.book_isbn_number,
        //                           BookLanguage = book.book_language,
        //                           Price = book.book_price,
        //                           Pages = book.pages,
        //                           PublishedDate = book.book_published_date,
        //                           Rating = book.rating,
        //                           AuthorName = author.author_name,
        //                           PublisherName = publisher.publisher_name,
        //                           Status = status.book_status,
        //                           BookImage = book.book_image // Include book image in the view model
        //                       }).ToList();
        //}

        // GET: Book
        public ActionResult Index()
        {
            var bookDetails = (from book in db.table_book_detail
                               join author in db.table_author on book.author_id_FK equals author.author_id
                               join publisher in db.table_publisher on book.publisher_id_FK equals publisher.publisher_id
                               join status in db.table_book_status on book.book_status_id_FK equals status.book_status_id
                               select new BookViewModel
                               {
                                   BookId = book.book_id,
                                   Title = book.book_name,
                                   ISBN = book.book_isbn_number,
                                   BookLanguage = book.book_language,
                                   Price = book.book_price,
                                   Pages = book.pages,
                                   PublishedDate = book.book_published_date,
                                   Rating = book.rating,
                                   AuthorName = author.author_name,
                                   PublisherName = publisher.publisher_name,
                                   Status = status.book_status,
                                   BookImage = book.book_image // Include book image in the view model
                               }).ToList();

            return View(bookDetails);
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
        public ActionResult AddBook(table_book_detail table_Book_Detail)
        {
            try
            {
                if (db.table_book_detail.Any(bookDetail => bookDetail.book_isbn_number == table_Book_Detail.book_isbn_number))
                {
                    return Json(new { status = "409", message = "The ISBN number already exist!" });
                    //return View();

                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(table_Book_Detail.BookImageFile.FileName);
                    string extension = Path.GetExtension(table_Book_Detail.BookImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    table_Book_Detail.book_image = "../BookImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("../BookImages/"), fileName);
                    table_Book_Detail.BookImageFile.SaveAs(fileName);


                    db.table_book_detail.Add(table_Book_Detail);
                    db.SaveChanges();

                    ModelState.Clear();

                    return RedirectToAction("AddBook", "Book");
                    //return Json(new { status = "200", message = "The book detail has been saved successfully." });

                }
            }
            catch (Exception ex)
            {
                ViewBag.Notification = "An error occurred: " + ex.Message;


                //return View(table_Book_Detail);
            }

            ViewBag.Authors = new SelectList(db.table_author.ToList(), "author_id", "author_name", table_Book_Detail.author_id_FK);
            ViewBag.Publishers = new SelectList(db.table_publisher.ToList(), "publisher_id", "publisher_name", table_Book_Detail.publisher_id_FK);
            ViewBag.BookStatus = new SelectList(db.table_book_status.ToList(), "book_status_id", "book_status", table_Book_Detail.book_status_id_FK);

            return View(table_Book_Detail);
        }


        // Find Books
        public ActionResult FindBooks()
        {
                var bookDetails = (from book in db.table_book_detail
                                   join author in db.table_author on book.author_id_FK equals author.author_id
                                   join publisher in db.table_publisher on book.publisher_id_FK equals publisher.publisher_id
                                   join status in db.table_book_status on book.book_status_id_FK equals status.book_status_id
                                   select new BookViewModel
                                   {
                                       BookId = book.book_id,
                                       Title = book.book_name,
                                       ISBN = book.book_isbn_number,
                                       BookLanguage = book.book_language,
                                       Price = book.book_price,
                                       Pages = book.pages,
                                       PublishedDate = book.book_published_date,
                                       Rating = book.rating,
                                       AuthorName = author.author_name,
                                       PublisherName = publisher.publisher_name,
                                       Status = status.book_status,
                                       BookImage = book.book_image // Include book image in the view model
                                   }).ToList();
            return View(bookDetails);
        }




    }
}