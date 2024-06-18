using System;
using System.Collections.Generic;
using System.Data;
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
        public ActionResult Create(int id)
        {
            return View();
        }

        // POST: BookOrder/Create
        [HttpPost]
        public JsonResult Create(int CartId, double Price)
        {
            try
            {
                // TODO: Add insert logic here
                var cartItem = db.table_cart.Find(CartId);
                if (cartItem == null)
                {
                    return Json(new { status = 404, message = "Cart item not found." });
                }

                DateTime orderDate = DateTime.Now;
                DateTime deliveryDate = orderDate.AddDays(10);

                table_customer_order_book table_Customer_Order_Book = new table_customer_order_book
                {
                    customer_id_FK = cartItem.cart_id,
                    total_price = Price,
                    order_date = orderDate,
                    delivery_date = deliveryDate,
                    order_status_id_FK = 0,
                    feedback_id_FK = 0,
                    payment_id_FK = 0
                };

                db.table_customer_order_book.Add(table_Customer_Order_Book);

                cartItem.confirmation_status = 1;
                db.Entry(cartItem).State = EntityState.Modified;

                db.SaveChanges();
                //return RedirectToAction("Index");
                return Json(new { status = 200, message = "Order confirmed successfully!" });
            }
            catch (Exception ex)
            {
                //return View();
                return Json(new { status = 500, message = ex.Message });
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
