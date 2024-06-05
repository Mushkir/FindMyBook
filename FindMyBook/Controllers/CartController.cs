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

        [HttpPost]
        public ActionResult AddCart(int customerId, int bookId)
        {
            

            //db.table_cart.Add(table_Cart);
            //db.SaveChanges();

            return Json(new { customerID = customerId, book = bookId });
        }
    }
}