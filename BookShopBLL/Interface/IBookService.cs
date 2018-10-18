using BookShopBOL.Models;
using BookShopBOL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookShopBLL.Interface
{
    public interface IBookService
    {
        List<Book> FindAllBooksOfSearch(SearchCategoryModel searchCategoryModel);
        SelectList GetSelectListSortBy();
        Book getByID(int? ID);
        List<Book> getBooksSameCategory(int limit, int? ID);
    }
}
