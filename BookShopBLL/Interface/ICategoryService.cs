using BookShopBOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookShopBLL.Interface
{
    public interface ICategoryService
    {
        List<Category> getAllCategories();
        SelectList getSelectListOfCategory();
    }
}
