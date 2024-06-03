using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindMyBook.Models;

namespace FindMyBook.Controllers
{
    public class HomeController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1(); // Database Entity

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(table_customer table_Customer)
        {
            try
            {

                if (db.table_customer.Any(userInfo => userInfo.customer_username == table_Customer.customer_username && userInfo.email == table_Customer.email))
                {
                    ViewBag.Notification = "Username and Email is already exist!";
                    return View();

                }
                else if (db.table_customer.Any(userInfo => userInfo.customer_username == table_Customer.customer_username))
                {
                    ViewBag.Notification = "This username is already exist! Please try new username!";
                    return View();

                }
                else if (db.table_customer.Any(userInfo => userInfo.email == table_Customer.email))
                {
                    ViewBag.Notification = "This email address already exist!";
                    return View();

                }
                else
                {
                    string fileName = Path.GetFileNameWithoutExtension(table_Customer.ImageFile.FileName);
                    string extension = Path.GetExtension(table_Customer.ImageFile.FileName);

                    table_Customer.customer_image = "../Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("../Image/"), fileName);

                    table_Customer.ImageFile.SaveAs(fileName);
                    table_Customer.account_creation_date = DateTime.Now;

                    db.table_customer.Add(table_Customer);
                    db.SaveChanges();

                    ModelState.Clear();

                    ViewBag.Notification = "Account Created!";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Notification = "An error occured: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(table_customer table_Customer)
        {
            try
            {
                var isUserExist = db.table_customer.Where(userInfo => userInfo.email.Equals(table_Customer.email) && userInfo.customer_password.Equals(table_Customer.customer_password)).FirstOrDefault();
                if (isUserExist != null)
                {
                    Session["CustomerId"] = isUserExist.customer_id.ToString();
                    Session["CustomerName"] = isUserExist.customer_last_name.ToString();

                    return RedirectToAction("Index", "Customer");

                }
                else
                {
                    ViewBag.Notification = "Wrong username and password. Try again...";
                }
                //if (ModelState.IsValid)
                //{


                //} else
                //{
                //    ViewBag.Notification = "Connection error! Please try again later...";
                //}

                return View();

            } catch (Exception ex)
            {
                ViewBag.Notification = "An error occured: " + ex.Message;
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
    }
}