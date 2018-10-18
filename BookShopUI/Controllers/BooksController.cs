using BookShopBLL.Interface;
using BookShopBOL.Models;
using BookShopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookShopUI.Controllers
{
    public class BooksController : Controller
    {
        private IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
                // GET: Books
        public ActionResult Index(int? id)
        {
            // get book
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _bookService.getByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            // get sach cung the loai
            ViewBag.sameCateBooks = _bookService.getBooksSameCategory(6, id);
            return View(book);
        }
    }
}