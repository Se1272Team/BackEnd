using BackEndBookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BackEndBookShop.Controllers
{
    public class BooksController : Controller
    {
        private BookShopContext db = new BookShopContext();
        // GET: Books
        public ActionResult Index(int? id)
        {
            // get book
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            // get sach cung the loai
            int idCategory = db.Books.Find(id).CategoryID;
            var sameCateBooks = (from b in db.Books
                                 where b.CategoryID == idCategory
                                 select b).Take(6);
            ViewBag.sameCateBooks = sameCateBooks.ToList();
            return View(book);
        }
    }
}