using FindMyBook.Models;
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
            return View();
        }
        [HttpGet]
        public ActionResult AddCart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCart(int customerId, int bookId)
        {
            // Assuming table_cart is your entity class for the table_cart table
            var table_Cart = new table_cart
            {
                customer_id_FK = customerId,
                book_id_FK = bookId
            };

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
    }
}