using BookShopWithAuthen.Domain;
using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookShopWithAuthen.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Categories
        public ActionResult Index(SearchCategoryModel searchCategoryModel)
        {
            // get select list of Author ID
            List<SelectListItem> selectListItemsAuthor = new List<SelectListItem>();
            foreach (Author item in db.Authors)
            {
                selectListItemsAuthor.Add(new SelectListItem { Text = item.Name, Value = Convert.ToString(item.ID) });
            }
            ViewBag.authorIDList = new SelectList(selectListItemsAuthor, "Value", "Text", -1);
            // get list of category
            var listCategory = (from c in db.Categories
                                select c).ToList();
            ViewBag.listCategory = listCategory;
            // get selected list of category
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Category cate in db.Categories)
            {
                selectListItems.Add(new SelectListItem { Text = cate.Name, Value = Convert.ToString(cate.ID) });
            }
            ViewBag.categoryID = new SelectList(selectListItems, "Value", "Text", -1);
            // get selected list of selectBy
            Dictionary<string, int> dicSortType = new Dictionary<string, int>();
            dicSortType.Add("Thứ tự theo giá: Cao đến thấp", (int)sortType.orderByPriceHigh);
            dicSortType.Add("Thứ tự theo giá: Thấp đến cao", (int)sortType.orderByPriceLow);
            dicSortType.Add("Thứ tự theo sản phẩm mới", (int)sortType.orderByNew);
            dicSortType.Add("Thứ tự theo mua nhiều", (int)sortType.orderBySell);
            dicSortType.Add("Thứ tự theo đánh giá", (int)sortType.orderByRate);
            List<SelectListItem> selectListItemsOrderBy = new List<SelectListItem>();
            foreach (KeyValuePair<string, int> entry in dicSortType)
            {
                selectListItemsOrderBy.Add(new SelectListItem { Text = entry.Key, Value = Convert.ToString(entry.Value) });
            }
            ViewBag.listSortType = new SelectList(selectListItemsOrderBy, "Value", "Text");


            // get all books
            // ID = -1 get books of all category
            var allBooks = from b in db.Books
                           where b.Price >= searchCategoryModel.PriceFrom && b.Price <= searchCategoryModel.PriceTo
                           select b;
            if (!string.IsNullOrEmpty(searchCategoryModel.SearchValue))
            {
                allBooks = allBooks.Where(b => b.Name.Contains(searchCategoryModel.SearchValue));
            }
            if (searchCategoryModel.ID != -1)
            {
                allBooks = allBooks.Where(b => b.CategoryID == searchCategoryModel.ID);
            }
            if (searchCategoryModel.AuthorID != -1)
            {
                allBooks = allBooks.Where(b => b.Authors.FirstOrDefault(a => a.ID == searchCategoryModel.AuthorID) != null);
            }
            switch (searchCategoryModel.sortBy)
            {
                case (int)sortType.orderByPriceHigh:
                    allBooks = allBooks.OrderByDescending(b => b.Price);
                    break;
                case (int)sortType.orderByPriceLow:
                    allBooks = allBooks.OrderBy(b => b.Price);
                    break;

            }
            ViewBag.allBooks = allBooks.ToList();
            return View(searchCategoryModel);
        }
    }
}
