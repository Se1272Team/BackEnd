using BookShopWithAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShopWithAuthen.Service
{
    public class AuthorService:BaseService<Author>
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