using BackEndBookShop.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace BackEndBookShop.Controllers
{
    public class TestController : Controller
    {
        private BookShopContext db = new BookShopContext();

        // GET: Categories
        public ActionResult Index(int ID = 1)
        {
            // get map of category
            var mapCategory = new Dictionary<string, int>();
            var listCategory = (from c in db.Categories
                                select c).ToList();
            foreach (Category c in listCategory)
            {
                mapCategory.Add(c.Name, c.Books.Count());
            }
            ViewBag.mapCategory = mapCategory;

            // get all books of category
            var allBooks = (db.Books.Where(b => b.CategoryID == ID)).ToList();
            ViewBag.allBooks = allBooks;
            return View();
        }


    }
}
