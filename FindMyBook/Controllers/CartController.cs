using FindMyBook.Models;
using FindMyBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyBook.Controllers
{
    public class CartController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();
        // GET: Cart


        public ActionResult Index()
        {
            if (Session["CustomerId"] == null)
            {
                ViewBag.Message = "Please Login to see your cart items...";
                return View("Index");
            }
            else
            {
                int customerId;
                try
                {
                    // Try to safely convert the session value to an integer
                    customerId = Convert.ToInt32(Session["CustomerId"]);
                }
                catch (InvalidCastException)
                {
                    // Handle the invalid cast exception if the conversion fails
                    ViewBag.Message = "Session value for CustomerId is not valid.";
                    return View("Index");
                }

                // Fetch cart details for the logged-in user
                var cartItemsDetail = (from cartItem in db.table_cart
                                       join book in db.table_book_detail on cartItem.book_id_FK equals book.book_id
                                       where cartItem.customer_id_FK == customerId && cartItem.confirmation_status == 0
                                       select new CartViewModel
                                       {
                                           CartId = cartItem.cart_id,
                                           BookId = book.book_id,
                                           BookName = book.book_name,
                                           Price = book.book_price,
                                           BookImage = book.book_image
                                       }).ToList(); // Fetch all matching records

                return View(cartItemsDetail);
            }
        }

        [HttpGet]
        public ActionResult AddCart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCart(int customerId, int bookId)
        {
            int status = 0;
            // Assuming table_cart is your entity class for the table_cart table
            var table_Cart = new table_cart
            {
                customer_id_FK = customerId,
                book_id_FK = bookId
            };
            table_Cart.confirmation_status = status;
            db.table_cart.Add(table_Cart);
            db.SaveChanges();

            // Return success status
            return Json(new { status = "200" });
        }

        [HttpPost]
        public ActionResult CountCartItems(int customerId)
        {
            // Count the number of cart items for the given customer ID
            var countedItem = db.table_cart.Count(a => a.customer_id_FK == customerId);

            // Return the count as JSON to the frontend
            return Json(new { count = countedItem });

        }

        public ActionResult DeleteCart(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCartConfirmed(int id)
        {
            var cartItem = db.table_cart.SingleOrDefault(c => c.cart_id == id);
            if (cartItem != null)
            {
                db.table_cart.Remove(cartItem);
                db.SaveChanges();
                return Json(new { success = true, message = "Cart item deleted successfully." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "Failed to delete cart item." }, JsonRequestBehavior.AllowGet);
        }

    }
}