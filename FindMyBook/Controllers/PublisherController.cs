using FindMyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindMyBook.Controllers
{
    public class PublisherController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();

        // GET: Publisher
        public ActionResult Index()
        {
            return View(db.table_publisher.ToList());
        }

        // GET: Publisher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publisher/Create
        [HttpPost]
        public ActionResult Create(table_publisher table_Publisher)
        {
            try
            {
                if (db.table_publisher.Any(publisherInfo => publisherInfo.publisher_name == table_Publisher.publisher_name))
                {

                    return Json(new { status = "409", message = "Sorry! The publisher name already exist!" });

                }
                else
                {
                    db.table_publisher.Add(table_Publisher);
                    db.SaveChanges();

                    return Json(new { status = "200", message = "New publisher detail has been registered successfully..." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = "401", message = "An error occured in Create() in Publisher Controller: " + ex.Message });
            }
        }

        // GET: Publisher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publisher/Edit/5
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

        // GET: Publisher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Publisher/Delete/5
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
