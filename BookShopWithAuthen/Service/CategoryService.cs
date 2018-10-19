using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebWithAuthentication.Service
{
    public class CategoryService:BaseService<Category>
    {
        public List<Category> getAllCategories()
        {
            return _repo.Get().ToList();
        }

        public SelectList getSelectListOfCategory()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (Category cate in _repo.Get())
            {
                selectListItems.Add(new SelectListItem { Text = cate.Name, Value = Convert.ToString(cate.ID) });
            }
            return new SelectList(selectListItems, "Value", "Text", -1);

        }
    }
}