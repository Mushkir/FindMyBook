using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindMyBook.Models;
using FindMyBook.ViewModels;

namespace FindMyBook.Controllers
{
    public class BookOrderController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();

        // GET: BookOrder
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookOrder/Details/5
        public ActionResult Details(int id)
        {
            // Fetch the cart item and associated book details
            var bookDetail = (from cartItem in db.table_cart
                              join book in db.table_book_detail on cartItem.book_id_FK equals book.book_id
                              where cartItem.cart_id == id
                              select new CartViewModel
                              {
                                  CartId = cartItem.cart_id,
                                  BookId = book.book_id,
                                  BookName = book.book_name,
                                  Price = book.book_price,
                                  BookImage = book.book_image,
                                  IsbnNumber = book.book_isbn_number,
                                  PublishedDate = book.book_published_date,
                                  Language = book.book_language,
                                  Pages = book.pages,

                              }).FirstOrDefault();

            if (bookDetail == null)
            {
                return HttpNotFound("Cart item not found.");
            }

            return View(bookDetail);
        }

        // GET: BookOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookOrder/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookOrder/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookOrder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
