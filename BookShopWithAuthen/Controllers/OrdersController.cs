using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BookShopWithAuthen.Models;
using Microsoft.AspNet.Identity;
using BookShopWithAuthen.Models;
using BookShopWithAuthen.Service;
using BookShopWithAuthen.ViewModel;
using Rotativa;

namespace BookShopWithAuthen.Controllers
{
    [Authorize(Roles ="User")]
    public class OrdersController : Controller
    {
        private OrderService _orderService;
        private CartService _cartService;
        private BookService _bookService;

        public OrdersController()
        {
            _orderService = new OrderService();
            _cartService = new CartService();
            _bookService = new BookService();
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            Order order = _orderService.GetByID(id);
            if (!order.UserId.Equals(User.Identity.GetUserId()))
            {
                return HttpNotFound();
            }
            ICollection<OrderDetailViewModel> orderDetailViewModels = new List<OrderDetailViewModel>();
            var orderDetails = order.OrderDetails;
            foreach (var item in orderDetails)
            {
                OrderDetailViewModel tmpOrDeViewModel = Mapper.Map<OrderDetailViewModel>(item);
                orderDetailViewModels.Add(tmpOrDeViewModel);
            }
            ViewBag.orderDetailViewModels = orderDetailViewModels;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            string userId = User.Identity.GetUserId();
            Order order = _orderService.GetByID(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                _orderService.ChangeStatus(id, (int)StatusOrder.Canceled);
                foreach (var item in order.OrderDetails)
                {
                    int wareHouseQuantity = (int)_bookService.getByID(item.BookID).Quantity;
                    _bookService.UpdateQuantityBook(item.BookID, wareHouseQuantity + item.Quantity);

                }
            }
            return RedirectToAction("Index");
        }









    }
}
