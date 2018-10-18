using BookShopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookShopBLL;
using BookShopBOL.ViewModel;
using BookShopBLL.Interface;

namespace BookShopUI.Controllers
{
    public class CategoriesController : Controller
    {
        private IAuthorService _authorService;
        private ICategoryService _categoryService;
        private IBookService _bookService;
        public CategoriesController()
        {
            _authorService = new AuthorService();
            _categoryService = new CategoryService();
            _bookService = new BookService();
        }
        // GET: Categories
        public ActionResult Index(SearchCategoryModel searchCategoryModel)
        {

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
            ViewBag.allBooks = _bookService.FindAllBooksOfSearch(searchCategoryModel);
            return View(searchCategoryModel);
        }
    }
}
