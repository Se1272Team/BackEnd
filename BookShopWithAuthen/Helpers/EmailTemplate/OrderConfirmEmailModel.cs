using BookShopWithAuthen.Models;
using BookShopWithAuthen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShopWithAuthen.Helpers.EmailTemplate
{
    public class OrderConfirmEmailModel
    {
        public Order Order { get; set; }
        public ICollection<OrderDetailViewModel> OrderDetailViewModels { get; set; }
    }
}