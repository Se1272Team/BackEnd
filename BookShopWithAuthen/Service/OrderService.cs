using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWithAuthentication.Models;

namespace WebWithAuthentication.Service
{
    public class OrderService:BaseService<Order>
    {
        public void Create(Order order)
        {
            _repo.Create(order);
            _repo.SaveChanges();
        }
    }
}