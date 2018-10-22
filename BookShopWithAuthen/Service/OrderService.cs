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
        public List<Order> GetAll()
        {
            return _repo.Get().ToList();
        }
        public Order GetByID(int id)
        {
            return _repo.Get(o => o.Id.Equals(id)).SingleOrDefault();
        }
        public void ChangeStatus(int id, int statusOrder)
        {
            Order order = GetByID(id);
            order.Status = statusOrder;
            _repo.SaveChanges();
        }

    }
}