using BookShopWithAuthen.ViewModel;
using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookShopWithAuthen.Service;

namespace BookShopWithAuthen.Controllers
{
    public class CategoriesController : Controller
    {
        private AuthorService _authorService;
        private CategoryService _categoryService;
        private BookService _bookService;
        public CategoriesController()
        {
            _authorService = new AuthorService();
            _categoryService = new CategoryService();
            _bookService = new BookService();
        }
        // GET: Categories
        public ActionResult Index(SearchCategoryModel searchCategoryModel)
        {
            int pageSize = 10;
            //get select list of Author ID
            ViewBag.authorIDList = _authorService.getSelectListOfAuthor();
            // get list of category
            ViewBag.listCategory = _categoryService.getAllCategories();
            // get selected list of category
            ViewBag.categoryID = _categoryService.getSelectListOfCategory();
            // get selected list of selectBy
            ViewBag.listSortType = _bookService.GetSelectListSortBy();
            // get all books
            // ID = -1 get books of all category
            var allWarehouseBooks = _bookService.FindAllBooksOfSearch(searchCategoryModel);
            ViewBag.pageCount = Math.Ceiling(allWarehouseBooks.Count() / (pageSize*1.0));
            int startIndex = pageSize * (searchCategoryModel.Page - 1);


            ViewBag.allBooks = _bookService.FindAllBooksOfSearch(searchCategoryModel).Skip(startIndex).Take(pageSize).ToList();
            return View(searchCategoryModel);
        }
    }
}
