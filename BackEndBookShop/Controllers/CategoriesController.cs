using BackEndBookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEndBookShop.Controllers
{
    public class CategoriesController : Controller
    {
        private BookShopContext db = new BookShopContext();
        // GET: Categories
        public ActionResult Index(string searchString = "", int ID = -1 )
        {
            
            // get list of category
            var listCategory = (from c in db.Categories
                                select c).ToList();
            ViewBag.listCategory = listCategory;

            // get all books of category
            // ID = -1 get books of all category
            var allBooks = new List<Book>();
            if (ID == -1)
            {
                allBooks = (db.Books.Where(b => (b.Name.Contains(searchString)))).ToList();
            }
            else
            {
                allBooks = (db.Books.Where(b => (b.CategoryID == ID && b.Name.Contains(searchString)))).ToList();
            }
            
            ViewBag.allBooks = allBooks;

            // get selected list of category
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Category cate in db.Categories)
            {
                selectListItems.Add(new SelectListItem {Text=cate.Name,Value=Convert.ToString(cate.ID) });
            }
            ViewBag.categoryID = new SelectList(selectListItems,"Value","Text",-1);
            return View();
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
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

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categories/Edit/5
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

        // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categories/Delete/5
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
