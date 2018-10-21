using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWithAuthentication.Models;

namespace WebWithAuthentication.Service
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