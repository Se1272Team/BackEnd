using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopWithAuthen.Service
{
    public class BookService:BaseService<Book>
    {
        public List<Book> FindAllBooksOfSearch(SearchCategoryModel searchCategoryModel)
        {
            var allBooks = from b in _repo.Get()
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
            return allBooks.ToList();
        }

        public List<Book> getBooksSameCategory(int limit, int? ID)
        {
            int idCategory = _repo.Get(b => b.ID == ID).SingleOrDefault().CategoryID;
            var sameCateBooks = _repo.Get(b => b.CategoryID == idCategory).Take(limit);
            return sameCateBooks.ToList();
        }

        public Book getByID(int? ID)
        {
            return _repo.Get(b => b.ID == ID).SingleOrDefault();
        }
        public Book getByID(int ID)
        {
            return _repo.Get(b => b.ID == ID).SingleOrDefault();
        }

        public SelectList GetSelectListSortBy()
        {
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
            return new SelectList(selectListItemsOrderBy, "Value", "Text");

        }

    
    }
}