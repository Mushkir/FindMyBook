using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindMyBook.Models;

namespace FindMyBook.Controllers
{
    public class AdminController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Login Function
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(table_admin table_Admin)
        {
            try
            {
                var adminData = db.table_admin.Where(adminInfo => adminInfo.username.Equals(table_Admin.username) && adminInfo.password.Equals(table_Admin.password)).FirstOrDefault();

                if (adminData != null)
                {
                    Session["AdminId"] = adminData.admin_id.ToString();
                    Session["AdminName"] = adminData.last_name.ToString();

                    ViewBag.AdminName = adminData.last_name.ToString();

                    return RedirectToAction("Dashboard", "Admin");

                }
                else
                {
                    ViewBag.Notification = "Wrong Username or Password.";
                    
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Notification = "An error occured: " + ex.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        // SignUp Function
        [HttpPost]
        public ActionResult SignUp(table_admin table_Admin)
        {
            try
            {
                if (db.table_admin.Any(userInput => userInput.email == table_Admin.email && userInput.username == table_Admin.username))
                {
                    ViewBag.Notification = "Email address and Username already exist!";
                    return View();

                }
                else if (db.table_admin.Any(userInput => userInput.email == table_Admin.email))
                {
                    ViewBag.Notification = "Email address already exist!";
                    return View();

                }
                else if (db.table_admin.Any(userInput => userInput.username == table_Admin.username))
                {
                    ViewBag.Notification = "Username already exist!";
                    return View();

                }
                else
                {
                    db.table_admin.Add(table_Admin);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Admin");
                }

            } catch (Exception ex)
            {
                ViewBag.Notification = "An error occured: " + ex.Message;
                return View();
            }
        }

        // Dashboard Function
        public ActionResult Dashboard()
        {
            // Total Books
            ViewBag.TotalBooks = db.table_book_detail.Count();

            // Total Customers
            ViewBag.TotalCustomers = db.table_customer.Count();

            // Total Publishers
            ViewBag.TotalPublishers = db.table_publisher.Count();

            // Total Orders
            ViewBag.TotalOrders = db.table_customer_order_book.Count();

            // Total Payment
            ViewBag.TotalPayments = db.table_customer_order_book.Count();

            // Pending Orders
            ViewBag.TotalPendingOrders = db.table_cart.Count(countPendingOrders => countPendingOrders.confirmation_status == 0);

            // Pending Payments
            ViewBag.TotalPendingPayments = db.table_customer_order_book.Count(countPendingPayments => countPendingPayments.payment_id_FK == 0);

            return View();
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}