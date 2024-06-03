using FindMyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyBook.Controllers
{
    public class BookStatusController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();

        // GET: BookStatus
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookStatus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(table_book_status table_Book_Status)
        {
            try
            {
                if (db.table_book_status.Any(bookStatus => bookStatus.book_status == table_Book_Status.book_status))
                {
                    return Json(new { status = "409", message = "The entered status already exist!" });
                
                } else
                {
                    db.table_book_status.Add(table_Book_Status);
                    db.SaveChanges();

                    return Json(new { status = "200", message = "New status entered successfully." });

                }


            }
            catch (Exception ex)
            {
                return Json(new { status = "520", message = "An error occured: " + ex.Message });
            }
        }

        // GET: BookStatus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookStatus/Edit/5
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

        // GET: BookStatus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookStatus/Delete/5
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
