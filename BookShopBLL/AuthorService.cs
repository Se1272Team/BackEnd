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
    public class AuthorService : BaseService<Author>, IAuthorService
    {


        public SelectList getSelectListOfAuthor()
        {
            List<SelectListItem> selectListItemsAuthor = new List<SelectListItem>();
            foreach (Author item in _repo.Get())
            {
                selectListItemsAuthor.Add(new SelectListItem { Text = item.Name, Value = Convert.ToString(item.ID) });
            }
            return new SelectList(selectListItemsAuthor, "Value", "Text", -1);

        }
    }
}
