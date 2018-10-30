using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShopWithAuthen.Models;

namespace BookShopWithAuthen.Service
{
    public class OrderDetailService:BaseService<OrderDetail>
    {
        public void Create(OrderDetail orderDetail)
        {
            _repo.Create(orderDetail);
            _repo.SaveChanges();
        }
    }
}