using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindMyBook.Models;
using FindMyBook.ViewModels;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using Stripe.Issuing;

namespace FindMyBook.Controllers
{
    public class PaymentController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();
        // GET: Payment
        public ActionResult Index(Int32 id)
        {
            var cartItem = db.table_cart.Find(id);

            return View("id: " + cartItem.cart_id);
        }

        // GET: Payment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
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

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Payment/Edit/5
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

        // GET: Payment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Payment/Delete/5
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

        [HttpGet]
        public ActionResult Checkout(int cartId)
        {
            if (cartId <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid cartId");
            }

            var bookDetail = (from cartItem in db.table_cart
                              join book in db.table_book_detail on cartItem.book_id_FK equals book.book_id
                              where cartItem.cart_id == cartId && cartItem.confirmation_status == 1
                              select new CartViewModel
                              {
                                  CartId = cartItem.cart_id,
                                  BookName = book.book_name,
                                  Price = book.book_price
                              }
                              ).FirstOrDefault();

            if (bookDetail == null)
            {
                return HttpNotFound("Cart item not found");
            }

            double bookPrice = bookDetail.Price;
            string amountInStringValue = Convert.ToString(bookPrice);

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt32(amountInStringValue)*100,
                            Currency = "lkr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = bookDetail.BookName,
                            }
                        },
                        Quantity = 1,
                    }
                },
                Mode = "payment",
                SuccessUrl = $"https://localhost:44390/Payment/ConfirmPayment?cartId={cartId}",
                CancelUrl = "https://localhost:44390/Cart/Index",
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }

        [HttpGet]
        public ActionResult ConfirmPayment(int cartId)
        {
            if (cartId <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid cartId");
            }

            var order = db.table_customer_order_book
                 .Where(o => o.customer_id_FK == cartId)
                 .Select(o => new { o.order_id, o.customer_id_FK })
                 .FirstOrDefault();

            if (order == null)
            {
                return HttpNotFound("Order not found for the provided cartId");
            }

            var orderId = order.order_id;

            var orderedBookDetail = db.table_customer_order_book.Find(orderId);

            orderedBookDetail.payment_id_FK = 1;
            db.Entry(orderedBookDetail).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Feedback", new { orderId = orderId});
            //return View(order);
        }
    }

   
}
