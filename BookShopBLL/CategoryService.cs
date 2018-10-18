using BookShopBLL.Interface;
using BookShopBOL.Models;
using BookShopDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookShopBLL
{
    public class CategoryService :BaseService<Category>, ICategoryService
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
