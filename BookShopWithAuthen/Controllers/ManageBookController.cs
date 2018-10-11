using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WebWithAuthentication.Controllers
{
    public class ManageBookController : Controller
    {
        // GET: ManageBook
        [HttpGet]
        [Route("api/getbooks")]
        public JsonResult Index()
        {
            Book tmpBook = new Book();
            List<Object> bookList = new List<Object>();
            var book1 = new { name = "book 1", category = "cate1" };
            var book2 = new { name = "book 2", category = "cate2" };
            bookList.Add(book1);
            bookList.Add(book2);
            return Json(bookList, JsonRequestBehavior.AllowGet); 
        }
    }
}