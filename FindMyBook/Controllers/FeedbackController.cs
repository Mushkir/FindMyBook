using FindMyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FindMyBook.Controllers
{
    public class FeedbackController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();

        // GET: Feedback
        [HttpGet]
        public ActionResult Index(int orderId)
        {
            if (orderId <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid orderId");
            }

            var paidBookDetail = db.table_customer_order_book.Find(orderId);

            if (paidBookDetail == null)
            {
                return HttpNotFound("Order not found.");
            }

            ViewBag.OrderId = orderId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFeedback(int orderId, string feedback)
        {
            if (orderId <= 0 || string.IsNullOrEmpty(feedback))
            {
                return Json(new { status = 404 , message = "Invalid data" });
            }

            try
            {
                table_feedback newFeedback = new table_feedback
                {
                    order_id_FK = orderId,
                    feedback = feedback,
                    customer_id_FK = 0,
                    book_id_FK = 0
                    
                };

                db.table_feedback.Add(newFeedback);
                db.SaveChanges();

                return Json(new { status = 200, message = "feedback saved successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { status = 501, message = "Error: " + ex.Message });
            }
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Feedback/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback/Create
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

        // GET: Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feedback/Edit/5
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

        // GET: Feedback/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feedback/Delete/5
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
