using FindMyBook.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace FindMyBook.Controllers
{
    public class AuthorController : Controller
    {
        findMyBookEntities1 db = new findMyBookEntities1();
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(table_author table_Author) 
        {
            try
            {
                if (db.table_author.Any(authorInfo => authorInfo.author_name == table_Author.author_name))
                {
                    return Json(new { status = "409", message = "The author name is already exist!" });

                }
                else
                {
                    db.table_author.Add(table_Author);
                    db.SaveChanges();

                    return Json(new { status = "200", message = "The author's detail registered successfully." });
                }
            } catch(Exception ex)
            {
                return Json(new { status = "401", message = "An error occured: " + ex.Message });
            }
        }


        // View all authors
        public ActionResult ManageAuthors()
        {
            return View(db.table_author.ToList());
        }

        // View author details based on their id
        public ActionResult AuthorDetails(int id)
        {
            return View(db.table_author.Where(x => x.author_id == id).FirstOrDefault());

        }

        // View and Update
        public ActionResult EditAuthor(Int32 id)
        {
            try
            {
                var AuthorData = db.table_author.Where(authorData => authorData.author_id == id).FirstOrDefault();
                if (AuthorData != null)
                {
                    TempData["AuthorID"] = id;
                     
                    ViewBag.AuthorName = AuthorData.author_name;
                    TempData.Keep();
                    return View(AuthorData);
                }
                return View();
            } catch (Exception ex)
            {
                ViewBag.Notification = "An error occured: " + ex.Message;
                return View();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAuthor(table_author table_Author)
        {
            try
            {
                Int32 authorId = (int)TempData["AuthorID"];

                var authorInfo = db.table_author.Where(authorData => authorData.author_id == authorId).FirstOrDefault();
                if (authorInfo != null)
                {
                    authorInfo.author_name = table_Author.author_name;
                    authorInfo.author_education = table_Author.author_education;
                    authorInfo.author_country = table_Author.author_country;
                    authorInfo.number_of_published_books = table_Author.number_of_published_books;

                    db.Entry(authorInfo).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Json(new { status = "200", message = "Author ID No. " + authorId + " has been updated successfully..." });

            }
            catch (Exception ex)
            {
                return Json(new { status = "401", message = "An error occured in Second Edit(): " + ex.Message });
            }
        }

        public ActionResult DeleteAuthor(Int32 id)
        {
           var authorInfo =  db.table_author.Where(authorData => authorData.author_id == id).FirstOrDefault();
           if (authorInfo != null)
            {
                TempData["AuthorID"] = id;
                ViewBag.AuthorName = authorInfo.author_name;
                TempData.Keep();
                return View(authorInfo);
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(table_author table_Author)
        {
            try
            {
                Int32 id = (int)TempData["AuthorID"];
                var authorData = db.table_author.Where(authorInfo => authorInfo.author_id == id).FirstOrDefault();
                if (authorData != null)
                {
                    db.Entry(authorData).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                return Json(new { status = "200", message = "Author ID No. " + id + " has been deleted successfully..." });
         
            } catch (Exception ex)
            {
                return Json(new { status = "401", message = "An error occured from DeleteAuthor(): " + ex.Message });
            }
        }
    }
}